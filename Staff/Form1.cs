using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Staff
{
    public partial class Form1 : Form
    {
        TcpClient client;
        StreamReader reader;
        StreamWriter writer;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIP.Text)) { MessageBox.Show("Vui lòng nhập IP!"); return; }

                client = new TcpClient(txtIP.Text, 8080);
                var s = client.GetStream();
                reader = new StreamReader(s, Encoding.UTF8);
                writer = new StreamWriter(s, Encoding.UTF8) { AutoFlush = true };

                btnConn.Enabled = false;
                btnConn.Text = "Đã kết nối";
                MessageBox.Show("Kết nối Server thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (client == null || !client.Connected) { MessageBox.Show("Chưa kết nối Server!"); return; }

                writer.WriteLine("GET_ORDERS");
                string res = reader.ReadLine();
                dgv.Rows.Clear();

                if (string.IsNullOrEmpty(res) || res == "EMPTY") return;

                foreach (var line in res.Split('#'))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] d = line.Split(';');
                    // Tính tổng tiền cho từng dòng
                    int totalRow = int.Parse(d[2]) * int.Parse(d[3]);
                    dgv.Rows.Add(d[0], d[1], d[2], d[3], totalRow);
                }
            }
            catch { MessageBox.Show("Lỗi khi cập nhật dữ liệu!"); }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTable.Text)) { MessageBox.Show("Nhập số bàn cần thanh toán!"); return; }
                if (client == null) return;

                writer.WriteLine($"PAY|{txtTable.Text}");
                string total = reader.ReadLine();

                lblTotal.Text = $"Tổng: {total} VNĐ";

                // Xuất file hóa đơn
                string fileName = $"bill_Ban{txtTable.Text}.txt";
                string content = $"HOÁ ĐƠN THANH TOÁN\nBàn: {txtTable.Text}\nNgày: {DateTime.Now}\n------------------\nTổng cộng: {total} VNĐ";
                File.WriteAllText(fileName, content);

                MessageBox.Show($"Thanh toán Bàn {txtTable.Text} thành công!\nTổng tiền: {total} VNĐ\nĐã xuất hóa đơn.");

                // Cập nhật lại danh sách sau khi thanh toán
                btnRefresh_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message);
            }
        }
    }
}