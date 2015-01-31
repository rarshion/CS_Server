﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using CS_Server.Net;
using System.Net;

namespace CS_Server
{

    public partial class MainForm : Form
    {

        ServerSocket controlServer = null;
        ServerSocket videoServer = null;
        ServerSocket photoServer = null;
        ServerSocket heartServer = null;

        Thread controlThread = null;
        Thread photoThread = null;
        Thread videoThread = null;
        Thread heartThread = null;

        bool controlAccept = false; //是否开始等待客户端连接
        bool photoAccept = false;
        bool videoAccept = false;
        bool heartAccept = false;

        bool controlListenning = false; //是否正在监听 
        bool photoListenning = false;
        bool videoListenning = false;
        bool heartListenning = false;

        Thread m_checkOnlineThread = null;

        List<ClientPoint> clients = new List<ClientPoint>();
        Dictionary<string, DevicePoint> deviceDictonary = new Dictionary<string, DevicePoint>();
           
        readonly static object _sync = new object(); //用于同步
        readonly static object listSync = new object(); //用于对List容器进行操作时的同步

        readonly static object syncLock = new object();


        public MainForm()
        {
            InitializeComponent();
  
        }
       
        private DialogResult Show(string op)
        {
            return MessageBox.Show(op, "警告!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

         [STAThread]
        private void MainForm_Load(object sender, EventArgs e)
        {
            //保证可以多个线程访问控件
            //不然，在调试的时候会出现异常： 线程间操作无效: 从不是创建控件XXX的线程访问它
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            Console.WriteLine("间隔分");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Show("确认关闭? 这将停止所有连接") != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            else
            {
                stop();
            }
        }

        private void bt_recv_conn_Click(object sender, EventArgs e)
        {
            if (controlAccept)
            {
                MessageBox.Show("已经开始接受客户端连接了");
                return;
            }

            int maxRecvNum;
            string str = this.maxclientn.Value.ToString();
            Console.WriteLine(str);

            if (str == "" || str.Trim() == "")
            {
                MessageBox.Show("请输入最大允许连接数");
                return;
            }

            try
            {
                maxRecvNum = Int32.Parse(str);
            }
            catch
            {
                MessageBox.Show("请输入一个整数");
                return;
            }

            controlServer = new ServerSocket(8989, maxRecvNum);
            controlListenning = true; 
            controlAccept = true;

            videoServer = new ServerSocket(8990, maxRecvNum);
            videoListenning = true;
            videoAccept = true;

            photoServer = new ServerSocket(8993, maxRecvNum);
            photoListenning = true;
            photoAccept = true;

            heartServer = new ServerSocket(8992, maxRecvNum);
            heartListenning = true;
            heartAccept = true;
            

            controlThread = new Thread(startControlAccept);
            controlThread.IsBackground = true;
            controlThread.Start();

            photoThread = new Thread(startPhotoAccept);
            photoThread.IsBackground = true;
            photoThread.Start();

            videoThread = new Thread(startVideoAccept);
            videoThread.IsBackground = true;
            videoThread.Start();

            heartThread = new Thread(startHeartAccept);
            heartThread.IsBackground = true;
            heartThread.Start();


            m_checkOnlineThread = new Thread(onlineCheck);
            m_checkOnlineThread.IsBackground = true;
            m_checkOnlineThread.Start();
        }


        private void startControlAccept()
        {

            while (controlListenning)
            {
                Console.WriteLine("StartControlAccept");
                Socket controlClient = controlServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("控制客户端IP地址:"+controlClient.RemoteEndPoint);
                addClient(controlClient,1); 
            }
        }


        private void startVideoAccept()
        {
            while (videoListenning)
            {
                Console.WriteLine("StartVideoAccept");
                Socket videoClient = videoServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("视频客户端IP地址:" + videoClient.RemoteEndPoint);
                addClient(videoClient,2); //向容器添加一个客户端
            }
        }


        private void startPhotoAccept()
        {
            while (photoListenning)
            {
                Console.WriteLine("StartPhotoAccept");
                Socket photoClient = photoServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("图像客户端IP地址:" + photoClient.RemoteEndPoint);
                addClient(photoClient,3);
            }
        }

        private void startHeartAccept()
        {
            while (heartListenning)
            {
                Console.WriteLine("StartHeartAccept");
                Socket heartClient = heartServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("心跳客户端IP地址:" + heartClient.RemoteEndPoint);
                addClient(heartClient,4);
            }
        }


        public void addClient(Socket client,int flag)
        {
            FindDevicePointByIp(client, flag, ref deviceDictonary);
            if (!HasValDevice())
                return;
            



            ClientPoint one_client = new ClientPoint();
            byte[] localtion = new byte[200];
            TcpPort tempClient = new TcpPort(client);
            tempClient.Receive(localtion); //获取客户端发送来的地址信息
            one_client.client = (Socket)client;
            one_client.localtion = Encoding.ASCII.GetString(localtion);



            //ClientPoint one_client = new ClientPoint();
            //byte[] localtion = new byte[200];
            //ClientSocket tempClient = new ClientSocket(client);
            //tempClient.Receive(localtion); //获取客户端发送来的地址信息
            //one_client.client = (Socket)client;
            //one_client.localtion = Encoding.ASCII.GetString(localtion);


            //byte[] data = Encoding.ASCII.GetBytes("Welcome to server");
            //tempClient.Send(data, Kind.message);
            //one_client.isChecking = false; //不是出于被检查在线 状态
            //one_client.isUsing = false;//还没被使用
            //one_client.lastAccessTime = DateTime.Now;
            //one_client.loseConnect = false; 


            //lock (_sync)
            //{
            //    if (controlListenning) //仍然在监听状态
            //    {
            //        lock (listSync) //套大锁，再套小锁
            //        {
            //            //检查该客户端之前是否已经登录过了。有的话，删除之前的，然后再添加

            //            int i;
            //            for (i = 0; i < clients.Count; ++i)
            //            {
            //                if (one_client.localtion.Equals(clients[i].localtion))
            //                {
            //                    clients.Remove(clients[i]);
            //                    break;
            //                }
            //            }

            //            clients.Add(one_client); //把连接好的客户端放到容器里面
            //            ShowClientsinGrid(one_client);
            //            lb_conn_num.Text = "" + clients.Count;
            //        }
            //    }
            //}
        }

        public bool FindDevicePointByIp(Socket client, int flag, ref Dictionary<string, DevicePoint> deviceDictonary)
        {
            IPEndPoint remoteEndPoint = (IPEndPoint)client.RemoteEndPoint;
            IPAddress   ipaddress = IPAddress.Parse(remoteEndPoint.Address.ToString());
            string ip = ipaddress.ToString();

            lock (syncLock)
            {
                DevicePoint device = null;
                if (!deviceDictonary.TryGetValue(ip, out device))
                {
                    device = new DevicePoint(ip);
                    deviceDictonary.Add(ip, device);
                }
                if (flag == 1)
                    device.ControlSocket = client;
                else if (flag == 2)
                    device.VideoSocket = client;
                else if (flag == 3)
                    device.PhotoSocket = client;
                else
                    device.HeartSocket = client;
            }
            return true;
        }

        public bool HasValDevice()
        {
            lock (listSync)
            {
                foreach (var item in deviceDictonary)
                {
                    Console.WriteLine(item.Key);
                    DevicePoint dev = item.Value;
                    if (IsValDevice(dev))
                    {
                        Console.WriteLine("这是一个有效的设备");
                        return true;
                    }
                }
            }
            return false;
        }


        public bool IsValDevice(DevicePoint device)
        {
            if (device == null)
                return false;

            if (device.ControlSocket != null && device.VideoSocket != null
                && device.PhotoSocket != null && device.HeartSocket != null)
            {
                Console.WriteLine("全都有了");
                return true;
            }
            else
                return false;
        }

        public  bool AddSocketClientToDevice(Socket client,int flag,ref DevicePoint device)
        {


            return true;
        }



        public void ShowClientsinGrid(ClientPoint one_client)
        {
            Console.WriteLine("write into gridview\n");
            int index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].Cells[0].Value = one_client.client.RemoteEndPoint;
            this.dataGridView1.Rows[index].Cells[1].Value = one_client.localtion.ToString();
            this.dataGridView1.Rows[index].Cells[2].Value = "监听";
            this.dataGridView1.Rows[index].Cells[3].Value = "信息学院640";
        }

        private void bt_stopConn_Click(object sender, EventArgs e)
        {
            if (!controlAccept) //还没有开始连接
            {
                MessageBox.Show("还没开始连接");
                return;
            }

            controlAccept = false;
            stop();

            lb_conn_num.Text = "0";
        }


        private void stop()
        {
            if (!controlAccept) //还没开始连接
                return;

            if (!videoAccept)
                return;

            if (!photoAccept)
                return;

            if (!heartAccept)
                return;

            lock (_sync)
            {
                controlThread.Abort(); //强制终止线程
                videoThread.Abort();
                photoThread.Abort();
                heartThread.Abort();

                controlListenning = false; //停止监听
                videoListenning = false;
                photoListenning = false;
                heartListenning = false;

                m_checkOnlineThread.Abort();
            }

            lock (listSync)
            {
                disConn();
            }
        }

        /// <summary>
        /// 断开所有的连接.
        /// </summary>
        private void disConn()
        {
            foreach (ClientPoint client in clients)
            {
                client.client.Close();
            }

            clients.Clear();

            if (controlServer != null)
            {
                controlServer.Close();
                controlServer = null;
            }

            if (photoServer != null)
            {
                photoServer.Close();
                photoServer = null;
            }

            if (videoServer != null)
            {
                videoServer.Close();
                videoServer = null;
            }
        }


        /// <summary>
        /// 对每一个客户端进行检测，看其是否还在线。该方法由主线程开辟一个线程单独运行
        /// </summary>
        private void onlineCheck()
        {
            int clientNum;
            while (true)
            {
                lock (listSync)
                {
                    clientNum = clients.Count;
                }

                if (clientNum == 0)
                {
                    Thread.Sleep(10 * 60 * 1000); //休眠10分钟
                    continue;
                }

                int i;
                for (i = 0; i < clientNum; ++i)
                {
                    lock( listSync )
                    {
                        if (clients[i].isUsing || clients[i].loseConnect)
                            continue;

                        clients[i].isChecking = true;
                    }

                    if( isOnline(clients[i] ) )
                    {
                        clients[i].isChecking = false;
                    }
                    else   //已经掉线了
                    {
                        //删除该客户端
                        lock (listSync)
                        {
                            clients.Remove(clients[i]);
                        }
                        --clientNum; //客户端个数减一
                    }
                }
            }
        }


        private bool isOnline(ClientPoint point)
        {
            CommunicateToClient client = new CommunicateToClient(point.client);
            return client.heartCheck(5 * 60); //等待时间为5分钟
        }


        /// <summary>
        /// 每选择一个客户端就新建一个线程并新建一个窗体，在新窗体里面控制客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [STAThread]
        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {   
            int index = cb.SelectedIndex;

            string local = (string)cb.Items[index];
            int i = 0;

            ClientPoint client = null;
            lock (listSync)
            {
                for (i= 0; i < clients.Count; ++i)
                {
                    if (local.Equals(clients[i].localtion))
                    {
                        break;
                    }
                }

                if (i >= clients.Count)
                    return;

               client = clients[i];
               client.isUsing = true;

            }

            if (client.loseConnect)
            {
                MessageBox.Show("服务器已经和该客户端失去连接");
                client.isUsing = false;
                return;
            }

            Thread thread = new Thread(controlClient);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(client);
            


            /*
            int index = cb.SelectedIndex;

            if (index < 0 || index >= clients.Count) //越界
                return;

            Thread thread = new Thread(controlClient);

            //不能使用下标的形式。因为有些节点可能正在处于进行在线检查。
            thread.Start(clients[index]);
             */
        }

        private void controlClient(Object client)
        {
            ClientPoint clientPort = (ClientPoint)client;
            controlClientForm form = new controlClientForm(clientPort);
            Application.Run(form);

            if (clientPort.loseConnect) //已经和客户端失去了连接
            {
                clients.Remove(clientPort);
                lb_conn_num.Text = "" + clients.Count; //更新连接数
            }
        }


        private void cb_MouseClick(object sender, MouseEventArgs e)
        {
            cb.Items.Clear(); //先清空之前的下拉数据

            lock (listSync)
            {
                foreach (ClientPoint client in clients)
                {
                    //没有处于被检查状态，才可以看得见。因为处于被检查时，并不能对其进行操作
                    //另外失去了联系的话，也是不是看见
                    //为了防止不小心进行了操作，干脆就不显示其
                    if( !client.isChecking)
                        cb.Items.Add(client.localtion);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            controlClientForm form = new controlClientForm();
            form.Show();
        }
    }

    //要用类，不能用结构体，因为C#对结构体的=运算 是进行赋值操作，而不是赋予引用
    public class ClientPoint
    {
        public Socket client;
        public string localtion; //客户端所在的位置

        //每半个小时就对客户端进行在线检查。如果客户端在半个小时内被访问过，就不检查
        public bool isChecking; //标识现在是否对该客户端进行在线检查
        public bool isUsing; //标识工作人员正在对该客户端进行操作
        public DateTime lastAccessTime; //上一次操作该客户端的时间

        public bool loseConnect; //失去和客户端的连接。这个属性一般是在监控客户端时发现，并设置的

        public void shutdown()
        {
            client.Shutdown(SocketShutdown.Both);
        }
    };
}