#region ************************文件说明************************************
/// 作者(Author)：                       黄顺彬
/// 
/// 日期(Create Date)：              2015.3.12
/// 
/// 功能：                                      程序主界面
///
/// 修改记录(Revision History)：     无
///
#endregion *****************************************************************

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
using System.Drawing;

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
        public NodeData curSelectNode;
        public ImageData curImageInMap;
        List<ArmClient> armClientList = new List<ArmClient>();
        Dictionary<string, ArmClient> armClientDictionary = new Dictionary<string, ArmClient>();//用于存放ARM客户端
        readonly static object deviceSync = new object(); //
        readonly static object listSync = new object(); //用于对List容器进行操作时的同步
        readonly static object dicSync = new object();//用于对dictionary容易进行操作时的同步
        public int nodeNum = 5;
        //public double[] pointArr = 
        //new double[10] { 113.35646, 23.16704, 113.35857, 23.16690, 113.35743, 23.16601, 113.35880, 23.16667, 113.35681, 23.16600 };
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

        #region 2.构造方法
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
        #endregion 构造方法

        #region 3.私有方法

        #region 服务器开始监听
        private void RealVideoImage_ServerInit_button_Click(object sender, EventArgs e)
        {
            if (controlAccept)
            {
                MessageBox.Show("已经开始接受客户端连接了");
                return;
            }

            int maxRecvNum = 50;
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

            updateNodeStatus_timer.Enabled = true;

            //m_checkOnlineThread = new Thread(onlineCheck);
            //m_checkOnlineThread.IsBackground = true;
            //m_checkOnlineThread.Start();
        }
        #endregion 服务器开始监听

        #region 服务器接收客户端连接
        private void startControlAccept()
        {
            while (controlAccept)
            {
                Console.WriteLine("StartControlAccept");
                Socket controlClient = controlServer.Accept();
                Console.WriteLine("控制客户端IP地址:" + controlClient.RemoteEndPoint);
                AddClient(controlClient, 1);
            }
        }

        private void startVideoAccept()
        {
            while (videoAccept)
            {
                Console.WriteLine("StartVideoAccept");
                Socket videoClient = videoServer.Accept();
                Console.WriteLine("视频客户端IP地址:" + videoClient.RemoteEndPoint);
                AddClient(videoClient, 2);
            }
        }

        private void startPhotoAccept()
        {
            while (photoAccept)
            {
                Console.WriteLine("StartPhotoAccept");
                Socket photoClient = photoServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("图像客户端IP地址:" + photoClient.RemoteEndPoint);
                AddClient(photoClient, 3);
            }
        }

        private void startHeartAccept()
        {
            while (heartAccept)
            {
                Console.WriteLine("StartHeartAccept");
                Socket heartClient = heartServer.Accept(); //在等待时，可能会被强制终止
                Console.WriteLine("心跳客户端IP地址:" + heartClient.RemoteEndPoint);
                AddClient(heartClient, 4);
            }
        }
        #endregion 接受客户端连接

        #region 服务器结束监听
        private void RealVideoImage_ServerUnInit_button_Click(object sender, EventArgs e)
        {

            controlAccept = false;
            photoAccept = false;
            videoAccept = false;
            heartAccept = false;

            if (controlThread != null && controlThread.IsAlive)
            {
                controlThread.Abort();
                controlThread = null;
            }

            if (videoThread != null)
            {
                videoThread.Abort();
                videoThread = null;
            }

            if (photoThread != null)
            {
                photoThread.Abort();
                photoThread = null;
            }

            if (heartThread != null)
            {
                heartThread.Abort();
                heartThread = null;
            }

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

            if (heartServer != null)
            {
                heartServer.Close();
                heartServer = null;
            }

            updateNodeStatus_timer.Enabled = false;

            lock (dicSync)
            {
                armClientDictionary.Clear();
            }

            lock (listSync)
            {
                armClientList.Clear();
            }
        }
        #endregion 服务器结束监听

        #endregion 3.私有方法

        #region 添加客户端连接
        /// <summary>
        ///  armClientDictionary
        /// </summary>
        /// 
        /// <param name="socket"></param>
        /// <param name="flag"></param>
        public void AddClient(Socket socket, int flag)
        {
            lock (deviceSync)
            {
                ArmClient armClient = null;
                TcpPort tempPort = new TcpPort(socket);
                AddClientPort(tempPort, flag, ref armClientDictionary);
                if (!(HasValDevice(out armClient))) return;
                if (armClient != null)
                {
                    TcpPort comPort = armClient.ControlPort;
                    byte[] nodeIdMes = new byte[200];
                    comPort.Receive(nodeIdMes);
                    armClient.Id = (Encoding.ASCII.GetString(nodeIdMes)).TrimEnd('\0');
                    Console.WriteLine("客户端编号为" + armClient.Id);
                    byte[] data = Encoding.ASCII.GetBytes("Welcome to server");
                    comPort.Send(data, Kind.message);
                    NodeBLL nodeBll = new NodeBLL();
                    int clientId;
                    if(Int32.TryParse(armClient.Id, out clientId))
                    {
                        UpdateArmClientId(armClient.Ipaddress, clientId);
                        NodeData nodeData = nodeBll.GetDetailByPK(clientId);
                        if (nodeData != null)
                        {
                            armClient.Location = nodeData.location;
                            if (controlListenning)
                                AddClientToList(armClient,ref armClientList);
                            DeleteClientPort(armClient.Ipaddress, ref armClientDictionary);
                            armClient.IsChecking = false;
                            armClient.IsUsing = false;
                            armClient.LastAccessTime = DateTime.Now;
                            armClient.IsLoseConnect = false;
                            armClient.Status = true;
                        }
                    }
                }
            }
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
        public bool AddClientPort(TcpPort port, int flag, ref Dictionary<string, ArmClient> deviceDictonary)
        {
            IPEndPoint remoteEndPoint = (IPEndPoint)port.PortSocket.RemoteEndPoint;
            IPAddress ipaddress = IPAddress.Parse(remoteEndPoint.Address.ToString());
            string ip = ipaddress.ToString();
            lock (dicSync)
            {
                ArmClient device = null;
                if (!deviceDictonary.TryGetValue(ip, out device))
                {
                    device = new ArmClient(ip);
                    deviceDictonary.Add(ip, device);
                    Console.WriteLine("在字典里面的新建" + device.Ipaddress);
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
            if (device == null)
                return false;
            if (device.ControlPort != null && device.VideoPort != null
                && device.PhotoPort != null && device.HeartPort != null)
            {
                Console.WriteLine("端口齐全");
                return true;
            }
            else
            {
                Console.WriteLine("端口不齐全");
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
            lock (dicSync)
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

        #region 删除字典中的客户端
        public void DeleteClientPort(string ipAddr, ref Dictionary<string, ArmClient> deviceDictonary)
        {
            lock (dicSync)
            {
                if (deviceDictonary.ContainsKey(ipAddr))
                {
                    Console.WriteLine("删除字典中的设备");
                    deviceDictonary.Remove(ipAddr);
                }
            }
        }
        #endregion 删除字典中的客户端

        #region  更新字典中客户端编号
        void UpdateArmClientId(string ipAdd,int id)
        {
            lock (dicSync)
            {
                foreach (var item in armClientDictionary)
                {
                    ArmClient dev = item.Value;
                    if (dev.Ipaddress.Trim().Equals(ipAdd))
                    {
                        Console.WriteLine("找到该设备ID为" + id);
                        dev.Id = id.ToString();
                    }
                }
            }
        }
        #endregion  更新字典中客户端编号

        #region 添加列表中的客户端
        public void AddClientToList(ArmClient armClient,ref List<ArmClient> armClientList)
        {
            lock (listSync)
            {
                DelectClientInList(armClient.Id, ref armClientList);
                armClientList.Add(armClient);
            }
        }
        #endregion 添加列表中的客户端

        #region 删除列表中的客户端
        public void DelectClientInList(string clientId, ref List<ArmClient> armClientList)
        {
            for (int i = 0; i < armClientList.Count; i++)
            {
                if (armClientList[i].Id.Equals(clientId))
                {
                    Console.WriteLine("它登录过了,消除他的记录");
                    armClientList.Remove(armClientList[i]);
                }
            }
        }
        #endregion 删除列表中的客户端

        #region 添加客户端到列表
        private delegate void AddItemToNodeInfoOnListViewDelegate(string nodeId, string nodeName, string nodeStatus);
        private void AddItemToNodeInfoOnListView(string nodeId,string nodeName,string nodeStatus)
        {
            if (nodeInfo__listView.InvokeRequired)
            {
                AddItemToNodeInfoOnListViewDelegate d
                    = AddItemToNodeInfoOnListView;
                nodeInfo__listView.Invoke(
                    d, new object[] { nodeId,nodeName,nodeStatus });
            }
            else
            {
                ListViewItem item = new ListViewItem(new string[] {nodeId,nodeName,nodeStatus});
                this.nodeInfo__listView.Items.Add(item);
            }
        }

        #endregion 添加客户端

        #region 更新客户端状态
        private void updateNodeStatus_timer_Tick(object sender, EventArgs e)
        {
            if (controlListenning)
            {
                lock (listSync)
                {
                    nodeInfo__listView.Items.Clear();
                    for (int i = 0; i < armClientList.Count; i++)
                    {
                        AddItemToNodeInfoOnListView(armClientList[i].Id,
                            armClientList[i].Location, armClientList[i].Status == true ? "在线" : "离线");
                    }
                }
            }
        }
        #endregion 更新客户端状态

        #region 移除客户端

        #endregion 移除客户端

        #region 百度地图操作

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
                    curSelectNode = nodeData;
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
                curSelectNode = null;
                curImageInMap = null;
                return false;
            }
        }

        public bool SelectNodeInListView(int nodeId)
        {
            NodeBLL nodeBll = new NodeBLL();
            NodeData nodeData =
                nodeBll.GetDetailByPK(nodeId);
            if (nodeData != null)
            {
                curSelectNode = nodeData;
                return true;
            }
            return false;
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
            if(curSelectNode !=null)
                return curSelectNode.id;
            else
                return -1;
        }
        #endregion 

        #region 获取选择节点名字
        public string SelectNodeName()
        {
            if (curSelectNode != null)
                return curSelectNode.name;
            else
                return "该节点无名字";
        }
        #endregion 获取选择节点名字
        
        #region 获取选择节点经度
        public string SelectNodeLong()
        {
            if (curSelectNode != null)
                return curSelectNode.longtitude.ToString();
            else
                return "该节点经度未知";
        }
        #endregion 获取选择节点经度

        #region 获取选择节点纬度
        public string SelectNodeLan()
        {
            if (curSelectNode != null)
                return curSelectNode.longtitude.ToString();
            else
                return "该节点纬度未知";
        }
        #endregion 获取选择节点纬度

        #region 根据位置查找客户端
        public bool FindDeviceByNoeId(int id, out ArmClient armClient)
        {
            lock (listSync)
            {
                foreach (var item in armClientList)
                {
                    int devId;
                    ArmClient dev = item;
                    if (Int32.TryParse(dev.Id, out devId))
                    {
                        if (devId == id)
                        {
                            Console.WriteLine("找到该设备了");
                            armClient = dev;
                            return true;
                        }
                    }
                }
            }
            armClient = null;
            return false;
        }
        #endregion 根据位置查找客户端

        #endregion 百度地图操作

        #region 节点操作

        #region 节点采集图像
        public void NodeCapture()
        {
            if (curSelectNode != null)
            {
                ArmClient armClient = null;
                if (FindDeviceByNoeId(curSelectNode.id, out armClient))
                {
                    if (armClient.Status)
                    {
                        ControlForm form = new ControlForm(armClient, curSelectNode);
                        form.Show();
                    }
                    else
                        MessageBox.Show("该节点不在线");
                }
            }
            else
            {
                return;
            }
        }
        #endregion 节点采集图像


        #region  节点列表
        private void nodeInfo__listView_MouseClick(object sender, MouseEventArgs e)
        {
            nodeInfo__listView.MultiSelect = false;
            if (e.Button == MouseButtons.Right)
            {
                int id;
                string nodeId = nodeInfo__listView.SelectedItems[0].Text;
                Point p = new Point(e.X, e.Y);
                this.nodeControl_contextMenuStrip.Show(this.nodeInfo__listView, p);
                if(Int32.TryParse(nodeId,out id))
                    SelectNodeInListView(id);
            }
        }
        #endregion 节点列表

        #region 文件列表右击打开控制界面
        private void NodeControl_Item_Click(object sender, EventArgs e)
        {
            if (curSelectNode != null)
            {
                ArmClient armClient = null;
                if (FindDeviceByNoeId(curSelectNode.id, out armClient))
                {
                    if (armClient.Status)
                    {
                        ControlForm form = new ControlForm(armClient, curSelectNode);
                        form.Show();
                    }
                    else
                        MessageBox.Show("该节点不在线");
                }
            }
            else
            {
                return;
            }
        }
        #endregion 文件列表右击打开控制界面


        #endregion 节点操作
    }
}
