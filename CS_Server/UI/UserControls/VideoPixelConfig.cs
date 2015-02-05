using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using MultiSpel.UserControls.Event;

namespace MultiSpel.UserControls
{
    public partial class VideoPixelConfig : UserControl
    {
        public int Whitebalance { get; set; }//白平衡
        public int Brightness { get; set; }//亮度
        public int Contrast { get; set; }//对比度
        public int Saturability { get; set; }//饱和度

        public event ImageConfigEventHandler ImageConfig;

        public VideoPixelConfig()
        {
            InitializeComponent();
            this.slider_Whitebalance.Value = 5;
            this.slider_Brightness.Value = 5;
            this.slider_Contrast.Value = 5;
            this.slider_Saturability.Value = 5;
            label_Whitebalance.Text = slider_Whitebalance.ToString();
            label_Brightness.Text = slider_Brightness.Value.ToString();
            label_Contrast.Text = slider_Contrast.Value.ToString();
            label_Saturability.Text = slider_Saturability.Value.ToString();
        }

        private void Slider_ValueChanged(object sender, EventArgs e)
        {
            Slider slider = sender as Slider;
            if (slider == null) return;
            switch (slider.Text)
            {
                case "白平衡":
                    label_Whitebalance.Text = slider_Whitebalance.ToString();break;
                case "亮度":
                    label_Brightness.Text = slider.Value.ToString(); break;
                case "对比度":
                    label_Contrast.Text = slider.Value.ToString(); break;
                case "饱和度":
                    label_Saturability.Text = slider.Value.ToString(); break;
            }

            Whitebalance = slider_Whitebalance.Value;
            Brightness = slider_Brightness.Value;
            Contrast = slider_Contrast.Value;
            Saturability = slider_Saturability.Value;

            if (ImageConfig != null)
            {
                ImageConfig(this, new ImageConfigEventArgs(Whitebalance,Brightness, Contrast, Saturability));
            }

        }
    }
}
