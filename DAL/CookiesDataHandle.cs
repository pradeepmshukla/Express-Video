using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CookiesDataHandle:Base
    {
        public int CDH { get; set; }

        public string GeneratedOrderId { get; set; }

        public int? UserId { get; set; }

        public int? BasketId { get; set; }

        public int? TopUpPaymentOrderId { get; set; }

    }
}
