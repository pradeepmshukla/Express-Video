using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class vmFeedbackDetails
    {
        public string FeedbackComment { get; set; }
        public string DateTime { get; set; }
        public string FilesUploaded { get; set; }
        public string ClientStatus { get; set; }
    }
}