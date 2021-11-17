using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Features:Base
    {
        public int FeaturesId { get; set; }

        public string FeatureName { get; set; }

        public string FeatureDescription { get; set; }

        public bool? IsEnabled { get; set; }

        
    }

    
}
