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
    public class OrderSentToArtistController : Controller
    {
        // GET: OrderSentToArtist
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SendOrderToArtist(OrderSentToArtist obj)
        {
            
            string EmailId=obj._Select("procOrderSentToArtist", "SendOrderToArtist", obj,true);
            if (!string.IsNullOrEmpty(EmailId) && EmailId.ToLower()!= "NotSaved".ToLower())
            {
                vmEmail objvm = new vmEmail();
                objvm.EmailTemplate = "Template5";
                objvm.EmailTo = EmailId;
                Email mail = new Email();
                mail.SendMail(objvm);
                GlobalFunctions.AddAnnouncement("Project sent to Artist ", "Project sent to "+EmailId +" for Oredr No: "+ obj.OrderId, 0, 0);
                GlobalFunctions.AddAnnouncement("Congratulation! New Freelance Opportunity", "Congratulation! There is a new freelancer opportunity available for you on expressvideo that match your profile with Order No. " + obj.OrderId, 0, obj.UserId);
            }
            return Json("Mailed to selected artist.");

        }
    }
}