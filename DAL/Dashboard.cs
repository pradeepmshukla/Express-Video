using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Dashboard:Base
    {
        public int? UserId { get; set; }
        public int? TotalNoOfProject { get; set; }
        public double? TotalRevenue { get; set; }
        public int? TotalNoOfCustomer { get; set; }
        public int? TotalNoOfOrders { get; set; }
        public double TotalEarning { get; set; }
        public double ThisMonthEarning { get; set; }
        public double ThisMonthProject { get; set; }
        public double YearlyRevenue { get; set; }
        public double MonthalyRevenue { get; set; }
        public double TotalNoOfOrdersYearly { get; set; }
        public double TotalNoOfOrdersMonthaly { get; set; }
    }
    public class chartdata
    {
        public int TotalOrders { get; set; }
    }
}
