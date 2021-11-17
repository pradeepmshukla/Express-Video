using CRM.Models;
using CRM.Models.Global;
using CRM.Models.ViewModel;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class EnquiryDetailsController : Controller
    {
        // GET: EnquiryDetails
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewEnquiry(EnquiryDetails obj)
        {
             string msg = obj._Insert("procEnquiryDetails", obj);

            vmEmail objmail = new vmEmail();
            objmail.EmailId =obj.Emailid ;
            objmail.Name= obj.Name;
            objmail.MobileNo = obj.MobileNo;
            objmail.MessageInfo = obj.MessageInfo;
            objmail.EmailTemplate = "Template1";
            objmail.EmailTo = "info@expressvideo.in";
            Email email = new Email();
            email.SendMail(objmail);

            GlobalFunctions.AddAnnouncement("New Enquiry", "New Enquiry with emaild : "+ obj.Emailid, 0, 0);
            return Json(msg);

        }
    }
}