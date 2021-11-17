using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PriceManagement : Base
    {
        public int PriceManagementId { get; set; }

        public string VideoDuration { get; set; }

        public int? AmountPercentage { get; set; }

        public double? VideoCharges { get; set; }

        public double? ScriptCharges { get; set; }

        public double? VOCharges_Regular { get; set; }

        public double? VOCharges_Premium { get; set; }

        public double? VideoCharges_Distribute { get; set; }

        public double? ScriptCharges_Distribute { get; set; }

        public double? VOCharges_Regular_Distribute { get; set; }

        public double? VOCharges_Premium_Distribute { get; set; }
        
        public int? ScriptDays { get; set; }
        public int? VODays { get; set; }
        public int? VideoDays_Level01 { get; set; }
        public int? VideoDays_Level02 { get; set; }
        public int? VideoDays_Level03 { get; set; }
        

    }



}
