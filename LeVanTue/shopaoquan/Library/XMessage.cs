using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace shopaoquan
{
    public class XMessage
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public XMessage () { }
        public XMessage(string type,string msg) {
            this.Type = type;
            this.Message=msg;
        }
    }
}