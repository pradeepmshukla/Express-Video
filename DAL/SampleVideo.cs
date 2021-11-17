using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SampleVideo:Base
    {
        public int SampleVideoId { get; set; }

        public string Name { get; set; }

        public double? Price { get; set; }

        public string VideoUrl { get; set; }
        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoCategory { get; set; }
        public string YoutubeLink { get; set; }
        public string VideoType { get; set; }
    }
}
