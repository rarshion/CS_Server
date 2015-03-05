using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace MultiSpel.Net
{
    //这个类负责进行socket通信
    class ServerSocket
    {
        private IPEndPoint m_ipe;
        private Socket m_server;

        public ServerSocket(int port, int listen)
        {
            m_ipe = new IPEndPoint(IPAddress.Any, port);
            m_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_server.Bind(m_ipe);
            m_server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, 1024 * 20);
            m_server.Listen(listen);
        }

        public Socket Accept()
        {
            return m_server.Accept();
        }

        public void Close()
        {
            m_server.Close();
        }

    }


   
}
