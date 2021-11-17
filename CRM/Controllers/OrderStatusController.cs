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
    public class OrderStatusController : Controller
    {
        // GET: OrderStatus
        public ActionResult ScriptAssigned(OrderStatus obj)
        {
            obj.ScriptAssignedUserId = GlobalFunctions.GetUserId();
            string msg = obj._Select("procOrderStatus", "ScriptAssigned", obj, true);
            return Json(msg);
        }
        public ActionResult MyJobs()
        {
            OrderStatus obj = new OrderStatus();
            return View(obj._Select("procOrderStatus", "MyJobs", obj).Tables[0]);

        }
        public ActionResult VOArtist()
        {
            OrderStatus obj = new OrderStatus();
            return View(obj._Select("procOrderStatus", "VOArtist", obj).Tables[0]);

        }
        public ActionResult VideoArtist()
        {
            OrderStatus obj = new OrderStatus();
            return View(obj._Select("procOrderStatus", "VideoArtist", obj).Tables[0]);

        }
        public ActionResult MyProject()
        {
            OrderStatus obj = new OrderStatus();
            return View(obj._Select("procOrderStatus", "MyProject", obj).Tables[0]);

        }
        public ActionResult UpdateScriptStatus(OrderStatus obj)
        {
            string msg =Convert.ToString( obj._Select("procOrderStatus", "UpdateScriptStatus", obj).Tables[0].Rows[0][0]);
            return Json(msg);
        }
        public ActionResult UpdateVOStatus(OrderStatus obj)
        {
            string msg = Convert.ToString(obj._Select("procOrderStatus", "UpdateVOStatus", obj).Tables[0].Rows[0][0]);
            return Json(msg);
        }
        public ActionResult UpdateVideoStatus(OrderStatus obj)
        {
            string msg = Convert.ToString(obj._Select("procOrderStatus", "UpdateVideoStatus", obj).Tables[0].Rows[0][0]);
            return Json(msg);
        }
        public ActionResult AssignOrderToUser(OrderStatus obj)
        {
            string msg = Convert.ToString(obj._Select("procOrderStatus", "AssignOrderToUser", obj).Tables[0].Rows[0][0]);
            return Json(msg);
        }
        public ActionResult CancelOrder(OrderStatus obj)
        {
            return Json(obj._Select("procOrderStatus", "CancelOrder", obj,true));
        }
        public ActionResult GenerateInvoice(OrderStatus obj)
        {
            if (GlobalFunctions.GetRoleId() == 5)
            {
                obj.ScriptInvoiceNo = String.Format("EV{0:000000}", GlobalFunctions.GetRandomNo());
            }
            if (GlobalFunctions.GetRoleId() == 6)
            {
                obj.VoiceInvoiceNo = String.Format("EV{0:000000}", GlobalFunctions.GetRandomNo());
            }
            if (GlobalFunctions.GetRoleId() == 7)
            {
                obj.VideoInvoiceNo = String.Format("EV{0:000000}", GlobalFunctions.GetRandomNo());
            }

            string msg = obj._Select("procOrderStatus", "GenerateInvoice", obj, true);
            return Json(msg);
        }
        public ActionResult _FreelancerInvoice(OrderStatus obj)
        {
            DataTable dt=obj._Select("procOrderStatus", "FreelancerInvoice", obj).Tables[0];
            vmFreelancerInvoice objList = GlobalFunctions.ConverDataTableToList<vmFreelancerInvoice>(dt).FirstOrDefault();
            return View(objList);
            
        }
        public ActionResult _UpdateProjectFileLink(OrderStatus obj)
        {
            return View(GlobalFunctions.ConverDataTableToList<OrderStatus>(obj._Select("procOrderStatus", "SELECT", obj).Tables[0]).FirstOrDefault());
        }
        public ActionResult UpdateProjectFileLink(OrderStatus obj)
        {
            return Json(obj._Select("procOrderStatus", "UpdateProjectFileLink",obj,true));
        }
        public ActionResult _OrderDetails(vmOrderStatus vmobj)
        {
            OrderStatus obj = new OrderStatus();
            obj.OrderId = vmobj.OrderId;
            Orders o = new Orders();
            o.OrderId = vmobj.OrderId;
            o = GlobalFunctions.ConverDataTableToList<Orders>(o._Select("procOrders", "SELECT", o).Tables[0]).FirstOrDefault();
            //DataTable dt = o._Select("procOrders", "SELECT", obj).Tables[0];
            ViewBag.Script_Required = o.Script_Required;
            ViewBag.VO_Required = o.VO_Required;
            UserDetails objuser = new UserDetails();
            List<UserDetails> lstUser = null;
            if (Session["lstUser"] == null)
            {
                lstUser = GlobalFunctions.ConverDataTableToList<UserDetails>(objuser._Select("procUserDetails", "UserListForAdminToAssignedJob", objuser).Tables[0]);
            }
            else
            {
                lstUser = (List<UserDetails>)Session["lstUser"];
            }
            ViewBag.lstUser = lstUser;
            Session["lstUser"] = lstUser;
            DataTable dt = obj._Select("procOrderStatus", "_OrderDetails", obj).Tables[0];
            ViewBag.OrderDetails = dt;
            OrderStatus objvm =GlobalFunctions.ConverDataTableToList<OrderStatus>(dt).FirstOrDefault();
            return View(objvm);
        }
        public ActionResult _UpdateProjectStatusByArtist(OrderStatus obj)
        {
            return View(GlobalFunctions.ConverDataTableToList<OrderStatus>(obj._Select("procOrderStatus", "SELECT", obj).Tables[0]).FirstOrDefault());
        }
        public ActionResult UpdateProjectStatusByArtist1(OrderStatus obj)
        {
            string CustomerUserId = obj._Select("procOrderStatus", "CustomerUserId", obj, true);

            if (GlobalFunctions.GetRoleId() == 5)
            {
                string CustomerEmailId = obj._Select("procOrderStatus", "CustomerEmailId", obj, true);
                vmEmail objvm = new vmEmail();
                objvm.EmailTemplate = "Template10";
                objvm.EmailTo = CustomerEmailId;
                Email mail = new Email();
                mail.SendMail(objvm);
                
                GlobalFunctions.AddAnnouncement("File uploaded by Script", "File uploaded by Script of Order No. " + obj.OrderId, 0, 0);//admin
                GlobalFunctions.AddAnnouncement("Your script is uploaded", " Your script is uploaded of Order No. " + obj.OrderId, GlobalFunctions.GetUserId(), Convert.ToInt32(CustomerUserId)); //to customer
                

            }
            if (GlobalFunctions.GetRoleId() == 6)
            {
                string CustomerEmailId = obj._Select("procOrderStatus", "CustomerEmailId", obj, true);
                vmEmail objvm = new vmEmail();
                objvm.EmailTemplate = "Template16";
                objvm.EmailTo = CustomerEmailId;
                Email mail = new Email();
                mail.SendMail(objvm);


                GlobalFunctions.AddAnnouncement("File uploaded by voice over", "File uploaded by voice over of Order No. " + obj.OrderId, 0, 0);//admin
                GlobalFunctions.AddAnnouncement("Your voice over is uploaded", "Your voice over is uploaded of Order No. " + obj.OrderId, GlobalFunctions.GetUserId(), Convert.ToInt32(CustomerUserId)); //to customer
            }
            if (GlobalFunctions.GetRoleId() == 7)
            {
                string CustomerEmailId = obj._Select("procOrderStatus", "CustomerEmailId", obj, true);
                vmEmail objvm = new vmEmail();
                objvm.EmailTemplate = "Template22";
                objvm.EmailTo = CustomerEmailId;
                Email mail = new Email();
                mail.SendMail(objvm);

                GlobalFunctions.AddAnnouncement("File uploaded by video", "File uploaded by video of Order No. " + obj.OrderId, 0, 0);//admin
                GlobalFunctions.AddAnnouncement("Your video is uploaded", "Your video is uploaded of Order No. " + obj.OrderId, GlobalFunctions.GetUserId(), Convert.ToInt32(CustomerUserId)); //to customer

            }
            string msg = obj._Select("procOrderStatus", "UpdateProjectStatusByArtist", obj, true);
            return Json("File sent to customer for checking");
        }
        public ActionResult UpdateProjectStatusByArtist2(OrderStatus obj)
        {
            string CustomerUserId = obj._Select("procOrderStatus", "CustomerUserId", obj, true);
            if (GlobalFunctions.GetRoleId() == 5)
            {
                string CustomerEmailId = obj._Select("procOrderStatus", "CustomerEmailId", obj, true);
                vmEmail objvm = new vmEmail();
                objvm.EmailTemplate = "Template12";
                objvm.EmailTo = CustomerEmailId;
                Email mail = new Email();
                mail.SendMail(objvm);

                GlobalFunctions.AddAnnouncement("File uploaded by Script Revesion 1", "File uploaded by Script of Order No. " + obj.OrderId, 0, 0);//admin
                GlobalFunctions.AddAnnouncement("Your script is uploaded Revesion 1", " Your script is uploaded of Order No. " + obj.OrderId, GlobalFunctions.GetUserId(), Convert.ToInt32(CustomerUserId)); //to customer

            }
            if (GlobalFunctions.GetRoleId() == 6)
            {
                string CustomerEmailId = obj._Select("procOrderStatus", "CustomerEmailId", obj, true);
                vmEmail objvm = new vmEmail();
                objvm.EmailTemplate = "Template18";
                objvm.EmailTo = CustomerEmailId;
                Email mail = new Email();
                mail.SendMail(objvm);

                GlobalFunctions.AddAnnouncement("File uploaded by voice over Revesion 1", "File uploaded by voice over of Order No. " + obj.OrderId, 0, 0);//admin
                GlobalFunctions.AddAnnouncement("Your voice over is uploaded Revesion 1", "Your voice over is uploaded of Order No. " + obj.OrderId, GlobalFunctions.GetUserId(), Convert.ToInt32(CustomerUserId)); //to customer
            }
            if (GlobalFunctions.GetRoleId() == 7)
            {
                string CustomerEmailId = obj._Select("procOrderStatus", "CustomerEmailId", obj, true);
                vmEmail objvm = new vmEmail();
                objvm.EmailTemplate = "Template24";
                objvm.EmailTo = CustomerEmailId;
                Email mail = new Email();
                mail.SendMail(objvm);

                GlobalFunctions.AddAnnouncement("File uploaded by video Revesion 1", "File uploaded by video of Order No. " + obj.OrderId, 0, 0);//admin
                GlobalFunctions.AddAnnouncement("Your video is uploaded Revesion 1", "Your video is uploaded of Order No. " + obj.OrderId, GlobalFunctions.GetUserId(), Convert.ToInt32(CustomerUserId)); //to customer
            }
            return Json(obj._Select("procOrderStatus", "UpdateProjectStatusByArtist", obj, true));
        }
        public ActionResult UpdateProjectStatusByArtist3(OrderStatus obj)
        {
            string CustomerUserId = obj._Select("procOrderStatus", "CustomerUserId", obj, true);
            if (GlobalFunctions.GetRoleId() == 5)
            {
                string CustomerEmailId = obj._Select("procOrderStatus", "CustomerEmailId", obj, true);
                vmEmail objvm = new vmEmail();
                objvm.EmailTemplate = "Template10";
                objvm.EmailTo = CustomerEmailId;
                Email mail = new Email();
                mail.SendMail(objvm);

                GlobalFunctions.AddAnnouncement("File uploaded by Script Revesion 2", "File uploaded by Script of Order No. " + obj.OrderId, 0, 0);//admin
                GlobalFunctions.AddAnnouncement("Your script is uploaded Revesion 2", " Your script is uploaded of Order No. " + obj.OrderId, GlobalFunctions.GetUserId(), Convert.ToInt32(CustomerUserId)); //to customer


            }
            if (GlobalFunctions.GetRoleId() == 6)
            {
                string CustomerEmailId = obj._Select("procOrderStatus", "CustomerEmailId", obj, true);
                vmEmail objvm = new vmEmail();
                objvm.EmailTemplate = "Template10";
                objvm.EmailTo = CustomerEmailId;
                Email mail = new Email();
                mail.SendMail(objvm);
                GlobalFunctions.AddAnnouncement("File uploaded by voice over Revesion 2", "File uploaded by voice over of Order No. " + obj.OrderId, 0, 0);//admin
                GlobalFunctions.AddAnnouncement("Your voice over is uploaded Revesion 2", "Your voice over is uploaded of Order No. " + obj.OrderId, GlobalFunctions.GetUserId(), Convert.ToInt32(CustomerUserId)); //to customer
            }
            if (GlobalFunctions.GetRoleId() == 7)
            {
                string CustomerEmailId = obj._Select("procOrderStatus", "CustomerEmailId", obj, true);
                vmEmail objvm = new vmEmail();
                objvm.EmailTemplate = "Template24";
                objvm.EmailTo = CustomerEmailId;
                Email mail = new Email();
                mail.SendMail(objvm);
                GlobalFunctions.AddAnnouncement("File uploaded by video Revesion 2", "File uploaded by video of Order No. " + obj.OrderId, 0, 0);//admin
                GlobalFunctions.AddAnnouncement("Your video is uploaded Revesion 2", "Your video is uploaded of Order No. " + obj.OrderId, GlobalFunctions.GetUserId(), Convert.ToInt32(CustomerUserId)); //to customer
            }
            return Json(obj._Select("procOrderStatus", "UpdateProjectStatusByArtist", obj, true));
        }
        public ActionResult _PaymentDetails(vmPaymentDetails obj)
        {
            return View(obj);

        }
        public ActionResult AcceptWorkAgreements(OrderStatus obj)
        {
            int CustomerUserId =Convert.ToInt32( obj._Select("procOrderStatus", "CustomerUserId", obj, true));
            if (GlobalFunctions.GetRoleId() == 5)
            {
                obj.ScriptAssignedUserId = GlobalFunctions.GetUserId();

                vmEmail objvm = new vmEmail();
                objvm.EmailTemplate = "Template6";
                objvm.EmailTo = GlobalFunctions.GetEmaildId();
                Email mail = new Email();
                mail.SendMail(objvm);
                GlobalFunctions.AddAnnouncement("Project Status", "Project Accept By Script Writer of Order No. " + obj.OrderId, 0, 0);//admin
                GlobalFunctions.AddAnnouncement("Congratulation! You got the project.", "Congratulation! You got the project with Order No. " + obj.OrderId, 0, GlobalFunctions.GetUserId());//Artist
                GlobalFunctions.AddAnnouncement("Project Status", "Project Accept By Script Writer of Order No. " + obj.OrderId, 0, CustomerUserId);//Customer

            }
            if (GlobalFunctions.GetRoleId() == 6)
            {
                
                obj.VOAssignedUserId = GlobalFunctions.GetUserId();

                vmEmail objvm = new vmEmail();
                objvm.EmailTemplate = "Template6";
                objvm.EmailTo = GlobalFunctions.GetEmaildId();
                Email mail = new Email();
                mail.SendMail(objvm);
                GlobalFunctions.AddAnnouncement("Project Status", "Project Accept By Voice Over of Order No. " + obj.OrderId, 0, 0);//admin
                GlobalFunctions.AddAnnouncement("Congratulation! You got the project.", "Congratulation! You got the project with Order No. " + obj.OrderId, 0, GlobalFunctions.GetUserId());//Artist
                GlobalFunctions.AddAnnouncement("Project Status", "Project Accept By voice Over of Order No. " + obj.OrderId, 0, CustomerUserId);//Customer
            }
            if (GlobalFunctions.GetRoleId() == 7)
            {
                
                obj.VideoAssignedUserId = GlobalFunctions.GetUserId();

                vmEmail objvm = new vmEmail();
                objvm.EmailTemplate = "Template6";
                objvm.EmailTo = GlobalFunctions.GetEmaildId();
                Email mail = new Email();
                mail.SendMail(objvm);
                GlobalFunctions.AddAnnouncement("Project Status", "Project Accept By Animation of Order No. " + obj.OrderId, 0, 0);//admin
                GlobalFunctions.AddAnnouncement("Congratulation! You got the project.", "Congratulation! You got the project with Order No. " + obj.OrderId, 0, GlobalFunctions.GetUserId());//Artist
                GlobalFunctions.AddAnnouncement("Project Status", "Project Accept By Animation of Order No. " + obj.OrderId, 0, CustomerUserId);//Customer
            }
            return Json(obj._Select("procOrderStatus", "AcceptWorkAgreements",obj,true));
        }
        public ActionResult SendInvoiceToAdmin(OrderStatus obj)
        {
            GlobalFunctions.AddAnnouncement("Invoice request sent for order id: "+obj.OrderId, "Invoice request sent for order id: " + obj.OrderId, 0, 0);//admin
            return Json("");
        }
    }
}
