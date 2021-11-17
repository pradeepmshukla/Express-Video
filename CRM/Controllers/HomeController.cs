using CRM.Models;
using CRM.Models.AuthData;
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
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            SocialPage obj = new SocialPage();
            ViewBag.Title = "Home";
            return View();
        }
        [HttpPost]
        public JsonResult ReturnURL(string Email, string FirstName, string LastName, string GoogleID, string ProfileURL)
        {
            //Do your code for Signin or Signup

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Authentication]
        public ActionResult SampleVideo()
        {
            if (GlobalFunctions.GetUserId() == 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Sample Video";
            ViewBag.CustomerId = GlobalFunctions.GetUserId();

            PriceManagement objprice = new PriceManagement();
            DataTable dt = objprice._Select("procPriceManagement").Tables[0];
            ViewBag.PriceManagement = GlobalFunctions.ConverDataTableToList<PriceManagement>(dt);

            if (GlobalFunctions.GetRoleId() == 4)
            {
                Basket b = new Basket();
                b.CustomerId = GlobalFunctions.GetUserId();
                string msg=b._Select("procBasket", "CheckingForBasketAndAdd", b,true);
            }

            Basket basket = new Basket();
            basket.CustomerId = GlobalFunctions.GetUserId();
            basket =GlobalFunctions.ConverDataTableToList<Basket>(basket._Select("procBasket", "CheckingBasket", basket).Tables[0]).FirstOrDefault();
            return View(basket);
        }

        [Authentication]
        public ActionResult getSampleVideo()
        {
            SampleVideo objSample = new SampleVideo();
            DataTable dt= objSample._Select("procSampleVideo", "SELECTForUser").Tables[0];
            List <SampleVideo> lst = new List<SampleVideo>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SampleVideo obj = new SampleVideo();
                    obj.SampleVideoId = Convert.ToInt32(dt.Rows[i]["SampleVideoId"]);
                    obj.Name = Convert.ToString(dt.Rows[i]["Name"]);
                    obj.Title = Convert.ToString(dt.Rows[i]["Title"]);
                    obj.Description = Convert.ToString(dt.Rows[i]["Description"]);
                    obj.Price = Convert.ToDouble(dt.Rows[i]["Price"]);
                    obj.VideoUrl = Convert.ToString(dt.Rows[i]["VideoUrl"]);
                    obj.ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);
                    lst.Add(obj);
                }
            }
            return Json(lst);

        }

        [Authentication]
        public ActionResult Videos()
        {
            SampleVideo objSample = new SampleVideo();
            DataTable dt = objSample._Select("procSampleVideo", "SELECTForUser").Tables[0];
            List<SampleVideo> lst = new List<SampleVideo>();
            lst= GlobalFunctions.ConverDataTableToList<SampleVideo>(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        SampleVideo obj = new SampleVideo();
            //        obj.SampleVideoId = Convert.ToInt32(dt.Rows[i]["SampleVideoId"]);
            //        obj.Name = Convert.ToString(dt.Rows[i]["Name"]);
            //        obj.Title = Convert.ToString(dt.Rows[i]["Title"]);
            //        obj.Description = Convert.ToString(dt.Rows[i]["Description"]);
            //        obj.Price = Convert.ToDouble(dt.Rows[i]["Price"]);
            //        obj.VideoUrl = Convert.ToString(dt.Rows[i]["VideoUrl"]);
            //        obj.ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);
            //        obj.VideoCategory = Convert.ToString(dt.Rows[i]["VideoCategory"]);
            //        lst.Add(obj);
            //    }
            //}
            return View(lst);
            
        }

        //public ActionResult FAQ()
        //{
        //    return View();
        //}
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        public ActionResult TermAndConditions()
        {
            return View();
        }
        
        public ActionResult NewEnquiry()
        {
            return Json("");
        }
    }
}