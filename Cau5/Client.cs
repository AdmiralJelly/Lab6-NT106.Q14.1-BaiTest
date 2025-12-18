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
    public partial class Form1 : Form
    {
        TcpClient client;
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;
        Thread recvThread;
        bool isConnected = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("127.0.0.1", 8080);
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
            // Neu server tra ve menu dang: ID;Name;Price (vd: 1;Pho;50000)
            if (data.Contains(";"))
            {
                try
                {
                    string[] parts = data.Split(';');
                    if (parts.Length == 3)
                    {
                        dgvMenu.Rows.Add(parts[0], parts[1], parts[2], "0");
                    }
                }
                catch { }
            }
            else if (data == "OK")
            {
                // Server xac nhan don hang
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

            // Duyet qua tung dong de xem mon nao duoc chon (SL > 0)
            foreach (DataGridViewRow row in dgvMenu.Rows)
            {
                if (row.Cells[3].Value != null)
                {
                    int qty = 0;
                    int.TryParse(row.Cells[3].Value.ToString(), out qty);

                    if (qty > 0)
                    {
                        string dishID = row.Cells[0].Value.ToString();
                        // Format: ORDER <Table> <DishID> <Qty>
                        string cmd = $"ORDER {tableFunc} {dishID} {qty}";
                        SendData(cmd);
                        ordered = true;
                        
                        // Reset so luong ve 0 sau khi dat
                        row.Cells[3].Value = "0";
                    }
                }
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
