using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FeedbackDetails:Base
    {
        public int FeedbackDetailsId { get; set; }

        public int? OrderId { get; set; }

        public int? SendFrom { get; set; }

        public int? SentTo { get; set; }

        public string FeedbackComments { get; set; }

        public string FilesUploaded { get; set; }
        public string FeedbackType { get; set; }
        public string ClientStatus {get;set;}
    }
}
