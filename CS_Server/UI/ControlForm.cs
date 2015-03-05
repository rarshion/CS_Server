using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

using MultiSpel.Net;
using MultiSpel.UserControls.Event;
using DevComponents.DotNetBar;
using MultiSpel.DataBaseModule.Model;
using MultiSpel.DataBaseModule.BLL;

namespace MultiSpel
{

    public partial class ControlForm : Form
    {
        private CommunicateToClient communicateArmClient;
        private CommunicateToClient communicateToClient;
        private ClientPoint m_clientPoint;
        private ArmClient armClient;
        private NodeData nodeData;

        private const int HEARTTIMEOUT = 5;

        private string videoSavePath = System.Windows.Forms.Application.StartupPath;
        private string imageSavePath = System.Windows.Forms.Application.StartupPath;

        public int Resolution { get; set; } //像素大小 1-6
        public int Whitebalance { get; set; } //白平衡
        public int Saturation { get; set; } //饱和度
        public int Bright { get; set; } //亮度
        public int Contrast { get; set; } //对比度
        public int Quanlity { get; set; } //质量
        public int capturemod { get; set; }//拍摄模式:0.单次抓拍 1.定时拍摄
        public int caphour { get; set; } //定时拍摄中的时间间隔小时
        public int capmin { get; set; }  //定时拍摄中的时间间隔分钟

        private bool isImageCpature = false;//判断是否在图像采集
        private bool isVideoCapture = false;//判断是否在视频采集

        private bool isBusy = false; //判断此刻，是否在进行socket通信
        private bool isVideoing = false; //判断此刻，是否在进程拍照
        private bool isTimePictrue = false; //定时拍摄
        private bool m_isTransformH264 = false; //是否进行从H264到mp4的转换

        private VideoNameBuff m_h264FileBuff = null;
        private VideoNameBuff m_mp4FileBuff = null;

        Bitmap srcBitmap;
        int rot = 0;
        private Point m_StarPoint = Point.Empty;        //for 拖动
        private Point m_ViewPoint = Point.Empty;
        private bool m_StarMove = false;
        Bitmap bmp;
        Point oldpoint;

        public ControlForm()
        {
            InitializeComponent();
            this.capture_panel.Visible = true;
            this.vedio_panel.Visible = false;
        }

        public ControlForm(ArmClient client)
        {
            InitializeComponent();
            armClient = client;
            communicateArmClient = new CommunicateToClient(armClient);
            this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);  //应添加到窗体然后再设置picturebox焦点
            this.Focus();
            this.capture_panel.Visible = false;
            this.vedio_panel.Visible = false;
            videoPixelConfig.ImageConfig +=new ImageConfigEventHandler(VideoPixelConfig);
        }


        public ControlForm(ArmClient client,NodeData node)
        {
            InitializeComponent();
            this.nodeData = node;
            this.armClient = client;
            communicateArmClient = new CommunicateToClient(armClient);
            this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);  //应添加到窗体然后再设置picturebox焦点
            this.Focus();
            this.capture_panel.Visible = false;
            this.vedio_panel.Visible = false;
            videoPixelConfig.ImageConfig += new ImageConfigEventHandler(VideoPixelConfig);
        }



        public ControlForm(ClientPoint cp)
        {
            InitializeComponent();
            m_clientPoint = cp;
            communicateToClient = new CommunicateToClient(cp.client);
            this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);  //应添加到窗体然后再设置picturebox焦点
            this.Focus();
            this.capture_panel.Visible = false;
            this.vedio_panel.Visible = false;
        }

        private void controlClientForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void controlClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isBusy)
            {
                MessageBox.Show("还有任务正在进行中");
                return;
            }
            if (pb.Image != null)
                pb.Image.Dispose();
            armClient.LastAccessTime = DateTime.Now;
            armClient.IsUsing = false;
        }

        #region 获取水分
        private void bt_water_Click(object sender, EventArgs e)
        {
            if (isBusy)
            {
                MessageBox.Show("等待前一个任务完成，再进行下一个任务");
                return;
            }
            if (m_clientPoint.loseConnect)
            {
                MessageBox.Show("连接中断,请取消对该客户端的操作");
                return;
            }
            isBusy = true;
            ThreadPool.QueueUserWorkItem(new WaitCallback(watering), null);
        }


        private void watering(object o)
        {
            String val = null;
            try
            {
                val = communicateToClient.getWater();
            }
            catch (SocketException ex)
            {
                DealwithSocketException dealEx = new DealwithSocketException(ex);
                MessageBox.Show(dealEx.errorMessage);
                if (ex.SocketErrorCode == SocketError.ConnectionAborted)
                    m_clientPoint.loseConnect = true; //和客户端失去了联系
                return;
            }
            finally
            {
                isBusy = false;
            }

          //  lb_water.Text = "水分值为: " + val;

        }
        #endregion 获取水分

        #region 获取温度
        private void bt_temp_Click(object sender, EventArgs e)
        {
            if (isBusy)
            {
                MessageBox.Show("等待前一个任务完成，再进行下一个任务");
                return;
            }

            if (m_clientPoint.loseConnect)
            {
                MessageBox.Show("连接中断,请取消对该客户端的操作");
                return;
            }

            isBusy = true;

            ThreadPool.QueueUserWorkItem(new WaitCallback(temping), null);
        }


        private void temping(object o)
        {
            String val = null;
            try
            {
                val = communicateToClient.getTemp(6); //设置一个60秒的最长等待时间
            }
            catch (SocketException ex)
            {
                DealwithSocketException dealEx = new DealwithSocketException(ex);
                MessageBox.Show(dealEx.errorMessage);

                if( ex.SocketErrorCode == SocketError.ConnectionAborted )
                    m_clientPoint.loseConnect = true; //和客户端失去了联系

                return;
            }
            finally
            {
            //    m_clientPoint.loseConnect = true; //和客户端失去了联系
                isBusy = false;
            }

          //  lb_temp.Text = "温度值为: " + val;
        }
        #endregion 获取温度

        #region 获取图片
        private void bt_photo_Click(object sender, EventArgs e)
        {
            //if (m_isBusy)
            //{
            //    MessageBox.Show("等待前一个任务完成，再进行下一个任务");
            //    return;
            //}

            //if (m_clientPoint.loseConnect)
            //{
            //    MessageBox.Show("连接中断,请取消对该客户端的操作");
            //    return;
            //}

            capturemod = 0;
            this.capture_panel.Visible = true;
            this.vedio_panel.Visible = false;

            photoAttribute photo = new photoAttribute(this, capturemod); //获取设定的照片属性
            if (photo.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return; 
            //m_isBusy = true;
            ThreadPool.QueueUserWorkItem(new WaitCallback(Photoing), null);
        }

        private void Photoing(object o)
        {
            DateTime dt = DateTime.Now;

            int[] pictureAttribute = new int[6];
            pictureAttribute[0] = Resolution;
            pictureAttribute[1] = Whitebalance;
            pictureAttribute[2] = Bright;
            pictureAttribute[3] = Contrast;
            pictureAttribute[4] = Saturation;
            pictureAttribute[5] = Quanlity;

            string savePath = imageSavePath + "\\capture\\";
             string fileName = dt.Year.ToString() + "-";
            fileName += dt.Month.ToString() + "-";
            fileName += dt.Day.ToString() + " _";
            fileName += dt.Hour.ToString() + "-";
            fileName += dt.Minute.ToString() + "-";
            fileName += dt.Second.ToString() + ".jpg";
            string fileFullPath = savePath + fileName;

            ThreadPool.QueueUserWorkItem(new WaitCallback(OperateImageCapture), pictureAttribute);
            bool flag = false;
            try
            {
                flag = communicateArmClient.GetPicture(fileFullPath, pictureAttribute);
            }
            catch (SocketException ex)
            {
                DealwithSocketException dealEx = new DealwithSocketException(ex);
                MessageBox.Show(dealEx.errorMessage);
                if (ex.SocketErrorCode == SocketError.ConnectionAborted)
                    armClient.IsLoseConnect = true;
                return;
            }
            finally
            {
                isBusy = false;
            }

            if (flag)
            {
                if (pb.Image != null) //先清除上一次的图片
                    pb.Image.Dispose();
                pb.Image = Image.FromFile(fileFullPath);
                if (fileFullPath != null)
                {
                    //Show(fileFullPath);
                    //srcBitmap = (Bitmap)Bitmap.FromFile(fileFullPath, true);
                    ImageBLL imageBll = new ImageBLL();
                    ImageData imageData = new ImageData();
                    imageData.nodeid = nodeData.id;
                    imageData.datetime = DateTime.Now;
                    imageData.path = savePath;
                    imageData.fullpath = fileFullPath;
                    imageData.fileName = fileName;
                    imageData.status = 1;
                    if (imageBll.Insert(imageData))
                        ShowControlWarnInMessageBox(true, "插入图像记录成功");
                    else
                        ShowControlWarnInMessageBox(false, "插入图像记录失败");
                }
            }
        }

        private void OperateImageCapture(object o)
        {
            int[] photoAtr = (int[])o;
            int value = -1;
            string errno = String.Empty;
            bool state = true;
            int[] config = new int[6];
            for (int i = 0; i < photoAtr.Length; i++)
            {
                config[i] = photoAtr[i];
            }
            communicateArmClient.Operate(OPERATE.MODIFY_STATE, DEVICE.CAMERA, ref state, ref value, ref config, ref errno);
        }
        #endregion 获取图片

        #region 视频操作
        private void bt_video_Click(object sender, EventArgs e)
        {
            //if( m_isBusy )
            //{
            //    MessageBox.Show("等待前一个任务完成，再进行下一个任务");
            //    return;
            //}

            //if (m_clientPoint.loseConnect)
            //{
            //    MessageBox.Show("连接中断,请取消对该客户端的操作");
            //    return;
            //}

            bt_stop_video.Enabled = true;
            this.vedio_panel.Visible = true;
            this.capture_panel.Visible = true;

            //vedioAttribute vedio_attr = new vedioAttribute(this); //获取设定的照片属性
            //if (vedio_attr.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
               // return; //取消拍照

            isBusy = true;
            isVideoing = true;
            ThreadPool.QueueUserWorkItem(new WaitCallback(videoing), null);
        }



        private void videoing(object o)
        {
            Console.WriteLine("start videoing\n");
            string fileName;
            bool flag = false;

            int value = -1;
            int[] config = null;
            string errno = String.Empty;
            bool state = true;

            communicateArmClient.Operate(OPERATE.MODIFY_STATE, DEVICE.VEDIO, ref state, ref value, ref config, ref errno);

            //if (m_h264FileBuff == null)
            //    m_h264FileBuff = new VideoNameBuff();
            //m_isTransformH264 = true;
            //ThreadPool.QueueUserWorkItem(new WaitCallback(transFromH264ToMp4), null);


            try
            {
                while (isVideoing)
                {
                    DateTime dt = DateTime.Now;

                    String path = System.Windows.Forms.Application.StartupPath; //获取当前执行文件的文件目录
                    Console.WriteLine(path);

                    fileName = "video\\";
                    fileName += "video_";

                    //fileName += dt.Year.ToString() + "-";
                    //fileName += dt.Month.ToString() + "-";
                    //fileName += dt.Day.ToString() + "_";
                    //fileName += dt.Hour.ToString() + "-";
                    //fileName += dt.Minute.ToString() + "-";
                    //fileName += dt.Second.ToString() + "--";
                    //fileName += dt.Millisecond.ToString() + ".264";

                    fileName += ".264";

                    //m_comToClient 会自动加上10
                    int[] videoAttr = {-10, Resolution, Whitebalance, Bright, Contrast, Saturation };
                    flag = communicateArmClient.getVideo(fileName, videoAttr);

                    //flag = communicateToClient.getVideo(fileName, videoAttr);
                    //flag 为 false时表明接收图片失败。
                    //if (flag)
                    //{
                    //    m_h264FileBuff.pushFileName(fileName);
                    //}
                }

                communicateToClient.stopVideo();
                bt_stop_video.Visible = false; //不可见
            }
            catch (SocketException ex)
            {
                DealwithSocketException dealEx = new DealwithSocketException(ex);
                MessageBox.Show(dealEx.errorMessage);

                if (ex.SocketErrorCode == SocketError.ConnectionAborted)
                    m_clientPoint.loseConnect = true; //和客户端失去了联系

                return;
            }
            finally
            {
                m_isTransformH264 = false;
                m_h264FileBuff.clear(); //清除残余文件
                isBusy = false;
            }
        }


        private void transFromH264ToMp4(object o)
        {
            MessageBox.Show("进入transFromH264ToMp4");
            string h264FileName, mp4FileName, arg;

            if (m_mp4FileBuff == null)
                m_mp4FileBuff = new VideoNameBuff();

            ThreadPool.QueueUserWorkItem(new WaitCallback(controlPlayVideo), null);

            while (m_isTransformH264)
            {
                h264FileName = m_h264FileBuff.popFileName();
                if (h264FileName.Length == 0) //还没有h264文件
                    Thread.Sleep(200);
                else
                {
                    mp4FileName = h264FileName.Replace(".264", ".mp4");
                    arg = "-y -i ";
                    arg += h264FileName + " ";
                    arg += mp4FileName;

                    Utility.ExcuteProcess("ffmpeg.exe", arg, (s, ee) => Console.WriteLine(ee.Data));

                    m_mp4FileBuff.pushFileName(mp4FileName);
                }
            }
        }

        private void controlPlayVideo(object o)
        {
            string fileName;
            int videoLength;
            axMediaPlayer1.ShowTracker = false; //不显示进度条
            while (m_isTransformH264)
            {
                fileName = m_mp4FileBuff.popFileName();
                if (fileName.Length == 0)
                {
                    Thread.Sleep(800);
                    // MessageBox.Show("no mp4");
                    continue;
                }
                axMediaPlayer1.FileName = fileName;
                videoLength = (int)axMediaPlayer1.Duration;
                Thread.Sleep(videoLength * 1000);
            }
        }

        private void bt_stop_video_Click(object sender, EventArgs e)
        {
            isVideoing = false; //停止拍摄视频，停止那个线程

            int value = -1;//这个用于单个属性
            int[] config = null;//这个用于多个属性
            string errno = String.Empty;
            bool state = false;
            communicateArmClient.Operate(OPERATE.MODIFY_STATE, DEVICE.VEDIO, ref state, ref value, ref config, ref errno);
        }

        #region 视频像素调节
        private void VideoPixelConfig(object sender, ImageConfigEventArgs e)
        {
            int value = -1;
            bool state = false;
            string errno = String.Empty;
            int[] config = new int[4];
            config[0] = e.Whitebalance;
            config[1] = e.Brightness;
            config[2] = e.Contrast;
            config[3] = e.Saturability;
            communicateToClient.Operate(OPERATE.ADJUST_PARAM, DEVICE.VEDIO, ref state, ref value, ref config, ref errno);
        }
        #endregion 视频像素调节

        #endregion 视频操作


        private void bt_stop_conn_Click(object sender, EventArgs e)
        {
            m_clientPoint.loseConnect = true;
            m_clientPoint.shutdown(); //断开连接

            this.Close(); //关闭当前窗口
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        #region 滚轮放大缩小
        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            
            this.pb.Focus();
            var t = this.pb.Size;

            t.Width += e.Delta;
            t.Height += e.Delta;
            pb.Size = t;


            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            int v = e.Delta / 100;
            int w, h;
            w = pb.Width;
            h = pb.Height;
            if (v > 0)
            {
                pb.Width *= (v + 1);
                pb.Height *= (v + 1);


                if (pb.Width > panel1.Width || pb.Height > panel1.Height)
                {
                    w = pb.Width - w;
                    h = pb.Height - h;
                    this.pb.Location = new Point(pb.Location.X - w / 2, pb.Location.Y - h / 2);
                }
                else
                {
                    this.pb.Location = oldpoint;
                    pianx = 0;
                    piany = 0;
                }
            }
            else
            {
                pb.Width /= (-v + 1);
                pb.Height /= (-v + 1);


                if (pb.Width > panel1.Width || pb.Height > panel1.Height)
                {
                    w = w - pb.Width;
                    h = h - pb.Height;
                    this.pb.Location = new Point(pb.Location.X + w / 2, pb.Location.Y + h / 2);
                }
                else
                {
                    this.pb.Location = oldpoint;
                    pianx = 0;
                    piany = 0;

                }

            }
        }
        #endregion
        #region 在panel中的picturebox中显示图片
        public void Show(string it)
        {
            Console.WriteLine("into showing\n");
            if (it != null)
            {
                bmp = new Bitmap(it);
                if (bmp.Width <= panel1.Width && bmp.Height <= panel1.Height)
                {
                    pb.Width = bmp.Width;
                    pb.Height = bmp.Height;
                    pb.Location = new Point((panel1.Width - bmp.Width) / 2, (panel1.Height - bmp.Height) / 2);
                }
                else
                {
                    float b1 = (float)bmp.Width / panel1.Width;
                    float b2 = (float)bmp.Height / panel1.Height;
                    float b3 = b1 > b2 ? b1 : b2;
                    pb.Width = (int)Math.Round(bmp.Width / b3);
                    pb.Height = (int)Math.Round(bmp.Height / b3);
                    pb.Location = new Point((panel1.Width - pb.Width) / 2, (panel1.Height - pb.Height) / 2);

                }
                pb.Image = bmp;
                oldpoint = new Point(pb.Location.X, pb.Location.Y);
            }
        }
        #endregion
        #region  拖动
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
         //   Console.WriteLine("mousedown\n");

            Cursor = Cursors.Hand;
            m_StarPoint = e.Location;

        }

        //判断在指定范围内移动
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {


            if (pb.Width > panel1.Width || pb.Height > panel1.Height)
                m_StarMove = true;
            else
                m_StarMove = false;

        }

        int pianx = 0, piany = 0;
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
          //  Console.WriteLine("mouseup\n");


            if (m_StarMove)
            {
                int x, y;
                x = m_StarPoint.X - e.X;
                y = m_StarPoint.Y - e.Y;
                pianx += x;
                piany += y;

                pb.Location = new Point(pb.Location.X - x, pb.Location.Y - y);
            }
            m_StarMove = false;




        }
        #endregion
        #region 图片旋转函数
        /// <summary>
        /// 以逆时针为方向对图像进行旋转
        /// </summary>
        /// <param name="b">位图流</param>
        /// <param name="angle">旋转角度[0,360](前台给的)</param>
        /// <returns></returns>
        public Bitmap Rotate(Bitmap b, int angle)
        {
            angle = angle % 360;            //弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            //原图的宽和高
            int w = b.Width;
            int h = b.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
            //目标位图
            Bitmap dsImage = new Bitmap(W, H);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //计算偏移量
            Point Offset = new Point((W - w) / 2, (H - h) / 2);
            //构造图像显示区域：让图像的中心与窗口的中心点一致
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(360 - angle);
            //恢复图像在水平和垂直方向的平移
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(b, rect);
            //重至绘图的所有变换
            g.ResetTransform();
            g.Save();
            g.Dispose();
            //dsImage.Save("yuancd.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return dsImage;
        }
        #endregion 图片旋转函数
        #region 旋转按钮
        private void button3_Click(object sender, EventArgs e)
        {
            rot = (rot + 90) % 360;

            if (srcBitmap != null)
            {
                this.pb.Image = Rotate(srcBitmap, rot);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rot = (rot - 90) % 360;

            if (srcBitmap != null)
            {
                this.pb.Image = Rotate(srcBitmap, rot);

            }
        }
        #endregion
        #region 放大缩小按钮
        private void button5_Click(object sender, EventArgs e)
        {
            var t = this.pb.Size;

            t.Width += 20;
            t.Height += 20;
            pb.Size = t;


            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            int v = 10 / 100;
            int w, h;
            w = pb.Width;
            h = pb.Height;

            pb.Width /= (-v + 1);
            pb.Height /= (-v + 1);


            if (pb.Width > panel1.Width || pb.Height > panel1.Height)
            {
                w = pb.Width - w;
                h = pb.Height - h;
                this.pb.Location = new Point(pb.Location.X - w / 2, pb.Location.Y - h / 2);
            }
            else
            {
                this.pb.Location = oldpoint;
                pianx = 0;
                piany = 0;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var t = this.pb.Size;

            t.Width -= 20;
            t.Height -= 20;
            pb.Size = t;


            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            int v = 10 / 100;
            int w, h;
            w = pb.Width;
            h = pb.Height;

            pb.Width *= (v + 1);
            pb.Height *= (v + 1);


            if (pb.Width > panel1.Width || pb.Height > panel1.Height)
            {
                w = pb.Width - w;
                h = pb.Height - h;
                this.pb.Location = new Point(pb.Location.X - w / 2, pb.Location.Y - h / 2);
            }
            else
            {
                this.pb.Location = oldpoint;
                pianx = 0;
                piany = 0;
            }

        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            string imageFileName = "";
            OpenFileDialog fileDialog1 = new OpenFileDialog();
            fileDialog1.InitialDirectory = "d://";
            fileDialog1.ShowDialog();

            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                imageFileName = fileDialog1.FileName;
            }
            else
            {
                textBox1.Text = "";
            }

            if (imageFileName != null)
            {
                if(File.Exists(imageFileName))
                    pb.Image = Image.FromFile(imageFileName);
            }
             /*
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择存储文件的路径";

          
          //  dialog.ShowNewFolderButton = false;
        
            if (dialog.ShowDialog() == DialogResult.OK)
            {
               // path = dialog.SelectedPath;
            //    this.textBox1.Text = path;
                //... 
            }
            Console.WriteLine("the button is click");
           
            Console.WriteLine(path);
            */
        }

        //private void button8_Click(object sender, EventArgs e)
        //{
        //    Console.WriteLine("开始转换\n");
        //    ExcuteProcess("ffmpeg.exe", "-y -i 1.264 output.mp4", (s, ee) => Console.WriteLine(ee.Data));
        //    MessageBox.Show("ok");
        //}
       

        /*
         *ExcuteProcess  h264转MP4格式 
         * exe:    一个外部可执行程序mmjpeg
         * agr:    视频属性等一些输入数据 
         * output: 输出视频
        */


        private void timephotoing(object o)
        {
            Console.WriteLine("timephotoing");

            try
            {
                while (isTimePictrue)
                {
                    DateTime dt = DateTime.Now;

                    String path = System.Windows.Forms.Application.StartupPath; //获取当前执行文件的文件目录
                    Console.WriteLine(path);
                    string fileName = path + "\\time_capture\\";


                    fileName += dt.Year.ToString() + "-";
                    fileName += dt.Month.ToString() + "-";
                    fileName += dt.Day.ToString() + " ";

                    //  fileName += "-";
                    fileName += dt.Hour.ToString() + "-";
                    fileName += dt.Minute.ToString() + "-";
                    fileName += dt.Second.ToString() + ".jpg";

                    //照片属性已经获取了。
                    //下面数组的每一个元素的顺序不能乱，要和客户端的一致。

                    int[] pictureAttribute = { -10, caphour, capmin, Resolution, Whitebalance, Bright, Contrast, Saturation, Quanlity };

                    Console.WriteLine("拍摄模式" + capturemod);
                    Console.WriteLine("间隔钟" + caphour);
                    Console.WriteLine("间隔分" + capmin);

                    Console.WriteLine(Resolution);
                    Console.WriteLine(Whitebalance);
                    Console.WriteLine(Bright);
                    Console.WriteLine(Contrast);
                    Console.WriteLine(Saturation);
                    Console.WriteLine(Quanlity);

                    bool flag = false;
                    flag = communicateToClient.gettimePicture(fileName, pictureAttribute);

                    //可能接收图片失败
                    if (flag)
                    {
                        if (pb.Image != null) //先清除上一次的图片
                            pb.Image.Dispose();

                        pb.Image = Image.FromFile(fileName);

                        Console.WriteLine("photoing finish\n");
                        Console.WriteLine(fileName);

                        if (fileName != null)
                        {
                            Show(fileName);
                            srcBitmap = (Bitmap)Bitmap.FromFile(fileName, true);
                        }
                    }
                }

                communicateToClient.stopTimePicture();
                bt_stop_timecap.Visible = false; //不可见
            }
            catch (SocketException ex)
            {
                DealwithSocketException dealEx = new DealwithSocketException(ex);
                MessageBox.Show(dealEx.errorMessage);

                if (ex.SocketErrorCode == SocketError.ConnectionAborted)
                    m_clientPoint.loseConnect = true; //和客户端失去了联系

                return;
            }
            finally
            {
                isBusy = false;
            }
        }


        private void time_capture_Click(object sender, EventArgs e)
        {

            if (isBusy)
            {
                MessageBox.Show("等待前一个任务完成，再进行下一个任务");
                return;
            }

            //if (m_clientPoint.loseConnect)
            //{
            //    MessageBox.Show("连接中断,请取消对该客户端的操作");
            //    return;
            //}

            capturemod = 1;

            this.bt_stop_timecap.Visible = true;
              this.capture_panel.Visible = true;
              this.vedio_panel.Visible = false;

            photoAttribute photo = new photoAttribute(this, capturemod); //获取设定的照片属性

            if (photo.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return; //取消拍照

            isBusy = true;
            isTimePictrue = true;

            ThreadPool.QueueUserWorkItem(new WaitCallback(timephotoing), null);
        }

        private void stop_timecap_Click(object sender, EventArgs e)
        {
            isTimePictrue = false;
        }

        private void filter_button_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(filtering), null);
        }

        private void filtering(object o)
        {
            int value = filter_trackBar.Value;
            int[] config = null;
            string errno = String.Empty;
            bool state = true;
            communicateArmClient.Operate(OPERATE.ADJUST_PARAM, DEVICE.FILTER, ref state, ref value, ref config, ref errno);
        }

        private void test_button_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        #region 测试心跳
        private void heartTest_button_Click(object sender, EventArgs e)
        {
             ThreadPool.QueueUserWorkItem(new WaitCallback(heartChecking), null);
        }

        private void heartChecking(object o)
        {
            if (communicateArmClient.heartCheck(HEARTTIMEOUT))
            {
                ShowControlWarnInMessageBox(true, "宝贝还在呢");
            }
            else
                ShowControlWarnInMessageBox(false, "宝贝失去连接了");
        }
        #endregion 

        #region 显示操作信息
        private void ShowControlWarnInMessageBox(bool controlSuccess, string warnMessage)
        {
            BeginInvoke(new MethodInvoker(delegate()
            {
                if (controlSuccess)
                {
                    MessageBoxEx.Show(this,
                        warnMessage,
                         "操作成功",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Information);
                }
                else
                {
                    MessageBoxEx.Show(this,
                          warnMessage,
                           "操作失败",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                }
            }));
        }
        #endregion 显示操作信息

    }
}
