using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ExceptionLogs:Base
    {
        public long Logid { get; set; }

        public string ExceptionMsg { get; set; }

        public string ExceptionType { get; set; }

        public string ExceptionSource { get; set; }

        public string ExceptionURL { get; set; }

        public void Save(Exception ex,string url)
        {
            ExceptionLogs objex = new ExceptionLogs();
            objex.ExceptionMsg = ex.Message.ToString();
            objex.ExceptionType = ex.GetType().Name.ToString();
            objex.ExceptionURL = url;
            objex.ExceptionSource = ex.StackTrace.ToString();
            string msg=objex._Insert("procExceptionLogs",objex);
        }

    }
    
}
