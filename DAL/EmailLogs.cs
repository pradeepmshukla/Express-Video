using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmailLogs:Base
    {
        public int EmailLogsId { get; set; }

        public int? EmailMangementID { get; set; }

        public string EMTitle { get; set; }

        public string EMFrom { get; set; }

        public string EMSubject { get; set; }

        public string EMTO { get; set; }

        public string EMCC { get; set; }

        public string EMBCC { get; set; }

        public string EMBody { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool? IsSent { get; set; }

        public DateTime? SentDatetime { get; set; }
        public string Status { get; set; }
    }
}
