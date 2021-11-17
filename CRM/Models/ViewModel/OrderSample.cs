using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class OrderSample
    {
        public SampleVideos SampleVideos { get; set; }
        public ScriptWriter ScriptWriter { get; set; }
        public ExtraDetails ExtraDetails { get; set; }
        public VO VO { get; set; }
        public double TotalAmount { get; set; }
        public int CustomerId { get; set; }
    }
    public class SampleVideos
    {
        public int SampleVideoId { get; set; }
        public double SampleVideoPrice { get; set; }
        public string VideoResolution { get; set; }
    }
    public class ScriptWriter
    {
        public bool Script_Required { get; set; }
        public string Script_VideoConcept { get; set; }
        public string Script_PurposeOfVideo { get; set; }
        public string Script_AboutCompany { get; set; }
        public string Script_BenifitesForCustomer { get; set; }
        public string Script_TargetAudience { get; set; }
        public string Script_CompanyName { get; set; }
        public string Script_CompanyWebsite { get; set; }
        public string Script_LogoName { get; set; }
        public double Script_Price { get; set; }
        public string Script_ProductServiceImages { get; set; }
    }
    public class VO
    {
        public bool VO_Required { get; set; }
        public string VO_Language { get; set; }
        public string VO_Gender { get; set; }
        public string VO_ArtistSamleType { get; set; }
        public int VO_SampleId { get; set; }
        public double VO_SamplePrice { get; set; }
    }
    public class ExtraDetails
    {
        public string ED_VideoDuration { get; set; }
        public string ED_ExtraDetails { get; set; }
        public string ED_FileName { get; set; }
    }

}