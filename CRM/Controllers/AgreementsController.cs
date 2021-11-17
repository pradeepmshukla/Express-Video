using CRM.Models.Global;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class AgreementsController : Controller
    {
        
        public ActionResult _FreelancerAGREEMENT_ForScriptWriter()
        {
            return View();
        }
        public ActionResult _FreelancerWorkAGREEMENT_ForScriptWriter(OrderStatus obj)
        {
            ViewBag.OrderStatus=obj._Select("procOrderStatus", "WorkAgrrementDetails", obj).Tables[0];
            Orders objOrder = new Orders();
            objOrder.OrderId = (int)obj.OrderId;
            ViewBag.Orders = objOrder._Select("procOrders", "SELECT", objOrder).Tables[0];
            return View();
        }

        public ActionResult _FreelancerAGREEMENT_ForVideo()
        {
            return View();
        }
        public ActionResult _FreelancerWorkAGREEMENT_ForVideo(OrderStatus obj)
        {
            ViewBag.OrderStatus = obj._Select("procOrderStatus", "WorkAgrrementDetails", obj).Tables[0];
            Orders objOrder = new Orders();
            objOrder.OrderId = (int)obj.OrderId;
            ViewBag.Orders = objOrder._Select("procOrders", "SELECT", objOrder).Tables[0];
            return View(obj);
        }


        public ActionResult _FreelancerAGREEMENT_ForVoiceOver()
        {
            return View();
        }
              
        public ActionResult _FreelancerWorkAGREEMENT_ForVoiceOver(OrderStatus obj)
        {
            ViewBag.OrderStatus = obj._Select("procOrderStatus", "WorkAgrrementDetails", obj).Tables[0];
            Orders objOrder = new Orders();
            objOrder.OrderId = (int)obj.OrderId;
            ViewBag.Orders = objOrder._Select("procOrders", "SELECT", objOrder).Tables[0];
            return View(obj);
        }
        public ActionResult AcceptAgreement(UserDetails obj)
        {
            obj.UserID = GlobalFunctions.GetUserId();
            obj.IsAgreementAccept = true;
            string msg=obj._Select("procUserDetails", "AcceptAgreement", obj, true);
            GlobalFunctions.SetCookie("IsAgreementAccept", "true");
            return Json(msg);
        }

    }
}