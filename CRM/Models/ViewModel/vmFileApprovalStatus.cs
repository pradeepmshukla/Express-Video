using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class vmFileApprovalStatus
    {
        public int FileApprovalId { get; set; }

        public int? UserId { get; set; }

        public int? OrderId { get; set; }

        public string FileName { get; set; }

        public string FileStatus { get; set; }

        public string ClientFeedback { get; set; }

        public string ClientStatus { get; set; }
        public string CreatedDate { get; set; }
    }
}