namespace Client
{
    partial class Form1
    {
        private void InitializeComponent()
        {
            dgv = new DataGridView();
            txtIP = new TextBox();
            btnConn = new Button();
            btnOrder = new Button();
            numTable = new NumericUpDown();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTable).BeginInit();
            SuspendLayout();
            // 
            // dgv
            // 
            dgv.ColumnHeadersHeight = 29;
            dgv.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            dgv.Location = new Point(12, 50);
            dgv.Name = "dgv";
            dgv.RowHeadersWidth = 51;
            dgv.Size = new Size(460, 200);
            dgv.TabIndex = 0;
            // 
            // txtIP
            // 
            txtIP.Location = new Point(12, 12);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(100, 27);
            txtIP.TabIndex = 1;
            txtIP.Text = "127.0.0.1";
            txtIP.TextChanged += txtIP_TextChanged;
            // 
            // btnConn
            // 
            btnConn.Location = new Point(120, 10);
            btnConn.Name = "btnConn";
            btnConn.Size = new Size(75, 23);
            btnConn.TabIndex = 2;
            btnConn.Text = "Kết nối";
            btnConn.Click += btnConn_Click;
            // 
            // btnOrder
            // 
            btnOrder.Location = new Point(350, 265);
            btnOrder.Name = "btnOrder";
            btnOrder.Size = new Size(120, 35);
            btnOrder.TabIndex = 3;
            btnOrder.Text = "ĐẶT MÓN";
            btnOrder.Click += btnOrder_Click;
            // 
            // numTable
            // 
            numTable.Location = new Point(60, 270);
            numTable.Name = "numTable";
            numTable.Size = new Size(120, 27);
            numTable.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.Width = 125;
            // 
            // Form1
            // 
            ClientSize = new Size(484, 320);
            Controls.Add(dgv);
            Controls.Add(txtIP);
            Controls.Add(btnConn);
            Controls.Add(btnOrder);
            Controls.Add(numTable);
            Name = "Form1";
            Text = "Khách hàng";
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.NumericUpDown numTable;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}