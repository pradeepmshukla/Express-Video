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
    public class AnnouncementController : Controller
    {
        // GET: Announcement
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMyAnnouncement()
        {
            try
            {
                Announcement obj = new Announcement();
                obj.ToUserId = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));
                if (GlobalFunctions.GetRoleId() == 2)
                {
                    obj.ToUserId = 0;
                }
                DataTable dt = obj._Select("procAnnouncement", "SELECT", obj).Tables[0];
                List<vmAnnouncement> objlist = GlobalFunctions.ConverDataTableToList<vmAnnouncement>(dt);
                return Json(objlist);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //[OutputCache(CacheProfile = "MyAnnouncement", Duration =60)]
        public ActionResult MyAnnouncement()
        { 
            return View();
        }
        public ActionResult DeleteAnnouncement(Announcement obj)
        {
            obj.ToUserId = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));
            DataTable dt = obj._Select("procAnnouncement", "DeleteAnnouncement", obj).Tables[0];
            return Json(Convert.ToString(dt.Rows[0][0]));
        }
        public ActionResult ViewAnnouncement(Announcement obj)
        {
            obj.ToUserId = Convert.ToInt32(Session["UserId"]);
            DataTable dt = obj._Select("procAnnouncement", "ViewAnnouncement", obj).Tables[0];
            return Json(Convert.ToString(dt.Rows[0][0]));
        }
    }
}