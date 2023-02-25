using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entitys
{
    internal class EventArgsExcep
    {
        public string Warning { get; set; }
        public Exception exception { get; set; }
        public EventArgsExcep(Exception ex = null,string warningMsg = null)
        {
            exception = ex;
            Warning = warningMsg;
        }
    }
}
