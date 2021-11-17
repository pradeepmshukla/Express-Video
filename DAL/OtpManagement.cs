using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OtpManagement:Base
    {
        public int OtpId { get; set; }

        public string MobileNo { get; set; }

        public string EmailId { get; set; }

        public string OTP { get; set; }

        public string SMS { get; set; }

        public bool? IsOtpVarify { get; set; }

        public bool? IsOtpExpire { get; set; }

        public int? ValidTime { get; set; }

    }
}
