using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class vmAnnouncement
    {
        public int Announcementid { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public int? FromUserId { get; set; }
        public string FromUser { get; set; }

        public int? ToUserId { get; set; }

        public string ToUser { get; set; }
        public bool? IsView { get; set; }

        public bool? IsDelete { get; set; }
        public string CreatedDate { get; set; }
    }
}