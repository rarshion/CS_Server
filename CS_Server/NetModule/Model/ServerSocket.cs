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
    public class ServerSocket
    {
        private IPEndPoint serverIpe;
        private Socket serverSocket;

        public ServerSocket(int port, int listen)
        {
            serverIpe = new IPEndPoint(IPAddress.Any, port);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(serverIpe);
            serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, 1024 * 20);
            serverSocket.Listen(listen);
        }

        public Socket Accept()
        {
            return serverSocket.Accept();
        }

        public void Close()
        {
            serverSocket.Close();
        }

    }


   
}
