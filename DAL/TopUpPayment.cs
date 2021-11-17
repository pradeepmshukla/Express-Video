using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TopUpPayment:Base
    {
        public int TopUpPaymentId { get; set; }

        public int? OrderId { get; set; }

        public string AdditionalDuration { get; set; }

        public double? ScriptCharges { get; set; }

        public double? VioceOverCharges { get; set; }

        public double? VideoCharges { get; set; }

        public bool? PaymentStatus { get; set; }

        public string PaymentDetails { get; set; }

        public DateTime? PaymentDate { get; set; }
         
        public double? AdditionalServiceCharegs { get; set; }
        public double? DiscountCharges { get; set; }
    }
}
