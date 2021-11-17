using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class vmOrderDetails
    {
        public int OrderId { get; set; }
        public int? ScriptStatus { get; set; }
        public int? VOStatus { get; set; }
        public int? VideoStatus { get; set; }

        public string ScriptFileName { get; set; }
        public string VOFileName { get; set; }
        public string VideoFileName { get; set; }

        public string ScriptFileName_1 { get; set; }
        public string VOFileName_1 { get; set; }
        public string VideoFileName_1 { get; set; }

        public string ScriptFileName_2 { get; set; }
        public string VOFileName_2 { get; set; }
        public string VideoFileName_2 { get; set; }

        public string ScriptFileName_3 { get; set; }
        public string VOFileName_3 { get; set; }
        public string VideoFileName_3 { get; set; }

        public bool? IsScriptPaymentCleared { get; set; }

        public bool? IsVoicePaymentCleared { get; set; }

        public bool? IsVideoPaymentCleared { get; set; }

    }
}