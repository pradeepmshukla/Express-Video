using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class vmPopupOrderDetails
    {
        public int OrderId { get; set; }
        public string Title { get; set; }
        public string VideoLink { get; set; }
        public double SampleVideoPrice { get; set; }
        public string Description { get; set; }
        public bool Script_Required { get; set; }
        public string Script_VideoConcept { get; set; }
        public string Script_PurposeOfVideo { get; set; }
        public string Script_AboutCompany { get; set; }
        public string Script_BenifitesForCustomer { get; set; }
        public string Script_TargetAudience { get; set; }
        public string Script_CompanyName { get; set; }
        public string Script_CompanyWebsite { get; set; }
        public string Script_LogoName { get; set; }
        public string Script_ProductServiceImages { get; set; }
        public double Script_Price { get; set; }
        public bool VO_Required { get; set; }
        public string VO_Language { get; set; }
        public string VO_Gender { get; set; }
        public string VO_ArtistSamleType { get; set; }
        public int VO_SampleId { get; set; }
        public double VO_SamplePrice { get; set; }
        public string ED_VideoDuration { get; set; }
        public string ED_ExtraDetails { get; set; }
        public string ED_FileName { get; set; }
        public double OrderTotal { get; set; }
        public double GrossAmount { get; set; }
        public double? IGST { get; set; }
        public double? CGST { get; set; }
        public double? SGST { get; set; }
        public double? FinalTotal { get; set; }
        public string CompanyName { get; set; }
        public string GSTIN { get; set; }
    }
}