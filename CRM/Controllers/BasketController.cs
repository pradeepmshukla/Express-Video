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
    public class BasketController : Controller
    {
        // GET: Basket
        public ActionResult MyCart()
        {
            ViewBag.Title = "My Cart";
            Basket obj = new Basket();
            List<vmMyCart> lstobjcart = new List<vmMyCart>();
            List<Basket> lstBasket = new List<Basket>();
            if (GlobalFunctions.GetUserId()>0)
            {
                obj.CustomerId = Convert.ToInt32(GlobalFunctions.GetUserId());
                DataTable dt = obj._Select("procBasket", "SELECT", obj).Tables[0];
                obj = GlobalFunctions.ConverDataTableToList<Basket>(dt).FirstOrDefault();

                //if (obj.CustomerId > 0)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        for (int i = 0; i < dt.Rows.Count; i++)
                //        {
                //            vmMyCart objcart = new vmMyCart();
                //            objcart.BasketId = Convert.ToInt32(dt.Rows[i]["BasketId"]);
                //            objcart.CustomerId = Convert.ToInt32(dt.Rows[i]["CustomerId"]);
                //            objcart.SampleVideoId = Convert.ToInt32(dt.Rows[i]["SampleVideoId"]);
                //            objcart.SampleVideoPrice = Convert.ToDouble(dt.Rows[i]["SampleVideoPrice"]);
                //            objcart.SampleVideoUrl = Convert.ToString(dt.Rows[i]["VideoUrl"]);
                //            objcart.SampleImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);

                //            objcart.Script_Required = Convert.ToBoolean(dt.Rows[i]["Script_Required"]);
                //            if (objcart.Script_Required == true)
                //            {
                //                objcart.Script_VideoConcept = Convert.ToString(dt.Rows[i]["Script_VideoConcept"]);
                //                objcart.Script_PurposeOfVideo = Convert.ToString(dt.Rows[i]["Script_PurposeOfVideo"]);
                //                objcart.Script_AboutCompany = Convert.ToString(dt.Rows[i]["Script_AboutCompany"]);
                //                objcart.Script_BenifitesForCustomer = Convert.ToString(dt.Rows[i]["Script_BenifitesForCustomer"]);
                //                objcart.Script_TargetAudience = Convert.ToString(dt.Rows[i]["Script_TargetAudience"]);
                //                objcart.Script_CompanyName = Convert.ToString(dt.Rows[i]["Script_CompanyName"]);
                //                objcart.Script_CompanyWebsite = Convert.ToString(dt.Rows[i]["Script_CompanyWebsite"]);
                //                objcart.Script_LogoName = Convert.ToString(dt.Rows[i]["Script_LogoName"]);
                //                objcart.Script_ProductServiceImages = Convert.ToString(dt.Rows[i]["Script_ProductServiceImages"]);
                //                objcart.Script_Price = Convert.ToDouble(dt.Rows[i]["Script_Price"]);
                //            }

                //            objcart.VO_Required = Convert.ToBoolean(dt.Rows[i]["VO_Required"]);
                //            if (objcart.VO_Required == true)
                //            {
                //                objcart.VO_Language = Convert.ToString(dt.Rows[i]["VO_Language"]);
                //                objcart.VO_Gender = Convert.ToString(dt.Rows[i]["VO_Gender"]);
                //                objcart.VO_ArtistSamleType = Convert.ToString(dt.Rows[i]["VO_ArtistSamleType"]);
                //                objcart.VO_SampleId = Convert.ToInt32(dt.Rows[i]["VO_SampleId"]);
                //                objcart.SoundFileName = Convert.ToString(dt.Rows[i]["SoundFileName"]);
                //                objcart.VO_SamplePrice = Convert.ToDouble(dt.Rows[i]["VO_SamplePrice"]);
                //            }
                //            objcart.ED_VideoDuration = Convert.ToString(dt.Rows[i]["ED_VideoDuration"]);
                //            objcart.ED_ExtraDetails = Convert.ToString(dt.Rows[i]["ED_ExtraDetails"]);
                //            objcart.ED_FileName = Convert.ToString(dt.Rows[i]["ED_FileName"]);

                //            objcart.Name = Convert.ToString(dt.Rows[i]["Name"]);
                //            objcart.Price = Convert.ToDouble(dt.Rows[i]["Price"]);
                //            objcart.VideoUrl = Convert.ToString(dt.Rows[i]["VideoUrl"]);
                //            objcart.YoutubeLink = Convert.ToString(dt.Rows[i]["YoutubeLink"]);
                //            objcart.ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);
                //            objcart.Title = Convert.ToString(dt.Rows[i]["Title"]);
                //            objcart.Description = Convert.ToString(dt.Rows[i]["Description"]);

                //            objcart.IGST =  Convert.ToDouble(dt.Rows[i]["IGST"]);
                //            objcart.CGST =  Convert.ToDouble(dt.Rows[i]["CGST"]);
                //            objcart.SGST =  Convert.ToDouble(dt.Rows[i]["SGST"]);
                //            objcart.FinalTotal = Convert.ToDouble(dt.Rows[i]["FinalTotal"]);

                //            objcart.CompanyName = Convert.ToString(dt.Rows[i]["CompanyName"]);
                //            objcart.GSTIN= Convert.ToString(dt.Rows[i]["GSTIN"]);
                //            objcart.VideoResolution = Convert.ToString(dt.Rows[i]["VideoResolution"]);

                //            objcart.VideoDays = Convert.ToInt32(dt.Rows[i]["VideoDays"]);
                //            objcart.ScriptDays = Convert.ToInt32(dt.Rows[i]["ScriptDays"]);
                //            objcart.VODays = Convert.ToInt32(dt.Rows[i]["VODays"]);
                //            lstobjcart.Add(objcart);
                //        }


                //    }


                //}
            }
            //return View(lstobjcart);
            return View(obj);
        }
        //public ActionResult MyCartForEdit()
        //{
        //    Basket obj = new Basket();
        //    List<vmMyCart> lstobjcart = new List<vmMyCart>();
        //    if (!string.IsNullOrEmpty(GlobalFunctions.GetCookie("UserID"))) ;
        //    {
        //        obj.CustomerId = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));

        //        if (obj.CustomerId > 0)
        //        {
        //            DataTable dt = obj._Select("procBasket", "SELECT", obj).Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dt.Rows.Count; i++)
        //                {
        //                    vmMyCart objcart = new vmMyCart();
        //                    objcart.BasketId = Convert.ToInt32(dt.Rows[i]["BasketId"]);
        //                    objcart.CustomerId = Convert.ToInt32(dt.Rows[i]["CustomerId"]);
        //                    objcart.SampleVideoId = Convert.ToInt32(dt.Rows[i]["SampleVideoId"]);
        //                    objcart.SampleVideoPrice = Convert.ToDouble(dt.Rows[i]["SampleVideoPrice"]);
        //                    objcart.SampleVideoUrl = Convert.ToString(dt.Rows[i]["VideoUrl"]);
        //                    objcart.SampleImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);

        //                    objcart.Script_Required = Convert.ToBoolean(dt.Rows[i]["Script_Required"]);
        //                    objcart.Script_VideoConcept = Convert.ToString(dt.Rows[i]["Script_VideoConcept"]);
        //                    objcart.Script_PurposeOfVideo = Convert.ToString(dt.Rows[i]["Script_PurposeOfVideo"]);
        //                    objcart.Script_AboutCompany = Convert.ToString(dt.Rows[i]["Script_AboutCompany"]);
        //                    objcart.Script_BenifitesForCustomer = Convert.ToString(dt.Rows[i]["Script_BenifitesForCustomer"]);
        //                    objcart.Script_TargetAudience = Convert.ToString(dt.Rows[i]["Script_TargetAudience"]);
        //                    objcart.Script_CompanyName = Convert.ToString(dt.Rows[i]["Script_CompanyName"]);
        //                    objcart.Script_CompanyWebsite = Convert.ToString(dt.Rows[i]["Script_CompanyWebsite"]);
        //                    objcart.Script_LogoName = Convert.ToString(dt.Rows[i]["Script_LogoName"]);
        //                    objcart.Script_ProductServiceImages = Convert.ToString(dt.Rows[i]["Script_ProductServiceImages"]);
        //                    objcart.Script_Price = Convert.ToDouble(dt.Rows[i]["Script_Price"]);

        //                    objcart.VO_Required = Convert.ToBoolean(dt.Rows[i]["VO_Required"]);
        //                    objcart.VO_Language = Convert.ToString(dt.Rows[i]["VO_Language"]);
        //                    objcart.VO_Gender = Convert.ToString(dt.Rows[i]["VO_Gender"]);
        //                    objcart.VO_ArtistSamleType = Convert.ToString(dt.Rows[i]["VO_ArtistSamleType"]);
        //                    objcart.VO_SampleId = Convert.ToInt32(dt.Rows[i]["VO_SampleId"]);
        //                    objcart.SoundFileName = Convert.ToString(dt.Rows[i]["SoundFileName"]);
        //                    objcart.VO_SamplePrice = Convert.ToDouble(dt.Rows[i]["VO_SamplePrice"]);

        //                    objcart.ED_VideoDuration = Convert.ToString(dt.Rows[i]["ED_VideoDuration"]);
        //                    objcart.ED_ExtraDetails = Convert.ToString(dt.Rows[i]["ED_ExtraDetails"]);
        //                    objcart.ED_FileName = Convert.ToString(dt.Rows[i]["ED_FileName"]);

        //                    objcart.Name = Convert.ToString(dt.Rows[i]["Name"]);
        //                    objcart.Price = Convert.ToDouble(dt.Rows[i]["Price"]);
        //                    objcart.VideoUrl = Convert.ToString(dt.Rows[i]["VideoUrl"]);
        //                    objcart.YoutubeLink = Convert.ToString(dt.Rows[i]["YoutubeLink"]);
        //                    objcart.ImageUrl = Convert.ToString(dt.Rows[i]["ImageUrl"]);
        //                    objcart.Title = Convert.ToString(dt.Rows[i]["Title"]);
        //                    objcart.Description = Convert.ToString(dt.Rows[i]["Description"]);


        //                    lstobjcart.Add(objcart);
        //                }


        //            }
        //        }
        //    }
        //    return Json(lstobjcart);
        //}
        public ActionResult Payment()
        { 
            return View("");
        }
        public ActionResult AddToCart(OrderSample obj)
        {
                        
                Basket objb = new Basket();
                objb.CustomerId = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));
                objb.SampleVideoId = obj.SampleVideos.SampleVideoId;
                objb.SampleVideoPrice = obj.SampleVideos.SampleVideoPrice;
                objb.VideoResolution = obj.SampleVideos.VideoResolution;
                objb.Script_Required = obj.ScriptWriter.Script_Required;
                objb.Script_VideoConcept = obj.ScriptWriter.Script_VideoConcept;
                objb.Script_PurposeOfVideo = obj.ScriptWriter.Script_PurposeOfVideo;
                objb.Script_AboutCompany = obj.ScriptWriter.Script_AboutCompany;
                objb.Script_BenifitesForCustomer = obj.ScriptWriter.Script_BenifitesForCustomer;
                objb.Script_TargetAudience = obj.ScriptWriter.Script_TargetAudience;
                objb.Script_CompanyName = obj.ScriptWriter.Script_CompanyName;
                objb.Script_CompanyWebsite = obj.ScriptWriter.Script_CompanyWebsite;
                objb.Script_LogoName = obj.ScriptWriter.Script_LogoName;
                objb.Script_ProductServiceImages = obj.ScriptWriter.Script_ProductServiceImages;
                objb.Script_Price = obj.ScriptWriter.Script_Price;
                objb.VO_Required = obj.VO.VO_Required;
                objb.VO_Language = obj.VO.VO_Language;
                objb.VO_Gender = obj.VO.VO_Gender;
                objb.VO_ArtistSamleType = obj.VO.VO_ArtistSamleType;
                objb.VO_SampleId = obj.VO.VO_SampleId;
                objb.VO_SamplePrice = obj.VO.VO_SamplePrice;
                objb.ED_VideoDuration = obj.ExtraDetails.ED_VideoDuration;
                objb.ED_ExtraDetails = obj.ExtraDetails.ED_ExtraDetails;
                objb.ED_FileName = obj.ExtraDetails.ED_FileName;
                objb.OrderTotal = ((objb.SampleVideoPrice == null) ? 0 : objb.SampleVideoPrice) +((objb.Script_Price==null)?0: objb.Script_Price) + ((objb.VO_SamplePrice == null) ? 0 : objb.VO_SamplePrice) ;
                string msg = objb._Insert("procBasket",objb);
                return Json(msg);
        }
        public ActionResult AdminBasketList()
        {
            Basket obj = new Basket();
            return View(obj._Select("procBasket", "SELECT", obj).Tables[0]);
        }
        public ActionResult DeleteBasketItem(Basket obj)
        {
            string customerid = GlobalFunctions.GetCookie("UserId");
            string msg = "";
            if (!string.IsNullOrEmpty(customerid))
            {
                obj.CustomerId = Convert.ToInt32(customerid);
                DataTable dt = obj._Select("procBasket", "DeleteBasketItem", obj).Tables[0];
                if (dt.Rows.Count > 0)
                {
                   msg = Convert.ToString(dt.Rows[0][0]);
                   
                }
                else
                {
                    msg = "Your Cart is Empty";
                }
            }
            else
            {
                msg = "Your Session is expired. Please login and continue";
            }
            return Json(msg);
        }
        public ActionResult PlaceOrder(Basket obj)
        {
            string customerid = GlobalFunctions.GetCookie("UserID");
            string msg = "";
            if (!string.IsNullOrEmpty(customerid))
            {
                obj.CustomerId = Convert.ToInt32(customerid);
                DataTable dt = obj._Select("procBasket", "CheckingBasket", obj).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Orders objorder = new Orders();
                    objorder.OrderTotal = Convert.ToDouble(dt.Rows[0]["OrderTotal"]);
                    objorder.CustomerId = obj.CustomerId;
                    objorder.BasketId = obj.BasketId;
                    /*DataTable dt1= objorder._Select("procOrders", "PlaceOrder",objorder).Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        msg =Convert.ToString( dt1.Rows[0][0]);
                    }
                    else
                    {
                        msg = "Please try again somthing went wrong";
                    }*/
                    msg = objorder._Insert("procOrders",objorder);
                }
                else
                {
                    msg = "Your Cart is Empty";
                }
            }
            else
            {
                msg = "Your Session is expired. Please login and continue";
            }
            return Json(msg);
        }
        public ActionResult UpdateBasket(Basket obj)
        {
            obj.CustomerId = GlobalFunctions.GetUserId();
            string msg = obj._Select("procBasket", "UpdateBasket", obj, true);
            return Json(msg);
        }
        public ActionResult SaveVideoSelected(Basket obj)
        {
            obj.CustomerId = GlobalFunctions.GetUserId();
            return Json(obj._Select("procBasket", "SaveVideoSelected", obj, true));
        }
        public ActionResult SampleVideo_Step1Save(Basket obj)
        {
            obj.CustomerId = GlobalFunctions.GetUserId();
            return Json(obj._Select("procBasket", "SampleVideo_Step1Save", obj, true));
        }
        public ActionResult SampleVideo_Step3Save_1(Basket obj)
        {
            obj.CustomerId = GlobalFunctions.GetUserId();
            return Json(obj._Select("procBasket", "SampleVideo_Step3Save_1", obj, true));
        }
        public ActionResult SampleVideo_Step3Save_2(Basket obj)
        {
            obj.CustomerId = GlobalFunctions.GetUserId();
            return Json(obj._Select("procBasket", "SampleVideo_Step3Save_2", obj, true));
        }
        public ActionResult SampleVideo_Step2_Submit(Basket obj)
        {
            obj.CustomerId = GlobalFunctions.GetUserId();
            return Json(obj._Select("procBasket", "SampleVideo_Step2_Submit", obj, true));
        }
        public ActionResult OrderNow(Basket obj)
        {
            obj.CustomerId = GlobalFunctions.GetUserId();
            string msg=obj._Select("procBasket", "OrderNow", obj, true);
            return Json(msg);
        }
        public ActionResult _VORequirements(Basket obj)
        {
            VoiceArtistSounds objvoice = new VoiceArtistSounds();
            DataTable dt = objvoice._Select("procVoiceArtistSounds", "VoiceArtistForCustomer", objvoice).Tables[0];
            ViewBag.VoiceArtistSounds = GlobalFunctions.ConverDataTableToList<vmVoiceList>(dt).ToList();

            obj.CustomerId = GlobalFunctions.GetUserId();
            obj = GlobalFunctions.ConverDataTableToList<Basket>(obj._Select("procBasket", "SELECT", obj).Tables[0]).FirstOrDefault();
            return View(obj);
        }
        public ActionResult RemoveScript()
        {
            Basket obj = new Basket();
            return Json(obj._Select("procBasket","RemoveScrip",obj,true));
        }
        public ActionResult RemoveVoiceOver()
        {
            Basket obj = new Basket();
            return Json(obj._Select("procBasket", "RemoveVoiceOver", obj, true));
        }

    }
}