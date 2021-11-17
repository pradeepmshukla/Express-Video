using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class vmVoiceList
    {
        public int VoiceArtistSoundsId { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string SoundFileName { get; set; }
        public string SoundName { get; set; }
        public string SoundType { get; set; }
        public double Price { get; set; }
        public string Gender { get; set; }
        public string SoundLanguage { get; set; }
    }
}