namespace CS_Server
{
    partial class vedioAttribute
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_clear = new System.Windows.Forms.Button();
            this.bt_ok = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.saturationbar = new System.Windows.Forms.TrackBar();
            this.contrastbar = new System.Windows.Forms.TrackBar();
            this.lightnessbar = new System.Windows.Forms.TrackBar();
            this.whitebalancebar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.resolutionbar = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saturationbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightnessbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.whitebalancebar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resolutionbar)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_clear);
            this.groupBox1.Controls.Add(this.bt_ok);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.resolutionbar);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 287);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // bt_clear
            // 
            this.bt_clear.Location = new System.Drawing.Point(229, 60);
            this.bt_clear.Name = "bt_clear";
            this.bt_clear.Size = new System.Drawing.Size(75, 23);
            this.bt_clear.TabIndex = 39;
            this.bt_clear.Text = "重置";
            this.bt_clear.UseVisualStyleBackColor = true;
            // 
            // bt_ok
            // 
            this.bt_ok.Location = new System.Drawing.Point(229, 23);
            this.bt_ok.Name = "bt_ok";
            this.bt_ok.Size = new System.Drawing.Size(75, 23);
            this.bt_ok.TabIndex = 38;
            this.bt_ok.Text = "确定";
            this.bt_ok.UseVisualStyleBackColor = true;
            this.bt_ok.Click += new System.EventHandler(this.bt_ok_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Location = new System.Drawing.Point(147, 14);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(76, 77);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "白天夜间";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 27);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 30;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "夜间";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(12, 53);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "白天";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.saturationbar);
            this.groupBox2.Controls.Add(this.contrastbar);
            this.groupBox2.Controls.Add(this.lightnessbar);
            this.groupBox2.Controls.Add(this.whitebalancebar);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Location = new System.Drawing.Point(0, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 192);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "色彩效果";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(192, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 46;
            this.label9.Text = "(-4 -0- +4)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(201, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 45;
            this.label7.Text = "(1 - 6)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(193, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 44;
            this.label8.Text = "(-4 -0- +4)";
            // 
            // saturationbar
            // 
            this.saturationbar.LargeChange = 1;
            this.saturationbar.Location = new System.Drawing.Point(50, 153);
            this.saturationbar.Maximum = 4;
            this.saturationbar.Minimum = -4;
            this.saturationbar.Name = "saturationbar";
            this.saturationbar.Size = new System.Drawing.Size(145, 45);
            this.saturationbar.TabIndex = 37;
            this.saturationbar.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // contrastbar
            // 
            this.contrastbar.LargeChange = 1;
            this.contrastbar.Location = new System.Drawing.Point(50, 101);
            this.contrastbar.Maximum = 4;
            this.contrastbar.Minimum = -4;
            this.contrastbar.Name = "contrastbar";
            this.contrastbar.Size = new System.Drawing.Size(145, 45);
            this.contrastbar.TabIndex = 38;
            this.contrastbar.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // lightnessbar
            // 
            this.lightnessbar.LargeChange = 1;
            this.lightnessbar.Location = new System.Drawing.Point(52, 58);
            this.lightnessbar.Maximum = 4;
            this.lightnessbar.Minimum = -4;
            this.lightnessbar.Name = "lightnessbar";
            this.lightnessbar.Size = new System.Drawing.Size(143, 45);
            this.lightnessbar.TabIndex = 39;
            this.lightnessbar.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // whitebalancebar
            // 
            this.whitebalancebar.Location = new System.Drawing.Point(52, 14);
            this.whitebalancebar.Maximum = 6;
            this.whitebalancebar.Minimum = 1;
            this.whitebalancebar.Name = "whitebalancebar";
            this.whitebalancebar.Size = new System.Drawing.Size(143, 45);
            this.whitebalancebar.TabIndex = 40;
            this.whitebalancebar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.whitebalancebar.Value = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 41;
            this.label2.Text = "饱和度";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 43;
            this.label4.Text = "对比度";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 42;
            this.label3.Text = "亮度";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 36;
            this.label1.Text = "白平衡";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(192, 71);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(71, 12);
            this.label19.TabIndex = 47;
            this.label19.Text = "(-4 -0- +4)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(88, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 12);
            this.label15.TabIndex = 29;
            this.label15.Text = "640*480";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(88, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 12);
            this.label14.TabIndex = 28;
            this.label14.Text = "800*600";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(88, 70);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 27;
            this.label13.Text = "1280*960";
            // 
            // resolutionbar
            // 
            this.resolutionbar.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.resolutionbar.LargeChange = 1;
            this.resolutionbar.Location = new System.Drawing.Point(52, 13);
            this.resolutionbar.Maximum = 3;
            this.resolutionbar.Minimum = 1;
            this.resolutionbar.Name = "resolutionbar";
            this.resolutionbar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.resolutionbar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.resolutionbar.Size = new System.Drawing.Size(45, 78);
            this.resolutionbar.TabIndex = 25;
            this.resolutionbar.Value = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "分辨率";
            // 
            // vedioAttribute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 311);
            this.Controls.Add(this.groupBox1);
            this.Name = "vedioAttribute";
            this.Text = "视频图像属性";
            this.Load += new System.EventHandler(this.vedioAttribute_Load_1);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.saturationbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightnessbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.whitebalancebar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resolutionbar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TrackBar resolutionbar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar saturationbar;
        private System.Windows.Forms.TrackBar contrastbar;
        private System.Windows.Forms.TrackBar lightnessbar;
        private System.Windows.Forms.TrackBar whitebalancebar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button bt_clear;
        private System.Windows.Forms.Button bt_ok;
    }
}