using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultiSpel.Net;

namespace MultiSpel.NetModule.Utils
{
    public class Transform
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
