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
    public class FileApprovalStatusController : Controller
    {
        // GET: FileApprovalStatus
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveFeedback(FileApprovalStatus obj)
        {
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
            obj.UserId = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));
            string msg = obj._Insert("procFileApprovalStatus",obj);
            return Json(msg);
        }
        public ActionResult GetFeedback(FileApprovalStatus obj)
        {
            obj.UserId = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));
            DataTable dt= obj._Select("procFileApprovalStatus","SELECT", obj).Tables[0];
            List<vmFileApprovalStatus> objList = new List<vmFileApprovalStatus>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    vmFileApprovalStatus objfa = new vmFileApprovalStatus();
                    objfa.OrderId = Convert.ToInt32(dt.Rows[i]["OrderId"]);
                    objfa.UserId = Convert.ToInt32(dt.Rows[i]["UserId"]);
                    objfa.FileName = Convert.ToString(dt.Rows[i]["FileName"]);
                    objfa.FileStatus = Convert.ToString(dt.Rows[i]["FileStatus"]);
                    objfa.ClientFeedback = Convert.ToString(dt.Rows[i]["ClientFeedback"]);
                    objfa.ClientStatus = Convert.ToString(dt.Rows[i]["ClientStatus"]);
                    objfa.CreatedDate = Convert.ToString(dt.Rows[i]["CreatedDate"]);
                    objList.Add(objfa);
                }

            }
            return Json(objList);
        }
    }
}