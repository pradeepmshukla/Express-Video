using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FileApprovalStatus:Base
    {
        public int FileApprovalId { get; set; }

        public int? UserId { get; set; }

        public int? OrderId { get; set; }

        public string FileName { get; set; }

        public string FileStatus { get; set; }

        public string ClientFeedback { get; set; }

        public string ClientStatus { get; set; }
        public string FileUploaded { get; set; }
        public string FeedbackType { get; set; }

    }

}
