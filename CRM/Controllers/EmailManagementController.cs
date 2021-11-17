using CRM.Models;
using CRM.Models.Global;
using CRM.Models.ViewModel;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class EmailManagementController : Controller
    {
        // GET: EmailManagement
        public ActionResult Index()
        {
            EmailManagement obj = new EmailManagement();
            return View(obj._Select("procEmailManagement").Tables[0]);
        }
        public ActionResult SendMail_Test()
        {
            string message="";
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("support@expressvideo.in");
                mailMessage.To.Add(new MailAddress("akhilesh.vis17@gmail.com"));

                mailMessage.Subject = "test";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "hello how are you";

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("support@expressvideo.in", "Expressvideo@0987");
                client.Host = "relay-hosting.secureserver.net";
                client.Send(mailMessage);
                message = "Success";
            }
            catch (Exception ex) {
                message = ex.Message;
            }
            return Json(message);
        }
        public ActionResult Create(string id)
        {
            EmailManagement obj = new EmailManagement();
            string ID = Convert.ToString(id);
            if (!string.IsNullOrEmpty(ID))
            {
                obj.EmailMangementID = Convert.ToInt32(id);
                DataTable dt = obj._Select("procEmailManagement", "SELECT", obj).Tables[0];
                obj = GlobalFunctions.ConverDataTableToList<EmailManagement>(dt).FirstOrDefault(); 
            }

            return View(obj);
        }
        public ActionResult SaveData(EmailManagement obj)
        {
            if (obj.EmailMangementID > 0)
            {
                return Json(obj._Update("procEmailManagement", obj));
            }
            else
            {
                return Json(obj._Insert("procEmailManagement", obj));
            }
        }
        public ActionResult SendMail(vmEmail data)
        {
            Email objEmail = new Email();
            return Json(objEmail.SendMail(data));
        }
        public ActionResult TestMail()
        {
            Email objEmail = new Email();
            return Json(objEmail.testmail());
        }

    }
}