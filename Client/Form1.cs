using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        TcpClient client; StreamReader reader; StreamWriter writer;
        public Form1() { InitializeComponent(); }

        private void btnConn_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient(txtIP.Text, 8080);
                var s = client.GetStream();
                reader = new StreamReader(s, Encoding.UTF8);
                writer = new StreamWriter(s, Encoding.UTF8) { AutoFlush = true };
                writer.WriteLine("MENU");
                string[] items = reader.ReadLine().Split('#');
                foreach (var i in items)
                {
                    if (!string.IsNullOrEmpty(i)) dgv.Rows.Add(i.Split(';'));
                }
                btnConn.Enabled = false; txtIP.Enabled = false;
            }
            catch { MessageBox.Show("Lỗi kết nối IP!"); }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dgv.Rows)
            {
                int q = Convert.ToInt32(r.Cells[3].Value);
                if (q > 0)
                {
                    writer.WriteLine($"ORDER|{numTable.Value}|{r.Cells[1].Value}|{q}|{r.Cells[2].Value}");
                    reader.ReadLine();
                }
            }
            MessageBox.Show("Đặt món thành công!");
        }

        private void txtIP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}