using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sever
{
    public partial class Form1 : Form
    {
        public class OrderItem
        {
            public int TableID { get; set; }
            public string FoodName { get; set; }
            public int Quantity { get; set; }
            public int Price { get; set; }
        }

        private List<OrderItem> listOrders = new List<OrderItem>();
        private string menuContent = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                menuContent = File.ReadAllText("menu.txt");
                TcpListener listener = new TcpListener(IPAddress.Any, 8080);
                listener.Start();
                lblStatus.Text = "Status: Server Running on Port 8080";
                btnStart.Enabled = false;

                Task.Run(() => {
                    while (true)
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Log($"New connection from: {client.Client.RemoteEndPoint}");
                        Task.Run(() => HandleClient(client));
                    }
                });
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void HandleClient(TcpClient client)
        {
            using (var stream = client.GetStream())
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            using (var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true })
            {
                try
                {
                    while (client.Connected)
                    {
                        string request = reader.ReadLine();
                        if (string.IsNullOrEmpty(request)) break;
                        Log($"Request: {request}");

                        string[] parts = request.Split('|');
                        switch (parts[0])
                        {
                            case "MENU":
                                writer.WriteLine(menuContent.Replace("\r\n", "#").Replace("\n", "#"));
                                break;
                            case "ORDER": // ORDER|TableID|FoodName|Qty|Price
                                listOrders.Add(new OrderItem
                                {
                                    TableID = int.Parse(parts[1]),
                                    FoodName = parts[2],
                                    Quantity = int.Parse(parts[3]),
                                    Price = int.Parse(parts[4])
                                });
                                writer.WriteLine("OK");
                                break;
                            case "GET_ORDERS":
                                string data = string.Join("#", listOrders.Select(o => $"{o.TableID};{o.FoodName};{o.Quantity};{o.Price}"));
                                writer.WriteLine(string.IsNullOrEmpty(data) ? "EMPTY" : data);
                                break;
                            case "PAY": // PAY|TableID
                                int tableId = int.Parse(parts[1]);
                                var bills = listOrders.Where(o => o.TableID == tableId).ToList();
                                long total = bills.Sum(o => (long)o.Quantity * o.Price);
                                writer.WriteLine(total.ToString());
                                listOrders.RemoveAll(o => o.TableID == tableId);
                                Log($"Table {tableId} Paid: {total}");
                                break;
                            case "QUIT":
                                return;
                        }
                    }
                }
                catch { }
            }
        }

        private void Log(string msg)
        {
            rtbLog.Invoke((MethodInvoker)(() => rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n")));
        }
    }
}