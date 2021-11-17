using CRM.Models;
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
    public class FeedbackDetailsController : Controller
    {
        // GET: FeedbackDetails
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveFeedback(FeedbackDetails obj)
        {
            obj.SendFrom = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));
            string msg = obj._Insert("procFeedbackDetails", obj);
            string EmailId = msg.Split('^')[1];
            string roleid = EmailId.Split('~')[0];
            EmailId = EmailId.Split('~')[1];
            string version = "1";
            if(obj.FeedbackType== "ScriptFileName_1" || obj.FeedbackType== "VOFileName_1" || obj.FeedbackType== "VideoFileName_1")
            {
                version = "1";
            }
            if (obj.FeedbackType == "ScriptFileName_2" || obj.FeedbackType == "VOFileName_2" || obj.FeedbackType == "VideoFileName_2")
            {
                version = "2";
            }
            if (obj.FeedbackType == "ScriptFileName_3" || obj.FeedbackType == "VOFileName_3" || obj.FeedbackType == "VideoFileName_3")
            {
                version = "3";
            }
            vmEmail objvm = new vmEmail();

            objvm.EmailTo = EmailId;

            if (roleid == "5")
            {
                objvm.EmailTemplate = "Template11";  /*v1-11, v2-13*/
            }
            if (roleid == "6")
            {
                objvm.EmailTemplate = "Template17"; /*v1-17, v2-19*/
            }
            if (roleid == "7")
            {
                objvm.EmailTemplate = "Template23"; /*v1-23, v2-25 */
            }
            Email mail = new Email();
            mail.SendMail(objvm);
            GlobalFunctions.AddAnnouncement("Order No. "+ obj.OrderId + " Customer Feedback Version 0"+ version, "Feedback updated of order no "+obj.OrderId, obj.SendFrom, 0);
            GlobalFunctions.AddAnnouncement("Order No. "+ obj.OrderId + " Customer Feedback Version 0"+version, "Feedback updated of order no " + obj.OrderId, obj.SendFrom, obj.SentTo);

            msg = msg.Split('^')[0];
            return Json(msg);

        }
        public ActionResult _ViewFeedback(FeedbackDetails obj)
        {
            ViewBag.OrderId = obj.OrderId;
            ViewBag.SentTo = obj.SentTo;
            ViewBag.FeedbackType = obj.FeedbackType;
            obj.SendFrom = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));
            DataTable dt= obj._Select("procFeedbackDetails", "ViewFeedback", obj).Tables[0];
            //List<vmFeedbackDetails> lst = GlobalFunctions.ConverDataTableToList<vmFeedbackDetails>(dt);
            return View(dt);

        }
        public ActionResult ViewFeedbackArtist(FeedbackDetails obj)
        {
            obj.SentTo = GlobalFunctions.GetUserId();
            if (GlobalFunctions.GetRoleId() == 5)
            {
                obj.FeedbackType = "Script Writer";
            }
            else if (GlobalFunctions.GetRoleId() == 6)
            {
                obj.FeedbackType = "VO Artist";
            }
            else if (GlobalFunctions.GetRoleId() == 7)
            {
                obj.FeedbackType = "Video Artist";
            }
            DataTable dt = obj._Select("procFeedbackDetails", "ViewFeedbackArtist", obj).Tables[0];
            List<vmFeedbackDetails> lst = new List<vmFeedbackDetails>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    vmFeedbackDetails objvm = new vmFeedbackDetails();
                    objvm.FeedbackComment = Convert.ToString(dt.Rows[i]["FeedbackComments"]);
                    objvm.DateTime = Convert.ToString(dt.Rows[i]["CreatedDate"]);
                    objvm.FilesUploaded = Convert.ToString(dt.Rows[i]["FilesUploaded"]);
                    objvm.ClientStatus = Convert.ToString(dt.Rows[i]["ClientStatus"]);
                    lst.Add(objvm);
                }
            }
            return Json(lst);

        }
    }
}