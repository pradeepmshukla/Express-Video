﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models.ViewModel
{
    public class vmResetPassword
    {
        public string MobileNo { get; set; }
        public string OTP { get; set; }
        public string Password { get; set; }
    }
}