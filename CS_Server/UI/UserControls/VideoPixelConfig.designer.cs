namespace MultiSpel.UserControls
{
    partial class VideoPixelConfig
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label_Saturability = new System.Windows.Forms.Label();
            this.label_Contrast = new System.Windows.Forms.Label();
            this.label_Brightness = new System.Windows.Forms.Label();
            this.slider_Saturability = new DevComponents.DotNetBar.Controls.Slider();
            this.slider_Contrast = new DevComponents.DotNetBar.Controls.Slider();
            this.slider_Brightness = new DevComponents.DotNetBar.Controls.Slider();
            this.slider_Whitebalance = new DevComponents.DotNetBar.Controls.Slider();
            this.label_Whitebalance = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Saturability
            // 
            this.label_Saturability.AutoSize = true;
            this.label_Saturability.BackColor = System.Drawing.Color.Transparent;
            this.label_Saturability.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Saturability.ForeColor = System.Drawing.Color.Black;
            this.label_Saturability.Location = new System.Drawing.Point(177, 136);
            this.label_Saturability.Name = "label_Saturability";
            this.label_Saturability.Size = new System.Drawing.Size(50, 20);
            this.label_Saturability.TabIndex = 9;
            this.label_Saturability.Text = "label1";
            // 
            // label_Contrast
            // 
            this.label_Contrast.AutoSize = true;
            this.label_Contrast.BackColor = System.Drawing.Color.Transparent;
            this.label_Contrast.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Contrast.ForeColor = System.Drawing.Color.Black;
            this.label_Contrast.Location = new System.Drawing.Point(179, 92);
            this.label_Contrast.Name = "label_Contrast";
            this.label_Contrast.Size = new System.Drawing.Size(50, 20);
            this.label_Contrast.TabIndex = 10;
            this.label_Contrast.Text = "label1";
            // 
            // label_Brightness
            // 
            this.label_Brightness.AutoSize = true;
            this.label_Brightness.BackColor = System.Drawing.Color.Transparent;
            this.label_Brightness.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Brightness.ForeColor = System.Drawing.Color.Black;
            this.label_Brightness.Location = new System.Drawing.Point(180, 52);
            this.label_Brightness.Name = "label_Brightness";
            this.label_Brightness.Size = new System.Drawing.Size(50, 20);
            this.label_Brightness.TabIndex = 8;
            this.label_Brightness.Text = "label1";
            // 
            // slider_Saturability
            // 
            this.slider_Saturability.BackColor = System.Drawing.Color.Silver;
            // 
            // 
            // 
            this.slider_Saturability.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider_Saturability.Cursor = System.Windows.Forms.Cursors.Hand;
            this.slider_Saturability.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.slider_Saturability.ForeColor = System.Drawing.Color.Black;
            this.slider_Saturability.LabelWidth = 60;
            this.slider_Saturability.Location = new System.Drawing.Point(2, 128);
            this.slider_Saturability.Maximum = 10;
            this.slider_Saturability.Minimum = 1;
            this.slider_Saturability.Name = "slider_Saturability";
            this.slider_Saturability.Size = new System.Drawing.Size(171, 32);
            this.slider_Saturability.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider_Saturability.TabIndex = 5;
            this.slider_Saturability.Text = "饱和度";
            this.slider_Saturability.TextColor = System.Drawing.Color.Black;
            this.slider_Saturability.Value = 0;
            this.slider_Saturability.ValueChanged += new System.EventHandler(this.Slider_ValueChanged);
            // 
            // slider_Contrast
            // 
            this.slider_Contrast.BackColor = System.Drawing.Color.Silver;
            // 
            // 
            // 
            this.slider_Contrast.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider_Contrast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.slider_Contrast.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.slider_Contrast.ForeColor = System.Drawing.Color.Black;
            this.slider_Contrast.LabelWidth = 60;
            this.slider_Contrast.Location = new System.Drawing.Point(3, 84);
            this.slider_Contrast.Maximum = 10;
            this.slider_Contrast.Minimum = 1;
            this.slider_Contrast.Name = "slider_Contrast";
            this.slider_Contrast.Size = new System.Drawing.Size(171, 35);
            this.slider_Contrast.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider_Contrast.TabIndex = 6;
            this.slider_Contrast.Text = "对比度";
            this.slider_Contrast.TextColor = System.Drawing.Color.Black;
            this.slider_Contrast.Value = 0;
            this.slider_Contrast.ValueChanged += new System.EventHandler(this.Slider_ValueChanged);
            // 
            // slider_Brightness
            // 
            this.slider_Brightness.BackColor = System.Drawing.Color.Silver;
            // 
            // 
            // 
            this.slider_Brightness.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider_Brightness.Cursor = System.Windows.Forms.Cursors.Hand;
            this.slider_Brightness.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.slider_Brightness.ForeColor = System.Drawing.Color.Black;
            this.slider_Brightness.LabelWidth = 60;
            this.slider_Brightness.Location = new System.Drawing.Point(3, 42);
            this.slider_Brightness.Maximum = 10;
            this.slider_Brightness.Minimum = 1;
            this.slider_Brightness.Name = "slider_Brightness";
            this.slider_Brightness.Size = new System.Drawing.Size(173, 33);
            this.slider_Brightness.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider_Brightness.TabIndex = 7;
            this.slider_Brightness.Text = "亮度";
            this.slider_Brightness.TextColor = System.Drawing.Color.Black;
            this.slider_Brightness.Value = 0;
            this.slider_Brightness.ValueChanged += new System.EventHandler(this.Slider_ValueChanged);
            // 
            // slider_Whitebalance
            // 
            this.slider_Whitebalance.BackColor = System.Drawing.Color.Silver;
            // 
            // 
            // 
            this.slider_Whitebalance.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.slider_Whitebalance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.slider_Whitebalance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.slider_Whitebalance.ForeColor = System.Drawing.Color.Black;
            this.slider_Whitebalance.LabelWidth = 60;
            this.slider_Whitebalance.Location = new System.Drawing.Point(3, 3);
            this.slider_Whitebalance.Maximum = 5;
            this.slider_Whitebalance.Minimum = 1;
            this.slider_Whitebalance.Name = "slider_Whitebalance";
            this.slider_Whitebalance.Size = new System.Drawing.Size(170, 33);
            this.slider_Whitebalance.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.slider_Whitebalance.TabIndex = 13;
            this.slider_Whitebalance.Text = "白平衡";
            this.slider_Whitebalance.TextColor = System.Drawing.Color.Black;
            this.slider_Whitebalance.Value = 0;
            // 
            // label_Whitebalance
            // 
            this.label_Whitebalance.AutoSize = true;
            this.label_Whitebalance.BackColor = System.Drawing.Color.Transparent;
            this.label_Whitebalance.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Whitebalance.ForeColor = System.Drawing.Color.Black;
            this.label_Whitebalance.Location = new System.Drawing.Point(177, 16);
            this.label_Whitebalance.Name = "label_Whitebalance";
            this.label_Whitebalance.Size = new System.Drawing.Size(50, 20);
            this.label_Whitebalance.TabIndex = 14;
            this.label_Whitebalance.Text = "label1";
            // 
            // VideoTeachImageConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_Whitebalance);
            this.Controls.Add(this.slider_Whitebalance);
            this.Controls.Add(this.label_Saturability);
            this.Controls.Add(this.label_Contrast);
            this.Controls.Add(this.label_Brightness);
            this.Controls.Add(this.slider_Saturability);
            this.Controls.Add(this.slider_Contrast);
            this.Controls.Add(this.slider_Brightness);
            this.Name = "VideoTeachImageConfig";
            this.Size = new System.Drawing.Size(230, 171);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Saturability;
        private System.Windows.Forms.Label label_Contrast;
        private System.Windows.Forms.Label label_Brightness;
        private DevComponents.DotNetBar.Controls.Slider slider_Saturability;
        private DevComponents.DotNetBar.Controls.Slider slider_Contrast;
        private DevComponents.DotNetBar.Controls.Slider slider_Brightness;
        private DevComponents.DotNetBar.Controls.Slider slider_Whitebalance;
        private System.Windows.Forms.Label label_Whitebalance;

    }
}
