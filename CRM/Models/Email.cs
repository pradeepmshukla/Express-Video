using CRM.Models.Global;
using CRM.Models.ViewModel;
using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace CRM.Models
{
    public class Email
    {
        public string SendMail(vmEmail emailcontent)
        {

            if (ConfigurationManager.AppSettings["IsEmailEnabled"].ToString() == "true")
            {
                EmailLogs objlogs = new EmailLogs();
                ExceptionLogs objex = new ExceptionLogs();
                objex.ExceptionType = "Mail Sent";
                try
                {
                    EmailManagement obj = new EmailManagement();
                    
                    
                    obj.UniqueKey = emailcontent.EmailTemplate;
                    obj = GlobalFunctions.ConverDataTableToList<EmailManagement>(obj._Select("procEmailManagement", "SelectbyTitle", obj).Tables[0]).FirstOrDefault();
                    if (obj != null && emailcontent != null)
                    {
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(Convert.ToString(obj.EMFrom));
                        mailMessage.To.Add(new MailAddress(Convert.ToString(emailcontent.EmailTo)));
                        mailMessage.Subject = MailContentReplace(obj.EMSubject, emailcontent);
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = MailContentReplace(obj.EMBody, emailcontent);
                        SmtpClient client = new SmtpClient();
                        client.Credentials = new System.Net.NetworkCredential(Convert.ToString(obj.EMFrom), "Expressvideo@0987");
                        client.Host = "relay-hosting.secureserver.net";
                        //client.Host = "smtpout.secureserver.net";
                        client.Port = 25;
                        

                        objlogs.EmailMangementID = obj.EmailMangementID;
                        objlogs.EMFrom = obj.EMFrom;
                        objlogs.EMCC = obj.EMCC;
                        objlogs.EMBCC = obj.EMBCC;
                        objlogs.EMBody = mailMessage.Body;
                        objlogs.EMFrom = obj.EMFrom;
                        objlogs.EMTO = emailcontent.EmailTo;
                        objlogs.EMSubject = mailMessage.Subject;
                        objlogs.IsSent = false;
                        objlogs.Status = "In Process";
                        objlogs.EmailLogsId =Convert.ToInt32(objlogs._Insert("procEmailLogs", objlogs));
                        client.Send(mailMessage);
                        //client.SendAsync(obj.EMFrom,emailcontent.EmailTo,obj.EMSubject,mailMessage.Body,)
                        objlogs.Status = "Mail Sent";
                        string msg= objlogs._Update("procEmailLogs", objlogs);
                        return "Sent from :" + obj.EMFrom + " EmailTo:" + Convert.ToString(emailcontent.EmailTo) + " Subject:" + Convert.ToString(obj.EMSubject)+">"+msg;
                    }
                    else
                    {
                        return "emailcontent is null";
                    }                    
                }
                catch (Exception ex)
                {
                    objlogs.Status = ex.Message;
                    ExceptionLogs objexer = new ExceptionLogs();
                    objexer.Save(ex, "Email>SendEmail");

                    objex.ExceptionMsg = "Step 5- Mail sending fail>" + emailcontent.EmailTo;
                    objex._Insert("procExceptionLogs", objex);

                    string msg = objlogs._Update("procEmailLogs", objlogs);
                    return ex.Message;
                }
            }
            else
            {
                return "setting off";
            }
            
        }
        public static string MailContentReplace(string content, vmEmail obj)
        {
            if (obj != null)
            {
                if (obj.OrderId != null)
                {
                    content = content.Replace("@OrderId", Convert.ToString(obj.OrderId));
                }
                else
                {
                    content = content.Replace("@OrderId", "");
                }
                //-------------------------------------
                if (obj.UserName != null)
                {
                    content = content.Replace("@UserName", Convert.ToString(obj.UserName));
                }
                else
                {
                    content = content.Replace("@UserName", "");
                }
                //-------------------------------------
                if (obj.Name != null)
                {
                    content = content.Replace("@FullName", Convert.ToString(obj.Name));
                }
                else
                {
                    content = content.Replace("@FullName", "");
                }
                //-------------------------------------
                if (obj.Password != null)
                {
                    content = content.Replace("@Password", Convert.ToString(obj.Password));
                }
                else
                {
                    content = content.Replace("@Password", "");
                }
                //-------------
                if (obj.EmailTo != null)
                {
                    content = content.Replace("@EmailId", Convert.ToString(obj.EmailTo));
                }
                else
                {
                    content = content.Replace("@EmailId", "");
                }
                //-------------
                if (obj.MobileNo != null)
                {
                    content = content.Replace("@MobileNo", Convert.ToString(obj.MobileNo));
                }
                else
                {
                    content = content.Replace("@MobileNo", "");
                }
                //-------------
                if (obj.MessageInfo != null)
                {
                    content = content.Replace("@MessageInfo", Convert.ToString(obj.MessageInfo));
                }
                else
                {
                    content = content.Replace("@MessageInfo", "");
                }

            }
            return Convert.ToString(content);
        }
        public string testmail()
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("update@expressvideo.in");
                mailMessage.To.Add(new MailAddress("akhilesh.vis17@gmail.com"));
                mailMessage.Subject = "test Subject";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "test body";
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("update@expressvideo.in", "Expressvideo@0987");
                client.Host = "relay-hosting.secureserver.net";
				client.Port = 25;
                client.Send(mailMessage);
                return "Sent";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
   
    public enum EmailTemplate
    {
        Template1,
        Template2,
        Template3,
        Template4,
        Template5,
        Template6,
        Template7,
        Template8,
        Template9,
        Template10,
        Template11,
        Template12,
        Template13,
        Template14,
        Template15,
        Template16,
        Template17,
        Template18,
        Template19,
        Template20,
        Template21,
        Template22,
        Template23,
        Template25,
        Template26,
        Template27,
        Template28,
        Template29,
        Template30
    }
    public enum MailReplace
    {
        OrderId,
        UserName,
    }

  

}