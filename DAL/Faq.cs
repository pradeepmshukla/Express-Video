using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Faq:Base
    {
        public int FaqId { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

       

    }
}
