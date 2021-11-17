using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL 
{
    public class UserDetails : Base
    {
        public int UserID { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailID { get; set; }

        public string MobileNo { get; set; }
        public string AlternateMobileNo { get; set; }

        public string DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }
        public int RoleId { get; set; }

        public string PanCard { get; set; }
        public string AadharCard { get; set; }
        public string BankAccountDetails { get; set; }
        public string ProfilePhoto { get; set; }
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

        public string SoundSampleHindi1 { get; set; }
        public string SoundSampleHindi2 { get; set; }
        public string SoundSampleEnglish1 { get; set; }
        public string SoundSampleEnglish2 { get; set; }

        public bool RegistrationByGoogle { get; set; }
        public bool LoginWithGmail { get; set; }

        public string GmailImageUrl { get; set; }
        public double? SoundSampleHindi1_Price { get; set; }
        public double? SoundSampleEnglish1_Price { get; set; }
        public bool IsAgreementAccept { get; set; }
        public double? ArtistRating { get; set; }
    }

}
