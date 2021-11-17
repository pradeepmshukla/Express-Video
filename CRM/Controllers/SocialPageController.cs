using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models.AuthData;
using CRM.Models.Global;
using DAL;

namespace CRM.Controllers
{
    public class SocialPageController : Controller
    {
        // GET: SocialPage
        [Authentication]
        [Authorized]
        public ActionResult Index()
        {
            SocialPage obj = new SocialPage();
            return View(obj._Select("procSocialPage").Tables[0]);
        }
        [Authentication]
        [Authorized]
        public ActionResult Create(string id)
        {
            SocialPage obj = new SocialPage();
            string ID = Convert.ToString(id);
            if (!string.IsNullOrEmpty(ID))
            {
                obj.SocialPAgeId = Convert.ToInt32(id);
                obj = GlobalFunctions.ConverDataTableToList<SocialPage>(obj._Select("procSocialPage", "SELECT", obj).Tables[0]).FirstOrDefault();
            }
            return View(obj);
        }
        [Authentication]
        [Authorized]
        public ActionResult SaveSocialPage(SocialPage socialPage)
        {
            if (socialPage.SocialPAgeId > 0)
            {
                return Json(socialPage._Update("procSocialPage", socialPage));
            }
            else
            {
                return Json(socialPage._Insert("procSocialPage", socialPage));
            }
            
        }

        [Authentication]
        [Authorized]
        public ActionResult InActiveSocialPage(Faq obj)
        {
            return Json(obj._Select("procSocialPage", "InActiveSocialPage", obj, true));
        }
    }
}