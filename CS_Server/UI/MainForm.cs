using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using MultiSpel.Net;
using System.Net;

namespace MultiSpel
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
        Dictionary<string, ArmClient> armClientDictionary = new Dictionary<string, ArmClient>();
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

        #region 开始接收连接
        private void bt_recv_conn_Click(object sender, EventArgs e)
        {
            if (controlAccept)
            {
                MessageBox.Show("已经开始接受客户端连接了");
                return;
            }

            int maxRecvNum;
            string str = this.maxclientn.Value.ToString();
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

            controlServer = new ServerSocket(8888, maxRecvNum);
            controlListenning = true; 
            controlAccept = true;

            videoServer = new ServerSocket(8889, maxRecvNum);
            videoListenning = true;
            videoAccept = true;

            photoServer = new ServerSocket(8890, maxRecvNum);
            photoListenning = true;
            photoAccept = true;

            heartServer = new ServerSocket(8891, maxRecvNum);
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
        #endregion 开始接收连接

        #region 接受客户端连接
        private void startControlAccept()
        {
            while (controlListenning)
            {
                Console.WriteLine("StartControlAccept");
                Socket controlClient = controlServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("控制客户端IP地址:"+controlClient.RemoteEndPoint);
                AddSocket(controlClient,1); 
            }
        }


        private void startVideoAccept()
        {
            while (videoListenning)
            {
                Console.WriteLine("StartVideoAccept");
                Socket videoClient = videoServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("视频客户端IP地址:" + videoClient.RemoteEndPoint);
                AddSocket(videoClient,2); //向容器添加一个客户端
            }
        }


        private void startPhotoAccept()
        {
            while (photoListenning)
            {
                Console.WriteLine("StartPhotoAccept");
                Socket photoClient = photoServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("图像客户端IP地址:" + photoClient.RemoteEndPoint);
                AddSocket(photoClient,3);
            }
        }

        private void startHeartAccept()
        {
            while (heartListenning)
            {
                Console.WriteLine("StartHeartAccept");
                Socket heartClient = heartServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("心跳客户端IP地址:" + heartClient.RemoteEndPoint);
                AddSocket(heartClient,4);
            }
        }
        #endregion 接受客户端连接

        #region 添加客户端连接
        /// <summary>
        /// 添加Socket
        /// </summary>
        /// <param name="socket">接收到的Socket</param>
        /// <param name="flag">该Socket类型</param>
        public void AddSocket(Socket socket,int flag)
        {
            lock (_sync)
            {
                ArmClient armClient = null;
                TcpPort tempPort = new TcpPort(socket);
                InstallClientPort(tempPort, flag, ref armClientDictionary);

                if (!(HasValDevice(out armClient)))
                    return;

                if (armClient != null)
                {
                    Console.WriteLine("ARM客户端开始接收数据");
                    TcpPort comPort = armClient.ControlPort;
                    byte[] location = new byte[200];
                    comPort.Receive(location);
                    armClient.Location = Encoding.ASCII.GetString(location);
                    Console.WriteLine("ARM客户端地址为:" + armClient.Location);


                    byte[] data = Encoding.ASCII.GetBytes("Welcome to server");
                    comPort.Send(data, Kind.message);
                    armClient.IsChecking = false;
                    armClient.IsUsing = false;
                    armClient.LastAccessTime = DateTime.Now;
                    armClient.IsLoseConnect = false;
                }
            }

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
        #endregion 添加客户端连接

        #region 添加端口
        /// <summary>
        /// 添加客户端Tcp端口
        /// </summary>
        /// <param name="port">tcp端口</param>
        /// <param name="flag">端口标志<1.控制 2.视频 3.图像 4.心跳></param>
        /// <param name="deviceDictonary">客户端字典</param>
        /// <returns></returns>
        public bool InstallClientPort(TcpPort port, int flag, ref Dictionary<string, ArmClient> deviceDictonary)
        {
            IPEndPoint remoteEndPoint = (IPEndPoint)port.PortSocket.RemoteEndPoint;
            IPAddress   ipaddress = IPAddress.Parse(remoteEndPoint.Address.ToString());
            string ip = ipaddress.ToString();

            lock (syncLock)
            {
                ArmClient device = null;
                if (!deviceDictonary.TryGetValue(ip, out device))
                {
                    device = new ArmClient(ip);
                    deviceDictonary.Add(ip, device);
                }
                if (flag == 1)
                    device.ControlPort = port;
                else if (flag == 2)
                    device.VideoPort = port;
                else if (flag == 3)
                    device.PhotoPort = port;
                else
                    device.HeartPort = port;
            }
            return true;
        }
        #endregion 添加端口

        #region 查找合法的客户端
        /// <summary>
        /// 在字典中寻找一个合法的客户端
        /// </summary>
        /// <param name="armClient"></param>
        /// <returns></returns>
        public bool HasValDevice(out ArmClient armClient)
        {
            lock (listSync)
            {
                foreach (var item in armClientDictionary)
                {
                    Console.WriteLine(item.Key);
                    ArmClient dev = item.Value;
                    if (IsValDevice(dev))
                    {
                        Console.WriteLine("找到一个有效的设备");
                        armClient =dev;
                        return true;
                    }
                }
            }
            armClient = null;
            return false;
        }
        #endregion 查找合法的客户端

        #region 检查客户端合法
        /// <summary>
        /// 判断该客户端是否为合法（具有四个端口)
        /// </summary>
        /// <param name="device">客户端</param>
        /// <returns>armClient</returns>
        public bool IsValDevice(ArmClient device)
        {
            lock (syncLock)
            {
                if (device == null)
                    return false;

                if (device.ControlPort != null && device.VideoPort != null
                    && device.PhotoPort != null && device.HeartPort != null)
                {
                    Console.WriteLine("端口齐全");
                    return true;
                }
                else
                    return false;
            }
        }
        #endregion  检查客户端合法

        #region 添加客户端


        #endregion 添加客户端

        #region 移除客户端

        #endregion 移除客户端


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
            //if (!controlAccept) //还没开始连接
            //    return;

            //if (!videoAccept)
            //    return;

            //if (!photoAccept)
            //    return;

            //if (!heartAccept)
            //    return;

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
                DisConnect();
            }
        }

        /// <summary>
        /// 断开所有的连接.
        /// </summary>
        private void DisConnect()
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
            bool flag = false;

            ArmClient armClient = null;
            lock (listSync)
            {
                foreach (var item in armClientDictionary)
                {
                    ArmClient client = item.Value;
                    if (local.Equals(client.Location))
                    {
                        client.IsUsing = true;
                        armClient = client;
                        flag = true;
                        break;
                    }
                }
            }

            if (!flag)
                return;

            Thread thread = new Thread(controlClient);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(armClient);

            //ClientPoint client = null;
            //lock (listSync)
            //{
            //    for (i= 0; i < clients.Count; ++i)
            //    {
            //        if (local.Equals(clients[i].localtion))
            //        {
            //            break;
            //        }
            //    }

            //    if (i >= clients.Count)
            //        return;

            //   client = clients[i];
            //   client.isUsing = true;
            //}

            //if (client.loseConnect)
            //{
            //    MessageBox.Show("服务器已经和该客户端失去连接");
            //    client.isUsing = false;
            //    return;
            //}

            //Thread thread = new Thread(controlClient);
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start(client);
            
        }



        private void controlClient(Object client)
        {
            //ClientPoint clientPort = (ClientPoint)client;
            //ControlForm form = new ControlForm(clientPort);
            //Application.Run(form);
            //if (clientPort.loseConnect) //已经和客户端失去了连接
            //{
            //    clients.Remove(clientPort);
            //    lb_conn_num.Text = "" + clients.Count; //更新连接数
            //}

            ArmClient armClient = (ArmClient)client;
            ControlForm form = new ControlForm(armClient);
            Application.Run(form);
        }


        private void cb_MouseClick(object sender, MouseEventArgs e)
        {
            cb.Items.Clear(); //先清空之前的下拉数据

            lock (listSync)
            {
                foreach (var item in armClientDictionary)
                {
                    ArmClient client = item.Value;
                    if (!client.IsChecking)
                        cb.Items.Add(client.Location);
                }
                //foreach (ClientPoint client in clients)
                //{
                //    //没有处于被检查状态，才可以看得见。因为处于被检查时，并不能对其进行操作
                //    //另外失去了联系的话，也是不是看见
                //    //为了防止不小心进行了操作，干脆就不显示其
                //    if( !client.isChecking)
                //        cb.Items.Add(client.localtion);
                //}
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ControlForm form = new ControlForm();
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
