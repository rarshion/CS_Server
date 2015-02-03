using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace CS_Server
{
    public partial class photoAttribute : Office2007Form
    {
        ControlForm father;
        int capturemode;
        public photoAttribute(ControlForm f,int cMod)
        {       
            father = f;
            capturemode = cMod;
            InitializeComponent();
        }   

        private void photoAttribute_Load(object sender, EventArgs e)
        {
            if (capturemode == 1)
            {
                this.TimeCapture_groupBox.Enabled = true;
            }
            else
            {
                this.TimeCapture_groupBox.Enabled = false;
            }
        }

        private void photoAttribute_FormClosing(object sender, FormClosingEventArgs e)
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

        private bool check_time(String[] str)
        {
            return true;
        }

        private string GetSelectLigFrenItem()
        {
            string s = "";
            foreach (Control c in LightFrequencty_groupBox.Controls)
            {
                if(c.GetType() == typeof(RadioButton))
                {
                    RadioButton r = c as RadioButton;
                    if (r.Checked)
                        return r.Tag.ToString();
                }
            }
            return null;
        }


        private void bt_photo_Click(object sender, EventArgs e)
        {
            string resolution = this.resolutionbar.Value.ToString();
            string whiteBalance = this.whitebalancebar.Value.ToString();
            string saturation = this.saturationbar.Value.ToString();
            string light = this.lightnessbar.Value.ToString();
            string constrast =this.contrastbar.Value.ToString();
            string quanlity = this.quanlitybar.Value.ToString();
            string[] str = { resolution, whiteBalance, light, constrast, saturation, quanlity };

            if (!check(str))
            {
                MessageBox.Show("请输入所有属性");
                return;
            }

            bool ok = false;
            try
            {

                if (father.capturemod == 0)
                {
                    father.caphour = 0;
                    father.capmin = 0;
                }
                else
                {
                    father.caphour = Int32.Parse(this.caphour.Value.ToString());
                    father.capmin = Int32.Parse(this.capmin.Value.ToString());
                }

                father.resolution = Int32.Parse(str[0]);
                father.whiteBalance = Int32.Parse(str[1]);
                father.light = Int32.Parse(str[2]);
                father.constrast = Int32.Parse(str[3]);
                father.saturation = Int32.Parse(str[4]);
                father.quanlity = Int32.Parse(str[5]);
               //father.lightfrequency = Int32.Parse(str[6]);

                ok = true;
            }
            catch
            {
                MessageBox.Show("请输入整数");
            }

            if (ok)
            {
                this.Close(); //两者的顺序不能乱
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.resolutionbar.Value = 1;
            this.whitebalancebar.Value = 1;
            this.lightnessbar.Value = 0;
            this.contrastbar.Value = 0;
            this.saturationbar.Value = 0;
        }

        public void whilebalance_click(object sender, EventArgs e)
        {
           MessageBox.Show("1.自动白平衡;\n2.太阳光模式;\n3.阴天模式;\n4.白日光模式;\n5.日光灯模式;\n6.黑暗环境;\n","白平衡使用说明");
        }

    }
}
