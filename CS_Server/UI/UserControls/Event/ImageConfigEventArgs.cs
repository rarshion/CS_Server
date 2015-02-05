using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiSpel.UserControls.Event
{
    public delegate void ImageConfigEventHandler(object sender, ImageConfigEventArgs e);
    public class ImageConfigEventArgs : EventArgs
    {
        public int Whitebalance { get; set; }
        public int Brightness { get; set; }//亮度
        public int Contrast { get; set; }//对比度
        public int Saturability { get; set; }//饱和度

        public ImageConfigEventArgs(int  whitebalance,int brightness, int contrast, int saturability)
        {
            Whitebalance = whitebalance;
            Brightness = brightness;
            Contrast = contrast;
            Saturability = saturability;
        }
    }
}
