using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EnquiryDetails:Base
    {
        public int EnquiryDetailsId { get; set; }

        public string Name { get; set; }

        public string Emailid { get; set; }

        public string MobileNo { get; set; }

        public string MessageInfo { get; set; }
            

    }
}
