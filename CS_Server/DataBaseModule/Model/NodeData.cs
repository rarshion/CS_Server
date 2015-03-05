using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiSpel.DataBaseModule.Model
{
    [Serializable]
    public class NodeData
    {
        public int id { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public string longtitude { get; set; }
        public string lantitude { get; set; }
        public string location { get; set; }
    }
}
