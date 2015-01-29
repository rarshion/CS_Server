namespace CS_Server
{
    partial class controlClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.capture_panel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.vedio_panel = new System.Windows.Forms.Panel();
            this.axMediaPlayer1 = new AxMediaPlayer.AxMediaPlayer();
            this.pb = new System.Windows.Forms.PictureBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonX5 = new DevComponents.DotNetBar.ButtonX();
            this.bt_stop_timecap = new DevComponents.DotNetBar.ButtonX();
            this.bt_start_timecap = new DevComponents.DotNetBar.ButtonX();
            this.bt_stop_video = new DevComponents.DotNetBar.ButtonX();
            this.bt_start_video = new DevComponents.DotNetBar.ButtonX();
            this.bt_photo = new DevComponents.DotNetBar.ButtonX();
            this.filter_trackBar = new System.Windows.Forms.TrackBar();
            this.filter_button = new DevComponents.DotNetBar.ButtonX();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.capture_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.vedio_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMediaPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filter_trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.22892F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.77109F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(664, 476);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel2);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(217, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(444, 470);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "采集的图像数据";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox6, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.capture_panel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.94736F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.05263F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(438, 450);
            this.tableLayoutPanel2.TabIndex = 29;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBox3);
            this.groupBox6.Controls.Add(this.textBox2);
            this.groupBox6.Controls.Add(this.button2);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.button1);
            this.groupBox6.Controls.Add(this.textBox1);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(3, 358);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(432, 89);
            this.groupBox6.TabIndex = 20;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "存储";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(145, 69);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(77, 21);
            this.textBox3.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(10, 68);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "192.168.1.13";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(282, 63);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "设置";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "端口号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "服务器IP:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(278, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "浏览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(64, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(208, 21);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "存储目录：";
            // 
            // capture_panel
            // 
            this.capture_panel.Controls.Add(this.panel1);
            this.capture_panel.Controls.Add(this.groupBox7);
            this.capture_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capture_panel.Location = new System.Drawing.Point(3, 3);
            this.capture_panel.Name = "capture_panel";
            this.capture_panel.Size = new System.Drawing.Size(432, 349);
            this.capture_panel.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.vedio_panel);
            this.panel1.Controls.Add(this.pb);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(432, 313);
            this.panel1.TabIndex = 15;
            // 
            // vedio_panel
            // 
            this.vedio_panel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.vedio_panel.Controls.Add(this.axMediaPlayer1);
            this.vedio_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vedio_panel.Location = new System.Drawing.Point(0, 0);
            this.vedio_panel.Name = "vedio_panel";
            this.vedio_panel.Size = new System.Drawing.Size(432, 313);
            this.vedio_panel.TabIndex = 17;
            // 
            // axMediaPlayer1
            // 
            this.axMediaPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMediaPlayer1.Location = new System.Drawing.Point(0, 0);
            this.axMediaPlayer1.Name = "axMediaPlayer1";
            this.axMediaPlayer1.Size = new System.Drawing.Size(432, 313);
            this.axMediaPlayer1.TabIndex = 14;
            // 
            // pb
            // 
            this.pb.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb.Location = new System.Drawing.Point(0, 0);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(432, 313);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb.TabIndex = 8;
            this.pb.TabStop = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.button6);
            this.groupBox7.Controls.Add(this.button5);
            this.groupBox7.Controls.Add(this.button4);
            this.groupBox7.Controls.Add(this.button3);
            this.groupBox7.Controls.Add(this.button7);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox7.Location = new System.Drawing.Point(0, 313);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(432, 36);
            this.groupBox7.TabIndex = 16;
            this.groupBox7.TabStop = false;
            // 
            // button6
            // 
            this.button6.Image = global::CS_Server.Properties.Resources.smaller;
            this.button6.Location = new System.Drawing.Point(301, 10);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(27, 24);
            this.button6.TabIndex = 13;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Image = global::CS_Server.Properties.Resources.Biger;
            this.button5.Location = new System.Drawing.Point(268, 9);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(27, 24);
            this.button5.TabIndex = 12;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Image = global::CS_Server.Properties.Resources.right;
            this.button4.Location = new System.Drawing.Point(164, 10);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(26, 23);
            this.button4.TabIndex = 11;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Image = global::CS_Server.Properties.Resources.left;
            this.button3.Location = new System.Drawing.Point(132, 10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(26, 23);
            this.button3.TabIndex = 10;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(199, 11);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(63, 21);
            this.button7.TabIndex = 14;
            this.button7.Text = "全屏显示";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.filter_button);
            this.groupBox4.Controls.Add(this.filter_trackBar);
            this.groupBox4.Controls.Add(this.buttonX5);
            this.groupBox4.Controls.Add(this.bt_stop_timecap);
            this.groupBox4.Controls.Add(this.bt_start_timecap);
            this.groupBox4.Controls.Add(this.bt_stop_video);
            this.groupBox4.Controls.Add(this.bt_start_video);
            this.groupBox4.Controls.Add(this.bt_photo);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(208, 470);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "数据采集";
            // 
            // buttonX5
            // 
            this.buttonX5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX5.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonX5.Enabled = false;
            this.buttonX5.Location = new System.Drawing.Point(112, 58);
            this.buttonX5.Name = "buttonX5";
            this.buttonX5.Size = new System.Drawing.Size(92, 34);
            this.buttonX5.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX5.TabIndex = 28;
            this.buttonX5.Text = "终止任务";
            this.buttonX5.Click += new System.EventHandler(this.bt_stop_conn_Click);
            // 
            // bt_stop_timecap
            // 
            this.bt_stop_timecap.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_stop_timecap.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_stop_timecap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_stop_timecap.Enabled = false;
            this.bt_stop_timecap.Location = new System.Drawing.Point(112, 152);
            this.bt_stop_timecap.Name = "bt_stop_timecap";
            this.bt_stop_timecap.Size = new System.Drawing.Size(92, 34);
            this.bt_stop_timecap.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_stop_timecap.TabIndex = 27;
            this.bt_stop_timecap.Text = "结束定时采集";
            this.bt_stop_timecap.Click += new System.EventHandler(this.stop_timecap_Click);
            // 
            // bt_start_timecap
            // 
            this.bt_start_timecap.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_start_timecap.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_start_timecap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_start_timecap.Location = new System.Drawing.Point(1, 151);
            this.bt_start_timecap.Name = "bt_start_timecap";
            this.bt_start_timecap.Size = new System.Drawing.Size(107, 34);
            this.bt_start_timecap.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_start_timecap.TabIndex = 26;
            this.bt_start_timecap.Text = "定时图像采集";
            this.bt_start_timecap.Click += new System.EventHandler(this.time_capture_Click);
            // 
            // bt_stop_video
            // 
            this.bt_stop_video.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_stop_video.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_stop_video.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_stop_video.Enabled = false;
            this.bt_stop_video.Location = new System.Drawing.Point(112, 101);
            this.bt_stop_video.Name = "bt_stop_video";
            this.bt_stop_video.Size = new System.Drawing.Size(92, 34);
            this.bt_stop_video.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_stop_video.TabIndex = 25;
            this.bt_stop_video.Text = "结束视频采集";
            this.bt_stop_video.Click += new System.EventHandler(this.bt_stop_video_Click);
            // 
            // bt_start_video
            // 
            this.bt_start_video.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_start_video.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_start_video.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_start_video.Location = new System.Drawing.Point(6, 101);
            this.bt_start_video.Name = "bt_start_video";
            this.bt_start_video.Size = new System.Drawing.Size(100, 34);
            this.bt_start_video.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_start_video.TabIndex = 24;
            this.bt_start_video.Text = "开始视频采集";
            this.bt_start_video.Click += new System.EventHandler(this.bt_video_Click);
            // 
            // bt_photo
            // 
            this.bt_photo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bt_photo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bt_photo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_photo.Location = new System.Drawing.Point(6, 57);
            this.bt_photo.Name = "bt_photo";
            this.bt_photo.Size = new System.Drawing.Size(100, 34);
            this.bt_photo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bt_photo.TabIndex = 23;
            this.bt_photo.Text = "单次图像采集";
            this.bt_photo.Click += new System.EventHandler(this.bt_photo_Click);
            // 
            // filter_trackBar
            // 
            this.filter_trackBar.LargeChange = 1;
            this.filter_trackBar.Location = new System.Drawing.Point(9, 333);
            this.filter_trackBar.Maximum = 2;
            this.filter_trackBar.Name = "filter_trackBar";
            this.filter_trackBar.Size = new System.Drawing.Size(179, 45);
            this.filter_trackBar.TabIndex = 1;
            this.filter_trackBar.Tag = "0";
            // 
            // filter_button
            // 
            this.filter_button.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.filter_button.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.filter_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.filter_button.Location = new System.Drawing.Point(51, 384);
            this.filter_button.Name = "filter_button";
            this.filter_button.Size = new System.Drawing.Size(92, 34);
            this.filter_button.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.filter_button.TabIndex = 29;
            this.filter_button.Text = "滤光片转换";
            this.filter_button.Click += new System.EventHandler(this.filter_button_Click);
            // 
            // controlClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 476);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "controlClientForm";
            this.Text = "基于ARM平台的多光谱图像采集系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.controlClientForm_FormClosing);
            this.Load += new System.EventHandler(this.controlClientForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.capture_panel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.vedio_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMediaPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filter_trackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
      //  private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
     //   private AxMediaPlayer.AxMediaPlayer axMediaPlayer1;

     //   private AxMediaPlayer.AxMediaPlayer axMediaPlayer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevComponents.DotNetBar.ButtonX buttonX5;
        private DevComponents.DotNetBar.ButtonX bt_stop_timecap;
        private DevComponents.DotNetBar.ButtonX bt_start_timecap;
        private DevComponents.DotNetBar.ButtonX bt_stop_video;
        private DevComponents.DotNetBar.ButtonX bt_start_video;
        private DevComponents.DotNetBar.ButtonX bt_photo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel capture_panel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel vedio_panel;
        private AxMediaPlayer.AxMediaPlayer axMediaPlayer1;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TrackBar filter_trackBar;
        private DevComponents.DotNetBar.ButtonX filter_button;
    }
}