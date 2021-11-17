using CRM.Models.AuthData;
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
    public class FaqController : Controller
    {

        // GET: Faq
        [Authentication]
        [Authorized]
        public ActionResult Index()
        {
            Faq obj = new Faq();
            return View(obj._Select("procFaq").Tables[0]);
        }

        public ActionResult Faq()
        {
            Faq obj = new Faq();
            return View(GlobalFunctions.ConverDataTableToList<Faq>(obj._Select("procFaq").Tables[0]));
        }

        [Authentication]
        [Authorized]
        public ActionResult Create(string id)
        {
            string ID = Convert.ToString(id);
            Faq obj = new Faq();
            if (!string.IsNullOrEmpty(ID))
            {
                obj.FaqId = Convert.ToInt32(ID);
                DataTable dt = obj._Select("procFaq", "SELECT", obj).Tables[0];
                obj = GlobalFunctions.ConverDataTableToList<Faq>(dt).FirstOrDefault();
            }
            return View(obj);
        }

        [Authentication]
        [Authorized]
        public ActionResult SaveFaQ(Faq obj)
        {
            if (obj.FaqId > 0)
            {
                return Json(obj._Update("procFaq", obj));
            }
            else
            {
                return Json(obj._Insert("procFaq", obj));
            }

        }

        [Authentication]
        [Authorized]
        public ActionResult InActiveFaq(Faq obj)
        {
            return Json(obj._Select("procFaq", "InActiveFaq",obj,true));
        }

    }
}