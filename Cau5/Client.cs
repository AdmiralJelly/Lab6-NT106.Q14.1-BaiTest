using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading; 

namespace Cau5
{
    public partial class Client : Form
    {
        TcpClient client;
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;
        Thread recvThread;
        bool isConnected = false;
        List<FoodItem> menuList = new List<FoodItem>();

        public class FoodItem
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public override string ToString()
            {
                return $"{Name} - {Price}";
            }
        }

        public Client()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("10.245.146.21", 8080);
                stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                writer.AutoFlush = true;

                isConnected = true;
                lblStatus.Text = "Đã kết nối Server!";
                lblStatus.ForeColor = Color.Green;
                btnConnect.Enabled = false;

                // Gui lenh lay Menu
                SendData("MENU");

                recvThread = new Thread(Receive);
                recvThread.IsBackground = true;
                recvThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        void SendData(string data)
        {
            if (isConnected && writer != null)
            {
                writer.WriteLine(data);
            }
        }

        void Receive()
        {
            try
            {
                while (isConnected)
                {
                    string line = reader.ReadLine();
                    if (line == null) break;
                    
                    // Xu ly du lieu nhan ve tren UI Thread
                    Invoke(new Action(() => ProcessData(line)));
                }
            }
            catch
            {
                if (isConnected) MessageBox.Show("Mất kết nối với Server.");
            }
        }

        void ProcessData(string data)
        {
            if (data.Contains(";"))
            {
                menuList.Clear();
                string[] lines = data.Split('#');
                foreach (string line in lines)
                {
                    try
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length == 3)
                        {
                            dgvMenu.Rows.Add(parts[0], parts[1], parts[2], "0");
                            
                            menuList.Add(new FoodItem 
                            { 
                                ID = int.Parse(parts[0]),
                                Name = parts[1],
                                Price = int.Parse(parts[2]) 
                            });
                        }
                    }
                    catch { }
                }   
                
                cbFood.DataSource = null;
                cbFood.DataSource = menuList;
            }
            else if (data == "OK")
            {
                MessageBox.Show("Đặt thành công!", "Thông báo");
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("Chưa kết nối Server!");
                return;
            }

            int tableFunc = (int)nudTable.Value;
            bool ordered = false;

            foreach (DataGridViewRow row in dgvMenu.Rows)
            {
                if (row.Cells[3].Value != null)
                {
                    int qty = 0;
                    int.TryParse(row.Cells[3].Value.ToString(), out qty);

                    if (qty > 0)
                    {
                        string dishName = row.Cells[1].Value.ToString();
                        string priceStr = row.Cells[2].Value.ToString();
                        string cmd = $"ORDER|{tableFunc}|{dishName}|{qty}|{priceStr}";
                        SendData(cmd);
                        ordered = true;
                        row.Cells[3].Value = "0";
                    }
                }
            }

            if (nudQty.Value > 0 && cbFood.SelectedItem is FoodItem selectedFood)
            {
                string cmd = $"ORDER|{tableFunc}|{selectedFood.Name}|{nudQty.Value}|{selectedFood.Price}";
                SendData(cmd);
                ordered = true;
                nudQty.Value = 0;
            }

            if (!ordered)
            {
                MessageBox.Show("Vui lòng nhập số lượng món cần đặt.");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isConnected)
            {
                SendData("QUIT");
                isConnected = false;
                client?.Close();
            }
        }
    }
}
