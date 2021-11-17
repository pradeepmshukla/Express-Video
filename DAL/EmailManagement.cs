using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmailManagement : Base
    {
        public int EmailMangementID { get; set; }

        public string UniqueKey { get; set; }
        public string EMTitle { get; set; }

        public string EMFrom { get; set; }
        public string EMSubject { get; set; }

        public string EMCC { get; set; }

        public string EMBCC { get; set; }

        public string EMBody { get; set; }
    }
    enum EmailTemplate
    {
        Template1,
        Template2,
        Template3,
        Template4

    }
}
