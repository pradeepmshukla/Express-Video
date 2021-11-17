using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class vmMyDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string AlternateMobileno { get; set; }
        public string ProfilePhoto { get; set; }
        public string PanCard { get; set; }
        public string AadharCard { get; set; }
        public string BankAccountDetails { get; set; }
        public string RoleId { get; set; }
        public string PanCard_text { get; set; }

        public string AadharCard_text { get; set; }

        public string BankName { get; set; }

        public string AccountHolderName { get; set; }

        public string AccountNumber { get; set; }

        public string IFSC { get; set; }

        public string BranchName { get; set; }

        public bool? IsPancardApproved { get; set; }

        public bool? IsAadharCardApproved { get; set; }

        public bool? IsBankDetailsApproved { get; set; }

        //public List<VoiceArtistSounds> VoiceArtistSounds { get; set; }
    }
}