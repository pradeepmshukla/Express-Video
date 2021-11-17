using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Announcement:Base
    {
        public int Announcementid { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public int? FromUserId { get; set; }

        public int? ToUserId { get; set; }

        public bool? IsView { get; set; }

        public bool? IsDelete { get; set; }
    }
}
