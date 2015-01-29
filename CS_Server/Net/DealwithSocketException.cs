using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace CS_Server
{
    class DealwithSocketException
    {
        private SocketException m_ex;

        //由于不能对所有的异常都进行判断处理，所以，当那些没有判断的异常，将使用系统的错误信息
        public string errorMessage { get; private set; }

        public DealwithSocketException(SocketException ex)
        {
            m_ex = ex;
            judge();
        }


        private void judge()
        {
            errorMessage = m_ex.SocketErrorCode.ToString();

            switch (m_ex.SocketErrorCode)
            {
                case SocketError.ConnectionAborted: 
                    errorMessage = "连接中断,请取消对该客户端的操作"; break;

                case SocketError.TimedOut:
                    errorMessage = "操作超时！已多次尝试，仍超时。建议检查线路连接情况"; break;
            }
        }


    }
}
