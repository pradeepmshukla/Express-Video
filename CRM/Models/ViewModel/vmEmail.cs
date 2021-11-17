using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class vmEmail
    {
        public string EmailTo { get; set; }
        public string EmailTemplate { get; set; }

        /*Replace Content*/
        public string OrderId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string MessageInfo { get; set; }
        public string EmailId { get; set; }
        public string TotalDays { get; set; }

    }
}