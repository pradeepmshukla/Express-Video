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
    public class PriceManagementController : Controller
    {
        // GET: PriceManagement
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult PriceManage()
        {
            PriceManagement obj = new PriceManagement();
            DataTable dt = obj._Select("procPriceManagement").Tables[0];
            List<PriceManagement> objList = GlobalFunctions.ConverDataTableToList<PriceManagement>(dt);
            ViewBag.objList = objList;
            return View();
        }
        public ActionResult PriceSave(PriceManagement obj)
        {
            string msg = "";
            if (obj.PriceManagementId > 0)
            {
                msg = Convert.ToString(obj._Select("procPriceManagement", "UPADTE", obj).Tables[0].Rows[0][0]);
            }
            else
            {
                
                msg = obj._Insert("procPriceManagement", obj);
            }
            return Json(obj);
        }
    }
}