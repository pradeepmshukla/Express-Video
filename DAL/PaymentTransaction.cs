using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PaymentTransaction:Base
    {
        public int PaymentTransactionid { get; set; }

        public string UserID { get; set; }

        public string OrderId { get; set; }

        public string OrderAmount { get; set; }

        public string ReferenceId { get; set; }

        public string TxStatus { get; set; }

        public string PaymentMode { get; set; }
        public string TxMsg { get; set; }

        public string TxTime { get; set; }

        public string Signature { get; set; }

        

    }
}
