using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace CS_Server.Net
{
    public class ArmClient
    {
        #region 1.变量属性
        private TcpPort controlPort = null;
        private TcpPort videoPort = null;
        private TcpPort photoPort = null;
        private TcpPort heartPort = null;
        private string id = string.Empty;
        private string ipaddress = string.Empty;
        private string location = string.Empty;
        private bool isChecking = false;
        private bool isLoseConnect = false;
        private bool isUsing = false;
        private DateTime lastAccessTime;

        public TcpPort ControlPort
        {
            get { return controlPort; }
            set { controlPort = value; }
        }

        public TcpPort VideoPort
        {
            get { return videoPort; }
            set { videoPort = value; }
        }

        public TcpPort PhotoPort
        {
            get { return photoPort; }
            set { photoPort = value; }
        }

        public TcpPort HeartPort
        {
            get { return heartPort; }
            set { heartPort = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public bool IsChecking
        {
            get { return isChecking; }
            set { isChecking = value; }
        }

        public bool IsLoseConnect
        {
            get
            {
                if (controlPort != null && controlPort.PortSocket.Connected
                    && videoPort != null && videoPort.PortSocket.Connected
                    && photoPort != null && photoPort.PortSocket.Connected
                    && heartPort != null && heartPort.PortSocket.Connected)
                    return true;
                else
                    return false;
            }
            set 
            { 
                isLoseConnect = value; 
            }
        }

        public string Ipaddress
        {
            get { return ipaddress; }
            set { ipaddress = value; }
        }

        public DateTime LastAccessTime
        {
            get { return lastAccessTime; }
            set { lastAccessTime = value; }
        }

        public bool IsUsing
        {
            get { return isUsing; }
            set { isUsing = value; }
        }

        #endregion 1.变量属性

        #region 2.构造方法
        public ArmClient(string ip)
        {
            ipaddress = ip;
        }
        #endregion 2.构造方法

        #region 3.私有方法

        #endregion 3.私有方法

        #region 4.公有方法
        public void ShutDown()
        {
            if (ControlPort != null)
            {
                ControlPort.Close();
                ControlPort = null;
            }
            if (PhotoPort != null)
            {
                PhotoPort.Close();
                PhotoPort = null;
            }
            if (VideoPort != null)
            {
                VideoPort.Close();
                VideoPort = null;
            }
            if (HeartPort != null)
            {
                HeartPort.Close();
                HeartPort = null;
            }
        }
        #endregion 4.公有方法
    }
}
