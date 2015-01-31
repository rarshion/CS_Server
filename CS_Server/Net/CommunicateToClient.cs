using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using System.Media;// axWindowsMediaPlayer1

namespace CS_Server.Net
{

    public enum Command //控制客户端的命令
    {
        heart = 0,
		stop,
        sendImage  ,
        getWater,
        getTemp, //温度
		video, //视频
		timerPicture, //定时拍摄
        changeFilter
    };

    public enum Kind //服务器发送到客户端的信息种类。向客户端发送消息前都要添加在信息头部
    {
        command = 0, //命令
        message = 1  //一些信息
    };

    /// <summary>
    /// 服务器端每接收到一个客户端的请求后，就启动一个线程，该线程创建本对象，然后对客户端进行操作
    /// </summary>
    public class CommunicateToClient
    {

        private TcpPort clientPort;
        private ArmClient armClient;
        private byte[] additionalInformation;
        private const bool hasAdditionalInformation = true;
        private const bool noAdditionalInformation = false;

        private static int RECVSIZE = 1024 * 20;

        public CommunicateToClient(Socket client)
        {
            clientPort = new TcpPort(client);
        }

        public CommunicateToClient(ArmClient client)
        {
            armClient = client;
        }

        #region 发送信息
        /// <summary>
        /// 通知客户端的程序，准备进行一些由kind指定的操作
        /// </summary>
        /// <param name="kind">king为操作的类型.0为上传图片</param>
        public void Prepare(Command com, bool hasAdditionalInformation)
        {
            int x = (int)com;
            byte[] b = Transform.parseMinInt(x); //转换为byte类型
            byte[] message;

            if (hasAdditionalInformation)
            {
                //additionalInformation 并没有在构造函数中初始化
                //所以要想使用，就必须先在对应的控制客户端的方法中进行初始化。
                message = new byte[b.Length + additionalInformation.Length];
                Array.Copy(b, 0, message, 0, b.Length);
                Array.Copy(additionalInformation, 0, message, b.Length, additionalInformation.Length);
                clientPort.Send(message, Kind.command);
            }
            else
            {
                clientPort.Send(b, Kind.command); //将命令发送到客户端
            }
        }
        #endregion 发送信息

        #region 图片
        //在进行实际的数据传输前，必须先调用Prepare()方法，通知客户端接下来要做的
        //调用完后，就可以开始接收数据或者发送数据了
        //对于接收数据的方法，收到数据后，还要发送确认。发送数据的方法则不用
        public bool getPicture(string fileName, int[] photoAttribute)
        {
            FileStream output = File.Create(fileName);

            byte[] data = new byte[RECVSIZE]; //new byte[1024];
            int recv_num;

            additionalInformation = new byte[photoAttribute.Length];
            
            int i;
            for(i = 0; i < photoAttribute.Length; ++i)
            {
				//因为这些值的有负值。但网络上传输负数比较麻烦，所以+=10变成正数再去传
				//到时在客户端，control程序那里-=10，获得原始值
				photoAttribute[i] += 10;
                //将照片的属性(整型)转换为byte类型。并依次放到附加信息数组中去。
                Array.Copy(Transform.parseMinInt(photoAttribute[i]), 0, 
                           additionalInformation, i, 1);
            }

            Prepare(Command.sendImage, hasAdditionalInformation); //告诉客户端做好发送图片的准备。

            recv_num = clientPort.Receive(data); //先接收发送的图片的大小。
            long fileSize = Transform.bytes2long(data, recv_num);

            long recvSize = 0;
            while ( fileSize != 0 )
            {
                //函数返回的是本次接收的字节数。不包括最前面的表示信息长度的两个字节
                recv_num = clientPort.Receive(data);
                if (recv_num == 0) //接收到了客户端发来的发送完毕标志。
                    break;

                recvSize += recv_num;
                output.Write(data, 0, recv_num);//写文件
            }

            output.Close();

            //可能客户端没有发送数据，或者由于网络不好，客户端取消了发送
            return fileSize != 0 && recvSize == fileSize;
        }
        #endregion 图片




        #region 视频
        public bool getVideo(string fileName, int[] videoAttribute)
        {
            Console.WriteLine("start getvideo\n");

            FileStream output = File.Create(fileName);
            byte[] data = new byte[RECVSIZE]; //new byte[1024];
            int recv_num;

            //additionalInformation = new byte[videoAttribute.Length];
            //int i;
            //for (i = 0; i < videoAttribute.Length; ++i)
            //{
            //    //因为这些值的有负值。但网络上传输负数比较麻烦，所以+=10变成正数再去传
            //    //到时在客户端，control程序那里-=10，获得原始值
            //    videoAttribute[i] += 10;
            //    Console.WriteLine("getVideo"+videoAttribute[i]);
            //    //将照片的属性(整型)转换为byte类型。并依次放到附加信息数组中去。
            //    Array.Copy(Transform.parseMinInt(videoAttribute[i]), 0,
            //               additionalInformation, i, 1);
            //}

            //Prepare(Command.video, hasAdditionalInformation); //告诉客户端做好发送图片的准备。
            //recv_num = clientPort.Receive(data); //先接收发送的图片的大小。

            recv_num = armClient.VideoPort.Receive(data); //先接收发送的图片的大小。
            if (recv_num > 0)
            {
                Console.WriteLine("接收到" + recv_num);
                 output.Write(data, 0, recv_num);//写文件
            }


            //long fileSize = Transform.bytes2long(data, recv_num);

            //long recvSize = 0;
            //while (fileSize != 0 )
            //{
            //    //函数返回的是本次接收的字节数。不包括最前面的表示信息长度的两个字节
            //    recv_num = armClient.VideoPort.Receive(data);
            //    if (recv_num == 0) //接收到了客户端发来的发送完毕标志。
            //        break;
            //    Console.WriteLine("接收到" + recv_num);
            //    recvSize += recv_num;
            //    output.Write(data, 0, recv_num);//写文件
            //}

            output.Close();
            return true;

            //可能客户端没有发送数据，或者由于网络不好，客户端取消了发送
           // return fileSize != 0 && fileSize == recvSize;
        }
        #endregion 视频

        #region 定时采集
        /// <summary>
        /// 进行定时采集
        /// </summary>
        public bool gettimePicture(string fileName, int[] photoAttribute)
        {
            Console.WriteLine("gettimepicture in communicateToclient");
            FileStream output = File.Create(fileName);
            byte[] data = new byte[RECVSIZE]; //new byte[1024];
            int recv_num;

            additionalInformation = new byte[photoAttribute.Length];

            int i;
            for (i = 0; i < photoAttribute.Length; ++i)
            {
                photoAttribute[i] += 10;
                Array.Copy(Transform.parseMinInt(photoAttribute[i]), 0,
                           additionalInformation, i, 1);
            }

            Prepare(Command.timerPicture, hasAdditionalInformation); //告诉客户端做好发送图片的准备。

            recv_num = clientPort.Receive(data); //先接收发送的图片的大小。
            long fileSize = Transform.bytes2long(data, recv_num);

            long recvSize = 0;
            while (fileSize != 0)
            {
                //函数返回的是本次接收的字节数。不包括最前面的表示信息长度的两个字节
                recv_num = clientPort.Receive(data);
                if (recv_num == 0) //接收到了客户端发来的发送完毕标志。
                    break;

                recvSize += recv_num;
                output.Write(data, 0, recv_num);//写文件
            }

            output.Close();

            //可能客户端没有发送数据，或者由于网络不好，客户端取消了发送
            return fileSize != 0 && recvSize == fileSize;
        }
        #endregion 定时采集

        #region 获取水分
        public String getWater()
        {
            Prepare(Command.getWater, noAdditionalInformation);
            byte[] data = new byte[100];
            //接收到的水分值是一些字符值。比如73，是由'7'和'3'。对应的ASCII码是
            //55， 51。所以，接收水分和温度时，不能再用Transform 来解析。而是用Encoding类
            int recv_num = clientPort.Receive(data);
            String val = Encoding.ASCII.GetString(data);
          //  int val = Transform.parseByte(data); //把字节转换为整型
            return val;
        }
        #endregion 获取水分

        #region 获取温度
        /// <summary>
        /// 获取温度
        /// </summary>
        /// <param name="waitTime">接收数据时允许的最长等待时间</param>
        /// <returns></returns>
        public String getTemp(int waitTime)
        {
            Prepare(Command.getTemp, noAdditionalInformation);

            byte[] data = new byte[100];
                
            clientPort.receiveTimeout(waitTime); //接收一个数据。waitTime秒的最长等待时间。
            int recv_num = clientPort.Receive(data);
            clientPort.receiveTimeout(0); //复原

            String val = Encoding.ASCII.GetString(data);
            return val;
        }
        #endregion 获取温度

        #region 控制滤光片转换
        public string changeFilter(int waitTime,int choice)
        {
            string val = string.Empty;
            byte[] data = new byte[RECVSIZE]; //new byte[1024];
            int recv_num;
            additionalInformation = new byte[1];
            Array.Copy(Transform.parseMinInt(choice), 0,
                    additionalInformation, 0, 1);
            Prepare(Command.changeFilter, hasAdditionalInformation);
            clientPort.receiveTimeout(waitTime); //接收一个数据。waitTime秒的最长等待时间。
            recv_num = clientPort.Receive(data);
            clientPort.receiveTimeout(0); //复原
            if (recv_num > 0)
                val = Encoding.ASCII.GetString(data);
            else
                val = "接收数据超时";
            return val;
        }
        #endregion 控制滤光片转换

        #region 心跳连接
        /// <summary>
        /// 如果在规定的时间内没有收到客户端的信息，就说明客户端掉线了，返回false
        /// </summary>
        /// <param name="second">等待的秒数</param>
        /// <returns></returns>
        public bool heartCheck(int second)
        {
            bool canRead = false;
            try
            {
                Prepare(Command.heart, noAdditionalInformation);
                byte[] data = new byte[100];
                canRead = clientPort.poll(second);
                if (canRead)
                {
                    int recv_num = clientPort.Receive(data); //接收数据，也就是清空数据
                    Console.WriteLine("---------心跳检查接收到的数据 "
                        + Encoding.ASCII.GetString(data));
                }
            }
            catch (SocketException ex)
            {
                canRead = false;
            }

            return canRead;
        }
        #endregion 心跳连接

        public void stopVideo()
        {
            additionalInformation = new byte[1];
            Array.Copy(Transform.parseMinInt(1), 0, additionalInformation, 0, 1);

            //just to tell the client to stop 
            Prepare(Command.video, hasAdditionalInformation);

            byte[] data = new byte[RECVSIZE]; //new byte[1024];
            int num = clientPort.Receive(data);
        }

        public void stopTimePicture()
        {
            additionalInformation = new byte[1];
            Array.Copy(Transform.parseMinInt(1), 0, additionalInformation, 0, 1);

            //just to tell the client to stop 
            Prepare(Command.timerPicture, hasAdditionalInformation);

            byte[] data = new byte[RECVSIZE]; //new byte[1024];
            int num = clientPort.Receive(data);
        }

        public int SendAndAcceptMsg(RequestFormat request, ref string erro, out ResponseFormat response)
        {
            //将Socket请求信息request进行编码，发送到指定的ip地址的指定端口上，对返回的响应信息进行解码成Socket响应信息response
            response = new ResponseFormat();
            byte[] requestMsg;
            int ret = SockMsgFormat.EnRequest(request, out requestMsg);//对请求消息进行编码
            if (requestMsg != null)
                armClient.ControlPort.Send(requestMsg);

            //if(requestMsg != null)
            //    clientPort.Send(requestMsg);

            //if (ret <= 0)
            //{
            //    erro += "编码出错" + ret.ToString();
            //    return ret;
            //}
            //IPAddress ipadr;
            //int port;
            //Socket lst;
            //IPEndPoint iped;
            //byte[] responseMsg = new byte[200];
            //int responseSize;
            //try
            //{
            //    ipadr = IPAddress.Parse("192.168.0.115");//ip地址
            //    port = 2312;//端口号
            //    iped = new IPEndPoint(ipadr, port);
            //    lst = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //}
            //catch (Exception e)
            //{
            //    erro += e.Message + '0';
            //    return 0;
            //}

            //try
            //{
            //    lst.Connect(iped);
            //}
            //catch (Exception e)
            //{
            //    erro += e.Message + '1';
            //    return 1;
            //}

            //try
            //{
            //    lst.Send(requestMsg);//发送请求消息
            //}
            //catch (Exception e)
            //{
            //    erro += e.Message + '2';
            //    return 2;
            //}

            //try
            //{
            //    responseSize = lst.Receive(responseMsg);//接受响应消息
            //}
            //catch (Exception e)
            //{
            //    erro += e.Message + '3';
            //    return 3;
            //}

            //ret = SockMsgFormat.DeResponse(responseMsg, responseSize, out response);//对响应消息进行解码
            //try
            //{
            //    lst.Shutdown(SocketShutdown.Both);
            //    lst.Close();
            //}
            //catch (Exception e)
            //{
            //    erro += e.Message + '4';
            //    return 4;
            //}
            //if (ret != 0)
            //{
            //    erro += "解码出错" + ret.ToString() + "!5";
            //    return 5;
            //}


            return -1;//成功

        }


        //对相关设备进行相关操作
        public  bool Operate(OPERATE Operate, DEVICE Device, ref bool State, ref int Value, ref int[] Config, ref string erro)
        {
            RequestFormat request = new RequestFormat();//将请求封装成Socket请求信息

            int value = Value;//用于暂时存放参数Value值，Value值即可能是传进参数又可能是传出参数
            bool state = State;//用于暂时存放参数State值，State值即可能是传进参数又可能是传出参数
            int[] config = Config;//用于暂时存放参数Config值，Config值即可能是传进参数又可能是传出参数
            Config = null;
            int j;
            request.FunCode = (byte)((int)Operate);
            request.Device = (byte)((int)Device);

            switch (Operate)//根据操作，用相应的参数对request进行封装
            {
                case OPERATE.MODIFY_STATE://该操作为更改开关状态操作，参数State表示请求打开还是关闭
                    {
                        if (Device == DEVICE.DOOR || Device == DEVICE.COOKER || Device == DEVICE.AIRCONDITION || Device == DEVICE.HUMIDIFIER || Device == DEVICE.VEDIO)
                        {
                            request.State = state;
                            request.Value = value;
                        }
                        else
                        {
                            erro += "你选择的设备不能进行更改开关状态操作！";
                            return false;
                        }
                        break;
                    }

                case OPERATE.QUERY_PARAM://该操作为查询操作，根据Value来判断，如何Value为0说明是查状态，如何Value为1说明是查参数，如何Value为2说明是查配置信息
                    {
                        request.Value = value;//需要做出是否合理的判断
                        break;
                    }

                case OPERATE.ADJUST_PARAM://该操作为调节参数操作
                    {
                        if (Device == DEVICE.AIRCONDITION)//调节空调操作，参数为State
                        {
                            if (state == true)
                                request.Value = 1;
                            else
                                request.Value = -1;
                        }
                        else
                        {
                            if (Device == DEVICE.HUMIDIFIER || Device == DEVICE.ROBOT || Device == DEVICE.VEDIO)//调节加湿器或机器人管家操作，参数为State
                                request.Value = value;
                            else
                            {
                                erro += "你选择的设备不能进行调节参数操作！";
                                return false;
                            }
                        }
                        break;
                    }

                case OPERATE.CONFIG_PARAM://该操作为配置操作
                    {
                        if (config == null)
                        {
                            erro += "配置操作时，传入参数出错！";
                            return false;
                        }
                        if (Device == DEVICE.ALL)//全局配置
                        {
                            if (config.Length != 5)//全局配置时，传入参数只有一个
                            {
                                erro += "全局配置时，传入参数出错！";
                                return false;
                            }
                            else
                            {
                                request.Config = new int[config.Length + 1];
                                request.Config[0] = config.Length;
                                j = 1;
                                foreach (int i in config)
                                {
                                    request.Config[j] = i;
                                    j++;
                                }
                            }
                        }
                        else
                        {
                            if (Device == DEVICE.AIRCONDITION || Device == DEVICE.HUMIDIFIER || Device == DEVICE.ROBOT)//空调或加湿器或机器人管家配置
                            {
                                if (config.Length != 1)//单一配置时，传入参数只有一个
                                {
                                    erro += "空调或加湿器或机器人管家配置时，传入参数出错！";
                                    return false;
                                }
                                else
                                    request.Value = config[0];
                            }
                            else
                            {
                                erro += "你选择的设备不能进行配置操作！";
                                return false;
                            }
                        }
                        break;
                    }

                default:
                    {
                        erro += "选择的操作有误！";
                        return false;
                    }
            }

            ResponseFormat response;//Socket响应信息用于接受响应消息
            int ret = SendAndAcceptMsg(request, ref erro, out response);//发送请求消息和接受响应消息
            if (ret != -1 && ret != 4)
            {
                erro += "Socket传输错误！";
                return false;//出错
            }

            return true;

            //if (response.IsSucceed == true)//该操作成功
            //{
            //    erro = Encoding.ASCII.GetString(response.Info);
            //    switch (Operate)
            //    {
            //        case OPERATE.MODIFY_STATE://该操作为更改开关状态操作
            //            {
            //                if (Device == DEVICE.AIRCONDITION || Device == DEVICE.HUMIDIFIER)
            //                    Value = response.Value;
            //                return true;
            //            }
            //        case OPERATE.QUERY_PARAM://该操作为查询参数操作
            //            {
            //                if (value == 0)//查询状态
            //                {
            //                    if (response.Value == 1)
            //                        State = true;
            //                    else
            //                        State = false;
            //                    return true;
            //                }
            //                else
            //                {
            //                    if (value == 1)//查询参数
            //                    {
            //                        Value = response.Value;
            //                        return true;
            //                    }
            //                    else
            //                    {

            //                        if (value == 2)//参数配置信息
            //                        {
            //                            if (Device == DEVICE.ALL)//查询全局配置信息操作
            //                            {
            //                                if (response.Config == null || response.Config[0] != 5)//全局查询配置时，返回值有8个
            //                                {
            //                                    erro += "全局查询配置信息时，返回值有误！";
            //                                    return false;
            //                                }
            //                                else
            //                                {
            //                                    Config = new int[5];
            //                                    j = 0;
            //                                    for (int i = 1; i < 6; i++)
            //                                    {
            //                                        Config[j] = response.Config[i];
            //                                        j++;
            //                                    }
            //                                    return true;
            //                                }
            //                            }
            //                            else
            //                            {
            //                                Config = new int[1];
            //                                Config[0] = response.Value;
            //                                return true;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            erro += "查询操作时，传出的Value标志有误！";
            //                            return false;
            //                        }
            //                    }
            //                }
            //            }
            //        case OPERATE.ADJUST_PARAM://该操作为调节参数操作
            //            {
            //                Value = response.Value;
            //                return true;
            //            }
            //        case OPERATE.CONFIG_PARAM://该操作为配置操作
            //            {
            //                return true;
            //            }
            //        default:
            //            {
            //                erro += "!@#$%^&*()";
            //                return false;
            //            }
            //    }
            //}
            //else
            //{//该操不成功
            //    if (response.Info == null)
            //    {
            //        erro += "板子崩毁！";
            //    }
            //    else
            //    {
            //        erro = Encoding.ASCII.GetString(response.Info);
            //    }
            //    return false;
            //}
        }

    }



 
}
