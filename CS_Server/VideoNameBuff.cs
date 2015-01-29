using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_Server
{
    class VideoNameBuff
    {
        private List<string> m_fileNameList = null;
        private readonly object m_sync = new object(); //用于同步


        public VideoNameBuff()
        {
            m_fileNameList = new List<string>();
        }

        public void pushFileName(string fileName)
        {
            lock (m_sync)
            {
                m_fileNameList.Add(fileName);
            }
        }

        public string popFileName()
        {
            string fileName = "";
            lock (m_sync)
            {
                if (m_fileNameList.Count != 0)
                {
                    fileName = m_fileNameList[0];
                    m_fileNameList.RemoveAt(0);
                }
            }

            return fileName;
        }


        public void clear()
        {
            lock (m_sync)
            {
                if (m_fileNameList.Count == 0)
                    return;
                try
                {
                    foreach (string e in m_fileNameList)
                        System.IO.File.Delete(e);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
