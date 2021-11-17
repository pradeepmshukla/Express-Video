using CRM.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class OtpManagementController : Controller
    {
        // GET: OtpManagement
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SentOtp(OtpManagement obj)
        {
            string msg = obj._Insert("procOtpManagement", obj);
            SMS objsms = new SMS();
            objsms.sendSMS(obj.MobileNo,msg);
            return Json("Otp Sent on "+obj.MobileNo);
        }
        public ActionResult SentOtpForgotPassword(OtpManagement obj)
        {
            UserDetails objuser = new UserDetails();
            objuser.MobileNo = obj.MobileNo;
            string msg = objuser._Select("procUserDetails", "IsUserExists", objuser, true);
            if (msg == "true")
            {
                msg = obj._Insert("procOtpManagement", obj);
                SMS objsms = new SMS();
                objsms.sendSMS(obj.MobileNo, msg);
                return Json("Otp Sent on " + obj.MobileNo);
            }
            else
            {
                return Json(msg);
            }
                       
        }
        public ActionResult VarifyOTP(OtpManagement obj)
        {
            string msg = obj._Select("procOtpManagement", "VarifyOTP", obj,true);
            return Json(msg);
        }
         
    }
}