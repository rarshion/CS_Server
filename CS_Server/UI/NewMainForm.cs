using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using MultiSpel.Net;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using MultiSpel.SystemDataModule;
using MultiSpel.DataBaseModule.BLL;
using MultiSpel.DataBaseModule.Model;

namespace MultiSpel.UI
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    public partial class NewMainForm : Form
    {
        #region 1.变量属性
        private string systemConfigXmlFilePath = @"./Config/SystemXmlCfg.xml";
        private SystemXmlConfig systemXmlConfig;
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
        public NodeData curNodeInMap;
        public ImageData curImageInMap;
        Dictionary<string, ArmClient> armClientDictionary = new Dictionary<string, ArmClient>();//用于存放ARM客户端
        readonly static object _sync = new object(); //用于同步
        readonly static object listSync = new object(); //用于对List容器进行操作时的同步
        readonly static object syncLock = new object();
        public int nodeNum = 5;
        public double[] pointArr = new double[10] { 113.35646, 23.16704, 113.35857, 23.16690, 113.35743, 23.16601, 113.35880, 23.16667, 113.35681, 23.16600 };
        public double[] nodePoints;

        public double[] NodePoints
        {
            get
            {
                if (nodePoints.Length == 0)
                    return null;
                return nodePoints;
            }
            set
            {
                nodePoints = value;    
            }
        }
        
        #endregion 1.变量属性

        public NewMainForm()
        {
            InitializeComponent();
        }

        [STAThread]
        private void NewMainForm_Load(object sender, EventArgs e)
        {
            //加载百度地图
            string str_url = Application.StartupPath + "\\BaiduJS\\BaiduMap.html";
            Uri url = new Uri(str_url);
            webBrowser1.Url = url;
            webBrowser1.ObjectForScripting = this;

            //systemXmlConfig = new SystemXmlConfig(systemConfigXmlFilePath);
            //systemXmlConfig.Open();
            //systemXmlConfig.ServerIp = "127.0.0.1";
            //systemXmlConfig.ServerControlPort = "8889";
            //systemXmlConfig.ServerPhotoPort = "8890";
            //systemXmlConfig.ServerVideoPort = "8891";
            //systemXmlConfig.ServerHeartPort = "8892";
            //systemXmlConfig.MaxRecNum = "10";
            //systemXmlConfig.Close();

            NodeBLL nodeBll = new NodeBLL();
            List<NodeData> list = nodeBll.GetAllNodes();
            
            //NodeData nodeData = new NodeData();
            //nodeData.name="光谱图像节点";
            //nodeData.longtitude = "113.35857";
            //nodeData.lantitude = "23.16690";
            //nodeData.location = "华南农业大学信息学院";
            //nodeData.status = 1;
            //nodeBll.Insert(nodeData);

            //nodeData.name = "光谱图像节点";
            //nodeData.longtitude = "113.35743";
            //nodeData.lantitude = "23.16667";
            //nodeData.location = "华南农业大学动物科学学院";
            //nodeData.status = 1;
            //nodeBll.Insert(nodeData);

            //nodeData.name = "光谱图像节点";
            //nodeData.longtitude = "113.35681";
            //nodeData.lantitude = "23.16600";
            //nodeData.location = "华南农业大学理学院";
            //nodeData.status = 1;
            //nodeBll.Insert(nodeData);

            //nodeData.name = "光谱图像节点";
            //nodeData.longtitude = "113.35681";
            //nodeData.lantitude = "23.16600";
            //nodeData.location = "华南农业大学理学院";
            //nodeData.status = 1;
            //nodeBll.Insert(nodeData);

            //nodeData.name = "光谱图像节点";
            //nodeData.longtitude = "113.35880";
            //nodeData.lantitude = "23.16667";
            //nodeData.location = "华南农业大学理学院";
            //nodeData.status = 1;
            //nodeBll.Insert(nodeData);

            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

        }

        #region 服务器初始化
        private void RealVideoImage_ServerInit_button_Click(object sender, EventArgs e)
        {
            if (controlAccept)
            {
                MessageBox.Show("已经开始接受客户端连接了");
                return;
            }

            int maxRecvNum = 10;
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

            //m_checkOnlineThread = new Thread(onlineCheck);
            //m_checkOnlineThread.IsBackground = true;
            //m_checkOnlineThread.Start();

        }

        #region 接受客户端连接
        private void startControlAccept()
        {
            while (controlListenning)
            {
                Console.WriteLine("StartControlAccept");
                Socket controlClient = controlServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("控制客户端IP地址:" + controlClient.RemoteEndPoint);
                AddSocket(controlClient, 1);
            }
        }


        private void startVideoAccept()
        {
            while (videoListenning)
            {
                Console.WriteLine("StartVideoAccept");
                Socket videoClient = videoServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("视频客户端IP地址:" + videoClient.RemoteEndPoint);
                AddSocket(videoClient, 2); //向容器添加一个客户端
            }
        }


        private void startPhotoAccept()
        {
            while (photoListenning)
            {
                Console.WriteLine("StartPhotoAccept");
                Socket photoClient = photoServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("图像客户端IP地址:" + photoClient.RemoteEndPoint);
                AddSocket(photoClient, 3);
            }
        }

        private void startHeartAccept()
        {
            while (heartListenning)
            {
                Console.WriteLine("StartHeartAccept");
                Socket heartClient = heartServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("心跳客户端IP地址:" + heartClient.RemoteEndPoint);
                AddSocket(heartClient, 4);
            }
        }
        #endregion 接受客户端连接

        #region 添加客户端连接
        /// <summary>
        /// 添加Socket
        /// </summary>
        /// <param name="socket">接收到的Socket</param>
        /// <param name="flag">该Socket类型</param>
        public void AddSocket(Socket socket, int flag)
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
                    armClient.Location = (Encoding.ASCII.GetString(location)).TrimEnd('\0');
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
            IPAddress ipaddress = IPAddress.Parse(remoteEndPoint.Address.ToString());
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
                        armClient = dev;
                        return true;
                    }
                }
            }
            armClient = null;
            return false;
        }
        #endregion 查找合法的客户端

        #region 根据位置查找客户端
        public bool FindDeviceByLocation(string location, out ArmClient armClient)
        {
            lock (listSync)
            {
                foreach (var item in armClientDictionary)
                {
                    ArmClient dev = item.Value;
                    if (dev.Location.Trim().Equals(location))
                    {
                        Console.WriteLine("找到该设备了");
                        armClient = dev;
                        return true;
                    }
                }
            }
            armClient = null;
            return false;
        }
        #endregion 根据位置查找客户端

        #region 添加客户端


        #endregion 添加客户端

        #region 移除客户端

        #endregion 移除客户端

        #endregion 服务器初始化

        #region 服务器
        private void RealVideoImage_ServerUnInit_button_Click(object sender, EventArgs e)
        {


        }
        #endregion 服务器

        #region 获取节点经纬度数组
        public int GetNodeNumber()
        {
            NodeBLL nodeBll = new NodeBLL();
            List<NodeData> list = nodeBll.GetAllNodes();
            int nodeNum = list.Count;
            if (nodeNum > 0)
            {
                nodePoints = new double[nodeNum*2];
                double dou_lng, dou_lat;
                for (int i = 0,j=0; i < nodePoints.Length -1 && j<list.Count; i += 2,j++)
                {
                    if (double.TryParse(list[j].longtitude, out dou_lng) 
                        && double.TryParse(list[j].lantitude, out dou_lat))
                    {
                        nodePoints[i] = dou_lng;
                        nodePoints[i + 1] = dou_lat;
                    }
                }
            }
            return nodeNum;
        }
        #endregion 获取节点经纬度数组

        #region 获取指定节点经纬度
        public double GetNodePoints(int index)
        {
            return nodePoints[index];
        }
        #endregion 获取指定节点经纬度

        #region 更新经纬度
        private void UpdateLocation_Tick(object sender, EventArgs e)
        {
            try
            {
                string tag_lng = webBrowser1.Document.GetElementById("lng").InnerText;
                string tag_lat = webBrowser1.Document.GetElementById("lat").InnerText;
                double dou_lng, dou_lat;
                if (double.TryParse(tag_lng, out dou_lng) && double.TryParse(tag_lat, out dou_lat))
                {
                    Longtitude_StatusLabel.Text = "经度" + dou_lng.ToString("F5");
                    Latitude_StatusLabel.Text = "纬度" + dou_lat.ToString("F5");
                }
            }
            catch (Exception ee)
            {
                Longtitude_StatusLabel.Text = "经度:未知";
                Latitude_StatusLabel.Text = "纬度:未知";
            }
        }
        #endregion 更新经纬度

        #region 显示节点
        private void ShowNodeButton_ButtonClick(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("SHOWNODE");
        }
        #endregion 显示节点

        #region 增加节点
        private void AddNodeButton_ButtonClick(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("ADDNODE");
        }
        #endregion 增加节点

        #region 选择节点
        public bool SelectNodeInMap(object JSlng, object JSlat)
        {
            Console.WriteLine(JSlng.ToString() + JSlat.ToString());
            double dou_lng, dou_lat;
            if (double.TryParse(JSlng.ToString(), out dou_lng)
                && double.TryParse(JSlat.ToString(), out dou_lat))
            {
                NodeBLL nodeBll = new NodeBLL();
                NodeData nodeData =
                    nodeBll.GetDetailByPoint(dou_lng, dou_lat);
                if (nodeData != null)
                {
                    curNodeInMap = nodeData;
                    ImageBLL imageBll = new ImageBLL();
                    ImageData imageData =
                        imageBll.GetDetailByLastTime(nodeData.id);
                    if (imageData != null)
                        curImageInMap = imageData;
                    else
                        curImageInMap = null;
                    return true;
                }
                return false;
            }
            else
            {
                curNodeInMap = null;
                curImageInMap = null;
                return false;
            }
        }
        #endregion 选择节点

        #region 获取节点图像路径
        public string ImagePathInNode()
        {
            if (curImageInMap != null)
                return curImageInMap.fullpath;
            else
                return null;
        }
        #endregion 获取节点图像路径

        #region 获取节点图像时间
        public string ImageTimeInNode()
        {
            if (curImageInMap != null)
                return curImageInMap.datetime.ToString();
            else
                return null;
        }
        #endregion 获取节点图像时间

        #region 获取选择节点编号
        public int SelectNodeNum()
        {
            if(curNodeInMap !=null)
                return curNodeInMap.id;
            else
                return -1;
        }
        #endregion 

        #region 获取选择节点名字
        public string SelectNodeName()
        {
            if (curNodeInMap != null)
                return curNodeInMap.name;
            else
                return "该节点无名字";
        }
        #endregion 获取选择节点名字
        
        #region 获取选择节点经度
        public string SelectNodeLong()
        {
            if (curNodeInMap != null)
                return curNodeInMap.longtitude.ToString();
            else
                return "该节点经度未知";
        }
        #endregion 获取选择节点经度

        #region 获取选择节点纬度
        public string SelectNodeLan()
        {
            if (curNodeInMap != null)
                return curNodeInMap.longtitude.ToString();
            else
                return "该节点纬度未知";
        }
        #endregion 获取选择节点纬度

        #region 节点采集图像
        public void NodeCapture()
        {
            if (curNodeInMap != null)
            {
                ArmClient armClient = null;
                if (FindDeviceByLocation(curNodeInMap.location, out armClient))
                {
                    ControlForm form = new ControlForm(armClient,curNodeInMap);
                    form.Show();
                    //Application.Run(form);
                }
            }
            else
            {
                return;
            }
        }
        #endregion 节点采集图像

    }
}
