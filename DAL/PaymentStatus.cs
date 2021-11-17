using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PaymentStatus:Base
    {
        public int PaymentId { get; set; }

        public int? UserId { get; set; }

        public double? Amount { get; set; }

        public string PaymentBy { get; set; }

        public int? OrderId { get; set; }

        public string PaymentComment { get; set; }
    }
}
