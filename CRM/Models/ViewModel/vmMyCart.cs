using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class vmMyCart
    {
        public int BasketId { get; set; }
        public int CustomerId { get; set; }
        public int SampleVideoId {get;set;}
        public double? VideosPrice { get; set; }
        public string ED_VideoDuration { get; set; }
        public string ED_ExtraDetails { get; set; }
		public bool Script_Required { get; set; }
        public double? Script_Price { get; set; }
        public bool VO_Required { get; set; }
        public double? VO_SamplePrice { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string VideoUrl { get; set; }
        public string YoutubeLink { get; set; }
        public string ImageUrl { get; set; }
        public string  Title { get; set; }
        public string  Description { get; set; }
        public string VideoResolution { get; set; }
        public string BillTo { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string GSTIN { get; set; }
        public double? IGST { get; set; }
        public double? CGST { get; set; }
        public double? SGST { get; set; }
        public double? FinalTotal { get; set; }
        


    }
}