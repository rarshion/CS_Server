using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_Server.Net
{
    public enum FILTER
    {
        first,
        second,
        third
    };

    public enum RESOLUTION
    {
        UHXGA,
        QXGA,
        UXGA,
        XGA,
        SVGA,
        VGA
    };

    public enum BRLEVEL
    {
        f4 = -4,
        f3 = -3,
        f2 = -2,
        f1 = -1,
        nor = 0,
        z1 = 1,
        z2 = 2,
        z3 = 3,
        z4 = 4
    };

        public enum DEVICE             //模拟设备
        {
            ALL,                          //所有设备，针对配置操作
            VEDIO,                     //视频
            FILTER,                     //滤片转换装置
            CAMERA                 //抓拍
        };

        public enum OPERATE            //请求操作类型
        {
            MODIFY_STATE,              //更改开关状态
            QUERY_PARAM,               //查询参数
            ADJUST_PARAM,              //调节参数
            CONFIG_PARAM,              //配置操作
        };

        public struct RequestFormat     //Socket请求信息
        {
            public byte FunCode;        //请求操作类型码
            public byte Device;         //操作设备类型码
            public bool State;          //状态参数
            public int Value;           //请求参数
            public int[] Config;        //配置信息
        }


        public struct ResponseFormat    //Socket响应信息
        {
            public bool IsSucceed;      //操作成功与否
            public int Value;           //返回相应值
            public byte[] Info;         //返回错误信息或视频流或全局配置信息
            public int InfoSize;        //Info数组的长度信息
            public int[] Config;
        }

        public class SockMsgFormat
        {
            private const int MIN_REQUEST_MSGSIZE = 5;              //RequestFormat的固定长度
            private const int MIN_RESPONSE_MSGSIZE = 9;             //ResponseFormat的固定长度
            private const int MAX_CONFIG_NUMBER = 5;
            private const int FORMAT_OK = 0;                        //解码成功       
            private const int FUNCODE_INVALID = -1;                 //解码码错误信息
            private const int DEVICECODE_INVALID = -2;              //解码错误信息
            private const int SENDER_FAULT = -3;                    //解码错误信息

            public SockMsgFormat()
            {

            }

            //对输入的请求消息结构体进行编码，存放在msgFormat数组中，返回编码后的消息长度（字节数）
            static public int EnRequest(RequestFormat Format, out byte[] MsgFormat)
            {
                return EnRequest(Format.FunCode, Format.Device, Format.State, Format.Value, Format.Config, out MsgFormat);
            }

            //根据输入参数对Socket请求的消息进行编码，存放在msgFormat数组中，返回编码后的消息长度（字节数）
            static public int EnRequest(byte FunCode, byte Device, bool State, int Value, int[] Config, out byte[] MsgFormat)
            {
                MsgFormat = null;
                byte funCode = (byte)(FunCode & 0x07);

                //请求操作类型编码：0x00表示开关设备操作，0x01表示查询参数操作，0x02表示调节控制操作，0x03表示设备配置操作
                if (funCode > 0x07)
                {
                    return FUNCODE_INVALID;
                }

                byte deviceCode = (byte)(Device & 0x0f);
                //操作设备编码：0x00表示所有设备(配置操作)，0x01表示门，0x02表示电饭锅，0x03表示空调，0x04表示湿度调节器，0x05表示视频设备，0x06表示机器人管家设备
                if (deviceCode > 0x0f)
                {
                    return DEVICECODE_INVALID;
                }

                int MsgLength = MIN_REQUEST_MSGSIZE;//5

                if ( (funCode == 0x03 && deviceCode == 0x00) 
                        ||  (funCode == 0x00 && deviceCode == 0x03) )//检查全局配置 或者打开抓拍设备
                { 
                    //定义可选数组的长度，并确定总的消息长度
                    if (Config == null || Config[0] == 0)
                        return FUNCODE_INVALID;
                    else
                        MsgLength += Config.Length * 4;
                }

                MsgFormat = new byte[MsgLength];
                byte[] temp = null;
                int i = 0, j = 0;

                //初始化字节流数组（清零）
                for (i = 0; i < MsgFormat.Length; i++)
                {
                    MsgFormat[i] = (byte)(MsgFormat[i] & (byte)0);
                }

                MsgFormat[0] = (byte)(MsgFormat[0] | (byte)(funCode << 5));
                MsgFormat[0] = (byte)(MsgFormat[0] | (byte)(deviceCode << 1));

                if ((funCode == 0x03 && deviceCode == 0x00)
                     || (funCode == 0x00 && deviceCode == 0x03))//配置所有设备时，对可选项进行编码
                {
                    if (State)//对二值参数进行编码
                    {
                        MsgFormat[0] = (byte)(MsgFormat[0] | 0x01);
                    }

                    for (i = 5, j = 0; j < Config.Length; i = i + 4, j++)
                    {
                        temp = BitConverter.GetBytes(Config[j]);
                        MsgFormat[i] = temp[0];
                        MsgFormat[i + 1] = temp[1];
                        MsgFormat[i + 2] = temp[2];
                        MsgFormat[i + 3] = temp[3];
                    }

                    return MsgFormat.Length;
                }
                else//对固定长度（5字节）中的参数进行编码
                {
                    if (State)//对二值参数进行编码
                    {
                        MsgFormat[0] = (byte)(MsgFormat[0] | 0x01);
                    }
                    //对整型参数进行编码
                    temp = BitConverter.GetBytes(Value);
                    for (i = 1, j = 0; j < temp.Length; i++, j++)
                    {
                        MsgFormat[i] = temp[j];
                    }
                    //返回请求消息编码长度
                    return MsgFormat.Length;
                }
            }

            //对输入的请求消息进行解码，解码信息保存在format中
            static public int DeRequest(byte[] Msg, out RequestFormat Format)
            {
                Format = new RequestFormat();
                int i = 0;
                int j = 0;

                //检查请求消息长度
                if (Msg == null | Msg.Length < MIN_REQUEST_MSGSIZE)
                {
                    return SENDER_FAULT;
                }

                //对操作类型进行解码
                byte temp = Msg[0];
                Format.FunCode = (byte)(temp >> 5);
                if (Format.FunCode > 0x03)
                {
                    return FUNCODE_INVALID;
                }

                //对操作设备进行解码
                temp = Msg[0];
                Format.Device = (byte)((byte)((temp >> 1) & 0x0f));
                if (Format.Device > 0x06)
                {
                    return DEVICECODE_INVALID;
                }

                //对二值参数进行解码
                temp = Msg[0];
                Format.State = ((byte)(temp & 0x01)) == 0x01 ? true : false;

                //对整型参数进行解码
                byte[] value = new byte[4];
                for (i = 0, j = 1; j < 5; i++, j++)
                {
                    value[i] = Msg[j];
                }
                Format.Value = BitConverter.ToInt32(value, 0);

                if (Msg.Length > MIN_REQUEST_MSGSIZE)//如果消息长度超过5字节，对可选项进行解码
                {
                    value[0] = Msg[5];
                    value[1] = Msg[6];
                    value[2] = Msg[7];
                    value[3] = Msg[8];
                    int lenght = BitConverter.ToInt32(value, 0);//取得可选数组的长度
                    if ((lenght + 1) * 4 != Msg.Length - MIN_REQUEST_MSGSIZE)
                    {
                        return SENDER_FAULT;
                    }
                    Format.Config = new int[lenght + 1];
                    Format.Config[0] = lenght;
                    for (i = 1, j = 9; i <= lenght; i++, j = j + 4)//对可选数组进行解码
                    {
                        value[0] = Msg[j];
                        value[1] = Msg[j + 1];
                        value[2] = Msg[j + 2];
                        value[3] = Msg[j + 3];
                        Format.Config[i] = BitConverter.ToInt32(value, 0);
                    }
                }
                return FORMAT_OK;
            }

            //根据输入的响应消息结构体进行编码，存放在msgFormat数组中，返回编码后的消息长度（字节数）
            static public int EnResponse(ResponseFormat Format, out byte[] MsgFormat)
            {
                return EnResponse(Format.IsSucceed, Format.Value, Format.InfoSize, Format.Info, Format.Config, out MsgFormat);
            }

            //根据输入的响应参数进行编码，存放在msgFormat数组中，返回编码后的消息长度（字节数）
            static public int EnResponse(bool IsSucceed, int Value, int InfoSize, byte[] Info, int[] Config, out byte[] MsgFormat)
            {
                MsgFormat = null;
                int msgLength = MIN_RESPONSE_MSGSIZE;
                int optSize;

                if (Info != null)//Info信息有效
                {
                    InfoSize = Info.Length;
                    msgLength += InfoSize;
                }
                else
                    InfoSize = 0;

                if (Config != null && Config[0] != 0)//配置信息有效
                {

                    optSize = Config[0];
                    if (optSize + 1 != Config.Length)
                        return 0;
                    msgLength += (optSize + 1) * 4;
                }
                else
                    optSize = 0;

                MsgFormat = new byte[msgLength];
                int i = 0;
                int j = 0;
                byte[] temp = null;

                //初始化字节流数组（清零）
                for (i = 0; i < MsgFormat.Length; i++)
                {
                    MsgFormat[i] = (byte)(MsgFormat[i] & (byte)0);
                }

                //对操作成功标志进行编码
                if (IsSucceed == true)
                {
                    MsgFormat[0] = (byte)(MsgFormat[0] | 0x80);
                }

                //对参数值进行编码
                temp = BitConverter.GetBytes(Value);
                for (i = 0, j = 1; i < temp.Length; i++, j++)
                {
                    MsgFormat[j] = temp[i];
                }

                //对InfoSize进行编码
                temp = BitConverter.GetBytes(InfoSize);
                for (i = 0, j = 5; i < temp.Length; i++, j++)
                {
                    MsgFormat[j] = temp[i];
                }

                //对错误信息或视频流进行编码
                if (InfoSize > 0)
                {
                    for (i = 0; i < InfoSize; i++, j++)
                    {
                        MsgFormat[j] = Info[i];
                    }
                }

                //对配置信息进行编码
                if (optSize > 0)
                {

                    for (i = 0; i < optSize + 1; i++, j += 4)
                    {
                        temp = BitConverter.GetBytes(Config[i]);
                        MsgFormat[j] = temp[0];
                        MsgFormat[j + 1] = temp[1];
                        MsgFormat[j + 2] = temp[2];
                        MsgFormat[j + 3] = temp[3];
                    }
                }

                return MsgFormat.Length;
            }

            //对输入的响应消息进行解码，解码信息保存在format中
            static public int DeResponse(byte[] Msg, int MsgSize, out ResponseFormat Format)
            {
                Format = new ResponseFormat();

                //检查请求消息长度
                if (Msg == null)
                {
                    return SENDER_FAULT;
                }
                if (MsgSize < MIN_RESPONSE_MSGSIZE)
                {
                    return SENDER_FAULT;
                }

                int i = 0;
                int j = 0;
                int optSize;

                //对操作成功标志进行解码
                byte temp = Msg[0];
                if ((byte)(temp & 0x80) == 0x80)
                {
                    Format.IsSucceed = true;
                }
                else
                {
                    Format.IsSucceed = false;
                }

                //对参数值进行解码
                byte[] value = new byte[4];
                for (i = 0, j = 1; i < 4; i++, j++)
                {
                    value[i] = Msg[j];
                }
                Format.Value = BitConverter.ToInt32(value, 0);

                //对InfoSize进行解码
                for (i = 0, j = 5; i < 4; i++, j++)
                {
                    value[i] = Msg[j];
                }
                Format.InfoSize = BitConverter.ToInt32(value, 0);

                //对Info进行解码
                if (Format.InfoSize > 0)
                {
                    Format.Info = new byte[Format.InfoSize];

                    for (i = 0; i < Format.InfoSize; i++, j++)
                    {
                        Format.Info[i] = Msg[j];
                    }
                }
                else
                    Format.Info = null;

                //对配置信息值进行解码
                if (j < MsgSize)
                {
                    value[0] = Msg[j];
                    value[1] = Msg[j + 1];
                    value[2] = Msg[j + 2];
                    value[3] = Msg[j + 3];
                    optSize = BitConverter.ToInt32(value, 0);
                    if ((optSize + 1) * 4 != MsgSize - j)
                    {
                        return SENDER_FAULT;
                    }
                    Format.Config = new int[optSize + 1];
                    Format.Config[0] = optSize;
                    j += 4;
                    for (i = 1; i <= optSize; i++, j = j + 4)
                    {
                        value[0] = Msg[j];
                        value[1] = Msg[j + 1];
                        value[2] = Msg[j + 2];
                        value[3] = Msg[j + 3];
                        Format.Config[i] = BitConverter.ToInt32(value, 0);
                    }
                }
                else
                    Format.Config = null;

                return FORMAT_OK;
            }
        }

}
