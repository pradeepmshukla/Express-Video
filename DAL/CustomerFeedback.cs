using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CustomerFeedback:Base
    {
        public int CustomerFeedbackId { get; set; }

        public int? OrderId { get; set; }

        public double? ScriptRating { get; set; }

        public double? VoiceOverRating { get; set; }

        public double? VideoRating { get; set; }
        public double? OverAllRating { get; set; }
        public string Comments { get; set; }
    }
}
