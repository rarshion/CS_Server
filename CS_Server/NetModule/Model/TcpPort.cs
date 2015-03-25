using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using MultiSpel.NetModule.Utils;

namespace MultiSpel.Net
{
    public class TcpPort
    {

        #region 1.变量属性

        private Socket portSocket;
        private const int LENFLAG = 2;

        public Socket PortSocket
        {
            get { return portSocket; }
            set { portSocket = value; }
        }

        #endregion 1.变量属性

        #region 2.构造方法
        public TcpPort(Socket socket)
        {
            portSocket = socket;
        }
        #endregion 2.构造方法

        #region 3.私有方法

        #endregion 3.私有方法

        #region 4.公有方法

        #region 设置超时时间
        public bool poll(int second)
        {
            return portSocket.Poll(second * 1000, SelectMode.SelectRead);
        }
        #endregion 设置超时时间

        #region 关闭端口
        public void Close()
        {
            portSocket.Shutdown(SocketShutdown.Both);
        }
        #endregion 关闭端口

        #region 设置等待的最长时间
        /// <summary>
        /// 设置Receive函数等待的最长时间
        /// </summary>
        /// <param name="second">等待的秒数</param>
        public void receiveTimeout(int second)
        {
            second *= 1000; //从秒变成毫秒
            if (second < 0)
                second = -1;
            portSocket.ReceiveTimeout = second ;
        }
        #endregion 设置等待的最长时间

        #region 接收数据
        /// <summary>
        /// 由于数据消息的边界问题而要使用该方法。
        /// </summary>
        /// <param name="data">接收的数据存储的位置</param>
        /// <param name="size">要接收的数据数</param>
        /// <returns>实际接收到的字节数</returns>
        public int Receive(byte[] data, int size)
        {
            int total = 0;
            int data_left = size; //余下未接收的字节数
            int recv_num = 0;
            int lastTotal = 0;
            int times = 0; 

            while (total < size) //还没接收完
            {
                try
                {
                    recv_num = portSocket.Receive(data, total, data_left, SocketFlags.None);
                    if (portSocket.Connected && recv_num == 0)
                    {
                        
                    }
                }
                catch (SocketException ex)
                {
                    //MessageBox.Show(ex.ToString());
                    //这个try catch 只捕抓 超时异常
                    if (ex.SocketErrorCode != SocketError.TimedOut)
                    {
                        throw ex;
                    }
                    else
                    {
                        //限定时间内没有接收到任何数据。
                        //或者已经接收过数据，但之后3次机会内也没有接收过新数据了
                        if (total == 0 || (lastTotal == total && times >= 3))
                        {
                            throw ex;
                        }
                        else if (lastTotal == total) //没有接收新数据
                        {
                            ++times; //次数加一
                        }
                        else
                        {
                            times = 0; //清零。从新开始计算
                        }
                    }
                }
                total += recv_num;
                data_left -= recv_num;
            }
            return total;
        }

        /// <summary>
        /// 从客户端接收数据. 
        /// 并且规定 如果是传输图片客户端发送完所有数据后，
        /// 还要发送 前两个字节的值为0的信息 作为结束标志
        /// </summary>
        /// <param name="data">存放接收的数据，不包括长度字节</param>
        /// <returns>返回实际接收的字节数。不包括一开始的两个字节</returns>
        public int Receive(byte[] data)
        {
            byte[] temp = new byte[3];
            Receive(temp, 2);//接收前面表示长度的两个字节
            int size = Transform.parseByte(temp);//得到本次要接收的字节数。
            Console.WriteLine("在TcpPort接收的长度" + size);
            if (size == 0) //已经发送完毕了，本次接收到的两个字节是结束标志. 发送图片时的特殊标志
                return 0;
            int recv_num = Receive(data, size); //接收真正的数据
            return recv_num;
        }

        public int Send(byte[] message)
        {
            byte[] data = Transform.addMsgLength(message);
            int size = data.Length;
            int total = 0;
            int data_left = size;
            int send_num;
            while (total < size)
            {
                send_num = portSocket.Send(data, total, data_left, SocketFlags.None);
                total += send_num;
                data_left -= send_num;
            }
            return total;
        }
        #endregion 接收数据

        #region 发送数据
        /// <summary>
        /// 发送信息，方法内部会自动添加信息头部
        /// </summary>
        /// <param name="message">要发送的信息</param>
        /// <param name="kind">信息类型</param>
        /// <returns></returns>
        public int Send(byte[] message, Kind kind)
        {
            byte[] data = Transform.addHeadMessage(message, kind);
            int size = data.Length;
            int total = 0;
            int data_left = size;
            int send_num;
            while (total < size)
            {
                send_num = portSocket.Send(data, total, data_left, SocketFlags.None);
                total += send_num;
                data_left -= send_num;
            }
            return total;
        }
        #endregion 发送数据

        #endregion 4.公有方法
    }
}