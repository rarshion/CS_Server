namespace CS_Server
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_recv_conn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_conn_num = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_stopConn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cb = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.maxclientn = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Node = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Islive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxclientn)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_recv_conn
            // 
            this.bt_recv_conn.Location = new System.Drawing.Point(409, 17);
            this.bt_recv_conn.Name = "bt_recv_conn";
            this.bt_recv_conn.Size = new System.Drawing.Size(89, 23);
            this.bt_recv_conn.TabIndex = 0;
            this.bt_recv_conn.Text = "开始接受连接";
            this.bt_recv_conn.UseVisualStyleBackColor = true;
            this.bt_recv_conn.Click += new System.EventHandler(this.bt_recv_conn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "目前已连接的数量";
            // 
            // lb_conn_num
            // 
            this.lb_conn_num.AutoSize = true;
            this.lb_conn_num.Location = new System.Drawing.Point(113, 55);
            this.lb_conn_num.Name = "lb_conn_num";
            this.lb_conn_num.Size = new System.Drawing.Size(11, 12);
            this.lb_conn_num.TabIndex = 2;
            this.lb_conn_num.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "最大允许连接数";
            // 
            // bt_stopConn
            // 
            this.bt_stopConn.Location = new System.Drawing.Point(409, 50);
            this.bt_stopConn.Name = "bt_stopConn";
            this.bt_stopConn.Size = new System.Drawing.Size(89, 23);
            this.bt_stopConn.TabIndex = 5;
            this.bt_stopConn.Text = "断开所有连接";
            this.bt_stopConn.UseVisualStyleBackColor = true;
            this.bt_stopConn.Click += new System.EventHandler(this.bt_stopConn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(199, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "已连接列表";
            // 
            // cb
            // 
            this.cb.FormattingEnabled = true;
            this.cb.Location = new System.Drawing.Point(270, 16);
            this.cb.Name = "cb";
            this.cb.Size = new System.Drawing.Size(121, 20);
            this.cb.TabIndex = 7;
            this.cb.SelectedIndexChanged += new System.EventHandler(this.cb_SelectedIndexChanged);
            this.cb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cb_MouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.maxclientn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cb);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.bt_recv_conn);
            this.groupBox1.Controls.Add(this.bt_stopConn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lb_conn_num);
            this.groupBox1.Location = new System.Drawing.Point(0, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(504, 87);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接列表";
            // 
            // maxclientn
            // 
            this.maxclientn.Location = new System.Drawing.Point(101, 15);
            this.maxclientn.Name = "maxclientn";
            this.maxclientn.Size = new System.Drawing.Size(69, 21);
            this.maxclientn.TabIndex = 9;
            this.maxclientn.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem,
            this.关于ToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(513, 25);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "文件";
            // 
            // 关于ToolStripMenuItem1
            // 
            this.关于ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem2,
            this.帮助ToolStripMenuItem});
            this.关于ToolStripMenuItem1.Name = "关于ToolStripMenuItem1";
            this.关于ToolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem1.Text = "关于";
            // 
            // 关于ToolStripMenuItem2
            // 
            this.关于ToolStripMenuItem2.Name = "关于ToolStripMenuItem2";
            this.关于ToolStripMenuItem2.Size = new System.Drawing.Size(100, 22);
            this.关于ToolStripMenuItem2.Text = "关于";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IP,
            this.Node,
            this.Islive,
            this.location});
            this.dataGridView1.Location = new System.Drawing.Point(8, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(496, 155);
            this.dataGridView1.TabIndex = 11;
            // 
            // IP
            // 
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            this.IP.Width = 150;
            // 
            // Node
            // 
            this.Node.HeaderText = "节点名";
            this.Node.Name = "Node";
            // 
            // Islive
            // 
            this.Islive.HeaderText = "活动状态";
            this.Islive.Name = "Islive";
            // 
            // location
            // 
            this.location.HeaderText = "地点";
            this.location.Name = "location";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(0, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(513, 201);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "节点信息";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 334);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "基于ARM平台的多光谱图像采集系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxclientn)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_recv_conn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_conn_num;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_stopConn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown maxclientn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ComboBox cb;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Node;
        private System.Windows.Forms.DataGridViewTextBoxColumn Islive;
        private System.Windows.Forms.DataGridViewTextBoxColumn location;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

