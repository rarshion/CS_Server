using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CS_Server
{
    public partial class vedioAttribute : Form
    {
        ControlForm father;

        public vedioAttribute(ControlForm f)
        {
            father = f;
            InitializeComponent();
        }


        private void vedioAttribute_Load(object sender, EventArgs e)
        {

        }

        private void vedioAttribute_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private bool check(string[] str)
        {
            for (int i = 0; i < str.Length; ++i)
            {
                if (str[i] == "")
                    return false;

                str[i] = str[i].Trim();
                if (str[i] == "")
                    return false;
            }
            return true;
        }

        private void bt_ok_Click(object sender, EventArgs e)
        {

            //视频图像属性
            string resolution = this.resolutionbar.Value.ToString();
            string whiteBalance = this.whitebalancebar.Value.ToString();
            string saturation = this.saturationbar.Value.ToString();
            string light = this.lightnessbar.Value.ToString();
            string constrast = this.contrastbar.Value.ToString();
            string[] str = { resolution, whiteBalance, light, constrast, saturation };

            if (!check(str))
            {
                MessageBox.Show("请输入所有属性");
                return;
            }

            bool ok = false;
            try
            {
                father.resolution = Int32.Parse(str[0]);
                father.whiteBalance = Int32.Parse(str[1]);
                father.light = Int32.Parse(str[2]);
                father.constrast = Int32.Parse(str[3]);
                father.saturation = Int32.Parse(str[4]);
            
                /*
                Console.WriteLine("vedio attr" + father.resolution);
                Console.WriteLine("vedio attr" + father.whiteBalance);
                Console.WriteLine("vedio attr" + father.light);
                Console.WriteLine("vedio attr" + father.constrast);
                Console.WriteLine("vedio attr" + father.saturation);
                  */
  
                ok = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("请输入整数");

            }

            if (ok)
            {
                this.Close(); //两者的顺序不能乱
                this.DialogResult = DialogResult.OK;
            }



        }

        private void vedioAttribute_Load_1(object sender, EventArgs e)
        {
           
        }



        

    }
}
