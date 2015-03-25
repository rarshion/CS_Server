using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigurationPattern;
using System.Xml.Serialization;

namespace MultiSpel.SystemDataModule
{
        [ConfigurationPattern(TPattern.XML)]
        public class SystemXmlConfig : Configuration
        {
            #region 1.变量属性

            #region 1.1 变量
            private const string xmlConfigFilePath = @"./Config/SystemConfig.xml";
            private int randId;
            private string serverIp = "127.0.0.1";
            private string serverControlPort = "8888";
            private string serverPhotoPort = "8889";
            private string serverVideoPort = "8890";
            private string serverHeartPort = "8891";
            private string maxRecNum = "10";
            #endregion 1.1 变量

            #region 1.2 属性

            [ConfigurationIgnore]
            [XmlIgnore]
            public int RandId
            {
                get { return randId; }
                set { randId = value; }
            }

            public string ServerIp
            {
                get { return serverIp; }
                set { serverIp = value; }
            }


            public string ServerControlPort
            {
                get { return serverControlPort; }
                set { serverControlPort = value; }
            }


            public string ServerPhotoPort
            {
                get { return serverPhotoPort; }
                set { serverPhotoPort = value; }
            }

            public string ServerVideoPort
            {
                get { return serverVideoPort; }
                set { serverVideoPort = value; }
            }

            public string ServerHeartPort
            {
                get { return serverHeartPort; }
                set { serverHeartPort = value; }
            }

            public string MaxRecNum
            {
                get { return maxRecNum; }
                set { maxRecNum = value; }
            }
            #endregion 1.2属性

            #endregion 1.变量属性

            #region 2.构造方法

            #region 2.1 无参构造
            public SystemXmlConfig()
                : base(xmlConfigFilePath)
            {
                Random rand = new Random();
                randId = rand.Next();
            }
            #endregion 2.1 无参构造

            #region 2.2 有参构造
            public SystemXmlConfig(string m_xmlConfigXmlFilePath)
                : base(m_xmlConfigXmlFilePath)
            {
                Random rand = new Random();
                randId = rand.Next();
            }

            #endregion 2.2 有参构造

            #endregion 2. 构造方法

            #region 3.私有方法
            #endregion 3.私有方法

            #region 4.共有方法
            #endregion 4. 共有方法

        }
}
