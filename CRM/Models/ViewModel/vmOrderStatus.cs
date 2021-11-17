using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class vmOrderStatus
    {
        public int OrderId { get; set; }
        public bool Script_Required { get; set; }
        public bool VO_Required { get; set; }
    }
}