using CRM.Models.Global;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class CustomerFeedbackController : Controller
    {
        // GET: CustomerFeedback
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GiveFeedback(int id)
        {
            CustomerFeedback obj = new CustomerFeedback();
            obj.OrderId = id;
            return View(obj);
        }
        public ActionResult SaveFeedback(CustomerFeedback obj)
        {
            string msg=obj._Insert("procCustomerFeedback", obj);
            return View(msg);
        }
        public ActionResult ViewCustomerFeedback(CustomerFeedback obj)
        {
            obj = GlobalFunctions.ConverDataTableToList<CustomerFeedback>(obj._Select("procCustomerFeedback", "ViewCustomerFeedback", obj).Tables[0]).FirstOrDefault();
            return View(obj);
        }
    }
}