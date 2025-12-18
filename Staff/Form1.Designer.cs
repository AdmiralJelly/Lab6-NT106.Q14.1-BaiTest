namespace Staff
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnConn = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtTable = new System.Windows.Forms.TextBox();
            this.btnPay = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();

            // label1 (Nhãn IP)
            this.label1.Text = "Server IP:";
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Size = new System.Drawing.Size(60, 20);

            // txtIP (Nhập IP)
            this.txtIP.Location = new System.Drawing.Point(80, 17);
            this.txtIP.Size = new System.Drawing.Size(150, 23);
            this.txtIP.Text = "127.0.0.1";

            // btnConn (Kết nối)
            this.btnConn.Location = new System.Drawing.Point(240, 15);
            this.btnConn.Size = new System.Drawing.Size(100, 30);
            this.btnConn.Text = "Kết nối";
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);

            // btnRefresh (Làm mới)
            this.btnRefresh.Location = new System.Drawing.Point(350, 15);
            this.btnRefresh.Size = new System.Drawing.Size(120, 30);
            this.btnRefresh.Text = "Cập nhật đơn";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // dgv (Bảng danh sách món)
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                new System.Windows.Forms.DataGridViewTextBoxColumn() { Name = "Table", HeaderText = "Bàn", Width = 80 },
                new System.Windows.Forms.DataGridViewTextBoxColumn() { Name = "Food", HeaderText = "Món Ăn", Width = 200 },
                new System.Windows.Forms.DataGridViewTextBoxColumn() { Name = "Qty", HeaderText = "SL", Width = 60 },
                new System.Windows.Forms.DataGridViewTextBoxColumn() { Name = "Price", HeaderText = "Giá", Width = 100 },
                new System.Windows.Forms.DataGridViewTextBoxColumn() { Name = "Total", HeaderText = "Thành tiền", Width = 120 }
            });
            this.dgv.Location = new System.Drawing.Point(15, 60);
            this.dgv.Size = new System.Drawing.Size(750, 320);

            // label2 (Nhãn Bàn)
            this.label2.Text = "Nhập bàn:";
            this.label2.Location = new System.Drawing.Point(15, 405);
            this.label2.Size = new System.Drawing.Size(80, 20);

            // txtTable (Số bàn cần thanh toán)
            this.txtTable.Location = new System.Drawing.Point(100, 402);
            this.txtTable.Size = new System.Drawing.Size(80, 23);

            // btnPay (Thanh toán)
            this.btnPay.Location = new System.Drawing.Point(200, 395);
            this.btnPay.Size = new System.Drawing.Size(120, 40);
            this.btnPay.Text = "THANH TOÁN";
            this.btnPay.BackColor = System.Drawing.Color.LightGreen;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);

            // lblTotal (Hiển thị tiền)
            this.lblTotal.Location = new System.Drawing.Point(350, 405);
            this.lblTotal.Size = new System.Drawing.Size(300, 20);
            this.lblTotal.Text = "Tổng: 0 VNĐ";
            this.lblTotal.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);

            // Form1
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.dgv, this.txtIP, this.btnConn, this.btnRefresh,
                this.txtTable, this.btnPay, this.lblTotal, this.label1, this.label2
            });
            this.Name = "Form1";
            this.Text = "Hệ Thống Quản Lý Nhân Viên - Staff App";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtTable;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}