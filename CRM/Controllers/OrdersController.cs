using CRM.Models.Global;
using CRM.Models.ViewModel;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult NewJobs()
        {
            Orders obj = new Orders();
            return View(obj._Select("procOrders", "NewJobs", obj).Tables[0]);

        }

        public ActionResult MyOrder()
        {
            Orders obj = new Orders();
            return View(obj._Select("procOrders", "MyOrder", obj).Tables[0]);

        }

        public ActionResult AdminOrder()
        {
            Orders obj = new Orders();
            return View(obj._Select("procOrders", "AdminOrder", obj).Tables[0]);
        }

        public ActionResult PopupOrderDetails(Orders obj)
        {
            DataTable dt = obj._Select("procOrders", "PopupOrderDetails", obj).Tables[0];
            List<vmPopupOrderDetails> vmList = GlobalFunctions.ConverDataTableToList<vmPopupOrderDetails>(dt);
            return Json(vmList);
        }
        public ActionResult _ScriptDetails(Orders obj)
        {
            if (GlobalFunctions.GetRoleId() == 4)
            {
                obj.CustomerId = GlobalFunctions.GetUserId();
            }
            
            DataTable dt = obj._Select("procOrders", "_ScriptDetails", obj).Tables[0];
            List<Orders> vmList = GlobalFunctions.ConverDataTableToList<Orders>(dt);
            return View(vmList.FirstOrDefault());
        }
        public ActionResult SaveScriptDetails(Orders obj)
        {
            if (obj.OrderId > 0)
            {
                return Json(obj._Select("procOrders", "SaveScriptDetails", obj, true));
            }
            else
            {
                obj.CustomerId = GlobalFunctions.GetUserId();
                return Json(obj._Select("procOrders", "SaveScriptDetails", obj, true));
            }
        }
        public ActionResult _CustomerInvoice(Orders obj)
        {
            List<vmMyCart> lstobjcart = new List<vmMyCart>();
            DataTable dt = obj._Select("procOrders", "_CustomerInvoice", obj).Tables[0];
            ViewBag.Date = dt.Rows[0]["CreatedDate"];
            ViewBag.OrderId = obj.OrderId;
            lstobjcart = GlobalFunctions.ConverDataTableToList<vmMyCart>(dt);
            return View(lstobjcart);
            
        }
        
    }
}