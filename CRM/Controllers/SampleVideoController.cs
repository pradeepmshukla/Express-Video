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
    public class SampleVideoController : BaseController
    {
        public ActionResult Index()
        {
            SampleVideo obj = new SampleVideo();
            return View(obj._Select("procSampleVideo").Tables[0]);
            
        }
        public ActionResult NewSample(string id)
        {
            string ID = Convert.ToString(id);
            SampleVideo obj = new SampleVideo();
            if (!string.IsNullOrEmpty(ID))
            {
                obj.SampleVideoId = Convert.ToInt32(ID);
                DataTable dt = obj._Select("procSampleVideo", "SELECT", obj).Tables[0];
                obj = GlobalFunctions.ConverDataTableToList<SampleVideo>(dt).FirstOrDefault();
                //if (dt.Rows.Count > 0)
                //{
                //    obj.Name = Convert.ToString(dt.Rows[0]["Name"]);
                //    obj.Price = Convert.ToDouble(dt.Rows[0]["Price"]);
                //    obj.Title = Convert.ToString(dt.Rows[0]["Title"]);
                //    obj.Description = Convert.ToString(dt.Rows[0]["Description"]);
                //    obj.VideoUrl = Convert.ToString(dt.Rows[0]["VideoUrl"]);
                //    obj.ImageUrl = Convert.ToString(dt.Rows[0]["ImageUrl"]);
                //    obj.VideoCategory = Convert.ToString(dt.Rows[0]["VideoCategory"]);
                //    obj.YoutubeLink= Convert.ToString(dt.Rows[0]["YoutubeLink"]);
                //}

            }
            return View(obj);
        }
        public ActionResult SampleDetails()
        {
            string ID = Convert.ToString(RouteData.Values["id"]);
            SampleVideo obj = new SampleVideo();
            if (!string.IsNullOrEmpty(ID))
            {
                try
                {
                    obj.SampleVideoId = Convert.ToInt32(ID);
                    DataTable dt = obj._Select("procSampleVideo", "SELECT", obj).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        obj.SampleVideoId = Convert.ToInt32(dt.Rows[0]["SampleVideoId"]);
                        obj.Name = Convert.ToString(dt.Rows[0]["Name"]);
                        obj.Price = Convert.ToDouble(dt.Rows[0]["Price"]);
                        obj.Title = Convert.ToString(dt.Rows[0]["Title"]);
                        obj.Description = Convert.ToString(dt.Rows[0]["Description"]);
                        obj.VideoUrl = Convert.ToString(dt.Rows[0]["VideoUrl"]);
                        obj.ImageUrl= Convert.ToString(dt.Rows[0]["ImageUrl"]);
                        obj.VideoCategory = Convert.ToString(dt.Rows[0]["VideoCategory"]);
                        obj.YoutubeLink = Convert.ToString(dt.Rows[0]["YoutubeLink"]);
                    }
                }
                catch (Exception e)
                {

                }

            }
            return View(obj);
        }
        public ActionResult SaveData(SampleVideo obj)
        {
            if (obj.SampleVideoId > 0)
            {
                return Json(obj._Update("procSampleVideo", obj));
            }
            else
            {
                return Json(obj._Insert("procSampleVideo", obj));
            }
        }
        public ActionResult AddToCart(Basket obj)
        {
            return Json(obj._Insert("procBasket", obj));
        }
        public ActionResult GetVoiceDetails()
        {
            return Json("");
        }
    }
}