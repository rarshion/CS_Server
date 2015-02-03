using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace CS_Server.Net
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

        public bool poll(int second)
        {
            return portSocket.Poll(second * 1000, SelectMode.SelectRead);
        }

        public void Close()
        {
            portSocket.Shutdown(SocketShutdown.Both);
        }

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
                }
                catch (SocketException ex)
                {
                    MessageBox.Show(ex.ToString());

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
    }


    /// <summary>
    /// 进行一下字节底层的转换
    /// </summary>
    class Transform
    {

        /// <summary>
        /// 将一个很小的整型转换为一个byte类型存储.返回的数组只有一个元素
        /// </summary>
        /// <param name="k">小于16的整型</param>
        /// <returns>返回对应的byte类型</returns>
        public static byte[] parseMinInt(int k)
        {
            byte[] b = new byte[1];
            b[0] = (byte)k;
            return b;
        }

        /// <summary>
        /// 将一个比较大的整型转换为一个byte数组。并且规定整型的高位在数组的前面
        /// </summary>
        /// <param name="k">大于128的整型，小于2^8 -1的整型</param>
        /// <returns>返回数组</returns>
        public static byte[] parseInt(int k)
        {
            byte[] b = new byte[2];
            b[0] = (byte)(k >> 7);
            b[1] = (byte)(k & 127);

            return b;
        }

        /// <summary>
        /// 将一个从客户端传过来的字节数组转换为整型(只转换前两个字节)
        /// </summary>
        /// <param name="b">b的前面字节为整型的高位</param>
        /// <returns></returns>
        public static int parseByte(byte[] b)
        {
            int k = b[0];
            k <<= 7;
            k += b[1];
            return k;
        }

        /// <summary>
        /// 服务器每次向客户端发送数据前，都要在数据的前面添加一些信息。包括数据的类型。数据的字节数
        /// </summary>
        /// <param name="data">要发送的数据</param>
        /// <param name="kind">数据的类型</param>
        /// <returns>添加头部后的数据</returns>
        public static byte[] addHeadMessage(byte[] data, Kind kind)
        {
            //返回的数组，最前面的两个字节是本数据的长度(不包括最前面的两个字节).然后是类型。最后才是真正的数据
            int x = (int)kind;
            byte[] temp1 = parseMinInt(x);
            int len = data.Length + 1;
            byte[] temp2 = parseInt(len);
            byte[] message = new byte[len + 2]; //存储最后要发送的数据
            Array.Copy(temp2, 0, message, 0, temp2.Length);
            Array.Copy(temp1, 0, message, temp2.Length, temp1.Length);
            Array.Copy(data, 0, message, temp1.Length + temp2.Length, data.Length);
            return message;
        }

        public static byte[] addMsgLength(byte[] data)
        {
            int allLen = data.Length + 2;//正文长度 +２字节的长度标识 = 整条信息长度
            byte[] temp1 = parseInt(data.Length);//将整条信息长度转为byte数组
            byte[] message = new byte[allLen]; //存储最后要发送的数据
            Array.Copy(temp1, 0, message, 0, temp1.Length);
            Array.Copy(data, 0, message, temp1.Length, data.Length);
            return message;
        }

        //这个方法只用于发送照片时，照片的大小转换。
        //客户端传来的数据是 高位在前。低位在后
        public static long bytes2long(byte[] data, int length)
        {
            long size = 0;
            for (int i = 0; i < length; ++i)
            {
                int val = (int)(data[i] - '0');
                size = size * 10 + val;
            }
            return size;
        }

    }
}
