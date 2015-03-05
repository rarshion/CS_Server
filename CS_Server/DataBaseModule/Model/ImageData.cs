using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiSpel.DataBaseModule.Model
{
    public class ImageData
    {
        public int id { get; set; }
        public int nodeid { get; set; }
        public DateTime datetime { get; set; }
        public int status { get; set; }
        public string path { get; set; }
        public string fullpath { get; set; }
        public string fileName { get; set; }
    }
}
