using CRM.Models.Global;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
    
namespace CRM.Controllers
{
    public class TopUpPaymentController : Controller
    {
        // GET: TopUpPayment
        public ActionResult Index()
        {
            TopUpPayment obj = new TopUpPayment();
            return View(obj._Select("procTopUpPayment").Tables[0]);
        }

        public ActionResult _TopUprequest(TopUpPayment obj)
        {
            PriceManagement objprice = new PriceManagement();
            List<PriceManagement> lstPrice = GlobalFunctions.ConverDataTableToList<PriceManagement>(objprice._Select("procPriceManagement").Tables[0]);
            ViewBag.PriceList = lstPrice;
            return View(obj);
        }
        public ActionResult _TopUpInvoice(TopUpPayment obj)
        {
            DataSet ds = obj._Select("procTopUpPayment", "_TopUpInvoice", obj);
            obj = GlobalFunctions.ConverDataTableToList<TopUpPayment>(ds.Tables[0]).FirstOrDefault();
            Orders order= GlobalFunctions.ConverDataTableToList<Orders>(ds.Tables[1]).FirstOrDefault();
            ViewBag.Data = ds.Tables[0];
            ViewBag.Orders = order;
            GlobalFunctions.SetCookie("TopUpPaymentOrderId", order.OrderId.ToString());
            return View(obj);
        }
        public ActionResult UpdateTopUpPrice(TopUpPayment obj)
        {
            string msg=obj._Select("procTopUpPayment", "UpdateTopUpPrice", obj, true);
            return Json(msg);
        }
        public ActionResult SentTopUpRequest(TopUpPayment obj)
        {
            string msg = obj._Insert("procTopUpPayment", obj);
            return Json(msg);
        }
         
    }
}