using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderSentToArtist:Base
    {
        public int OrderSentToArtistId { get; set; }

        public int? UserId { get; set; }

        public int? OrderId { get; set; }

        public bool? IsAccepted { get; set; }

        public int? RoleId { get; set; }

    }
}
