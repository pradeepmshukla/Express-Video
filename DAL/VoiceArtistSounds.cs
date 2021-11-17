using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VoiceArtistSounds:Base
    {
        public int VoiceArtistSoundsId { get; set; }
        public int? VoiceArtistUserId { get; set; }
        public string SoundName { get; set; }
        public string SoundType { get; set; }
        public string SoundFileName { get; set; }
        public double? Price { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string SoundLanguage { get; set; }
        public string SampleType { get; set; }

    }
}
