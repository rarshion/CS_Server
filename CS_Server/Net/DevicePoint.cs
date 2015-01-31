using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace CS_Server.Net
{
    public class DevicePoint
    {
        private Socket controlSocket = null;
        private Socket videoSocket = null;
        private Socket photoSocket = null;
        private Socket heartSocket = null;
        private string id = string.Empty;
        private string ipaddress = string.Empty;
        private string location = string.Empty;


        public Socket ControlSocket
        {
            get { return controlSocket; }
            set { controlSocket = value; }
        }   

        public Socket VideoSocket
        {
            get { return videoSocket; }
            set { videoSocket = value; }
        }

        public Socket PhotoSocket
        {
            get { return photoSocket; }
            set { photoSocket = value; }
        }

        public Socket HeartSocket
        {
            get { return heartSocket; }
            set { heartSocket = value; }
        }


        public string Id { get; set; }
        public string IpAddress { get; set; }
        public string Localtion { get; set; }
        public bool IsChecking{get; set;}
        public bool IsUsing { get; set; }
        public bool IsLoseConnect { get; set; } 
        public DateTime lastAccessTime;


        public DevicePoint(string ip)
        {
            IpAddress = ip;
        }

        public void ShutDown()
        {
            if (ControlSocket != null)
                ControlSocket.Shutdown(SocketShutdown.Both);
            if (PhotoSocket != null)
                PhotoSocket.Shutdown(SocketShutdown.Both);
            if (VideoSocket != null)
                VideoSocket.Shutdown(SocketShutdown.Both);
            if (HeartSocket != null)
                HeartSocket.Shutdown(SocketShutdown.Both);
        }
    }

}
