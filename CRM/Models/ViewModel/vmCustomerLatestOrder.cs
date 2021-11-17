using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class vmCustomerLatestOrder
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public double Total { get; set; }
        public DateTime DateofOrder { get; set; }
        public DateTime DateofDelivery { get; set; }
    }
}