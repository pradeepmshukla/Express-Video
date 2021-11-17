using CRM.Models;
using CRM.Models.AuthData;
using CRM.Models.Global;
using CRM.Models.ViewModel;
using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class UserDetailController : BaseController
    {

        public ActionResult NewUser(string id)
        {
            UserDetails obj = new UserDetails();
            if (!string.IsNullOrEmpty(id))
            {
                obj.UserID = Convert.ToInt32(id);
                if (obj.UserID > 0)
                {
                    DataTable dt = obj._Select("procUserDetails", "SELECT", obj).Tables[0];
                    obj = GlobalFunctions.ConverDataTableToList<UserDetails>(dt).FirstOrDefault();
                }
            }
            return View(obj);
        }
        public ActionResult SaveData(UserDetails obj)
        {
            string returnSms = "";
            if (obj.UserID > 0)
            {
                returnSms = obj._Update("procUserDetails", obj);
            }
            else
            {
                returnSms = obj._Insert("procUserDetails", obj);
                /*Send Mail*/
                if (obj.RoleId == 4) //customer
                {
                    if (!string.IsNullOrEmpty(obj.EmailID))
                    {
                        vmEmail objmail = new vmEmail();
                        objmail.EmailTo = obj.EmailID;
                        objmail.EmailTemplate = "Template1";
                        objmail.Name = obj.FirstName + " " + obj.LastName;
                        objmail.EmailId = obj.EmailID;
                        objmail.MobileNo = obj.MobileNo;

                        Email email = new Email();
                        email.SendMail(objmail);
                        GlobalFunctions.AddAnnouncement("New User", "New customer registration <br/> Name: " + objmail.Name + "<br/>Email Id: " + objmail.EmailId, 0, 0);// to admin
                        GlobalFunctions.AddAnnouncement("Welcome", "Congratulation on being a part of the Expressvideo team.", 0, GetUserIdByEmailId(obj.EmailID));// to customer

                    }
                }
                if(obj.RoleId==5 || obj.RoleId==6 || obj.RoleId == 7) //artist
                {
                    if (!string.IsNullOrEmpty(obj.EmailID))
                    {
                        vmEmail objmail = new vmEmail();
                        objmail.EmailTo = obj.EmailID;
                        objmail.EmailTemplate = "Template4";
                        objmail.UserName = obj.UserName;
                        objmail.Password = obj.EmailID;
                        
                        Email email = new Email();
                        email.SendMail(objmail);
                        GlobalFunctions.AddAnnouncement("New Artist", "New Artist registration <br/> Name: " + objmail.Name + "<br/>Email Id: " + objmail.EmailId, 0, 0);// to admin
                        GlobalFunctions.AddAnnouncement("Welcome", "Congratulation on being a part of the Expressvideo team.", 0, GetUserIdByEmailId(obj.EmailID));// to customer

                    }
                }
            }

            return Json(returnSms, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CheckLogin(UserDetails obj)
        {

            DataTable dt = obj._Select("procUserDetails", "Login", obj).Tables[0];
            string msg = "";
            if (dt.Rows.Count == 0)
            {
                msg = "User Id or Password is Incorrect! Login Fail";
                Session.Abandon();
            }
            else
            {
                msg = "Success";
                GlobalFunctions.SetUsersCookies(dt);
            }
            return Json(msg);
        }

        public ActionResult Logout()
        {
            GlobalFunctions.ExpireAllCookies();
            return Json("");
        }

        public ActionResult MyProfile()
        {
            UserDetails objuser = new UserDetails();
            objuser.UserID = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));
            DataTable dt = objuser._Select("procUserDetails", "MyProfile", objuser).Tables[0];
            objuser = GlobalFunctions.ConverDataTableToList<UserDetails>(dt).FirstOrDefault();
            return View(objuser);
        }

        public ActionResult StaffList()
        {
            UserDetails obj = new UserDetails();
            //obj.RoleId = 3;//Staff
            return View(obj._Select("procUserDetails", "StaffList", obj).Tables[0]);

        }
        public ActionResult CustomerList()
        {
            UserDetails obj = new UserDetails();
            obj.RoleId = 4;//Customer
            return View(obj._Select("procUserDetails", "SELECT", obj).Tables[0]);

        }
        public ActionResult SuperAdminList()
        {
            UserDetails obj = new UserDetails();
            obj.RoleId = 1;//Super Admin
            return View(obj._Select("procUserDetails", "SELECT", obj).Tables[0]);

        }
        public ActionResult AdminList()
        {
            UserDetails obj = new UserDetails();
            obj.RoleId = 2;//Admin
            return View(obj._Select("procUserDetails", "SELECT", obj).Tables[0]);

        }
        public ActionResult ScriptWriterList()
        {
            UserDetails obj = new UserDetails();
            obj.RoleId = 5;//Script Writer
            return View(obj._Select("procUserDetails", "SELECT", obj).Tables[0]);

        }
        public ActionResult VoiceArtistList()
        {
            UserDetails obj = new UserDetails();
            obj.RoleId = 6;//Voice Artist
            return View(obj._Select("procUserDetails", "SELECT", obj).Tables[0]);

        }
        public ActionResult VideoArtistList()
        {
            UserDetails obj = new UserDetails();
            obj.RoleId = 7;//Video Artist
            return View(obj._Select("procUserDetails", "SELECT", obj).Tables[0]);

        }

        public ActionResult UsersList(string id)
        {
            UserDetails obj = new UserDetails();
            obj.RoleId = Convert.ToInt32(id);
            return View(obj._Select("procUserDetails", "SELECT", obj).Tables[0]);
        }
        public ActionResult UserListForAdminToAssignedJob()
        {
            UserDetails obj = new UserDetails();
            DataTable dt = obj._Select("procUserDetails", "UserListForAdminToAssignedJob", obj).Tables[0];

            List<UserDetails> lstScriptUser = new List<UserDetails>();
            List<UserDetails> lstVoiceUser = new List<UserDetails>();
            List<UserDetails> lstVideoUser = new List<UserDetails>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UserDetails objud = new UserDetails();
                objud.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
                objud.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                objud.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                int roleid = Convert.ToInt32(dt.Rows[i]["RoleId"]);
                if (roleid == 5)
                {
                    lstScriptUser.Add(objud);
                }
                if (roleid == 6)
                {
                    lstVoiceUser.Add(objud);
                }
                if (roleid == 7)
                {
                    lstVideoUser.Add(objud);
                }
            }

            return Json(new { ScriptUser = lstScriptUser, VoiceUser = lstVoiceUser, VideoUser = lstVideoUser });

        }

        public ActionResult ChangePassword(vmPasswordChange obj)
        {
            UserDetails objUser = new UserDetails();
            objUser.UserID = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));
            objUser.Password = obj.CurrentPassword;
            string msg = objUser._Select("procUserDetails", "UpdatePasswordCheck", objUser).Tables[0].Rows[0][0].ToString();
            if (msg == "Password Mached")
            {
                UserDetails objUser1 = new UserDetails();
                objUser1.UserID = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));
                objUser1.Password = obj.NewPassword;
                msg = objUser1._Select("procUserDetails", "UpdatePassword", objUser1).Tables[0].Rows[0][0].ToString();

            }
            return Json(msg);

        }

        public ActionResult UpdateProfile(UserDetails obj)
        {
            obj.UserID = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));
            string msg = obj._Select("procUserDetails", "UpdateProfile", obj).Tables[0].Rows[0][0].ToString();
            GlobalFunctions.SetCookie("ProfilePhoto", obj.ProfilePhoto);
            return Json(msg);
        }
        public ActionResult InActiveUser(UserDetails obj)
        {
            return Json(obj._Select("procUserDetails", "InActiveUser", obj, true));
        }
        public ActionResult _UserPersonalDetails(UserDetails obj)
        {
            DataTable dt = obj._Select("procUserDetails", "UserPersonalDetails", obj).Tables[0];
            obj = GlobalFunctions.ConverDataTableToList<UserDetails>(dt).FirstOrDefault();
            return View(obj);
        }
        public ActionResult _ClientDetails(UserDetails obj)
        {
            ViewBag.ClientDetails = obj._Select("procUserDetails", "_ClientDetails", obj).Tables[0];
            return View();
        }
        public ActionResult _ArtistDetails(UserDetails obj)
        {
            List<UserDetails> lstobj = GlobalFunctions.ConverDataTableToList<UserDetails>(obj._Select("procUserDetails", "SELECT", obj).Tables[0]);
            return View(lstobj);
        }
        public ActionResult _ViewSample(UserDetails obj)
        {
            obj = GlobalFunctions.ConverDataTableToList<UserDetails>(obj._Select("procUserDetails", "SELECT", obj).Tables[0]).FirstOrDefault();
            return View(obj);
        }
        public ActionResult LoginAndRegistrationWithGmail(UserDetails obj)
        {
            DataTable dt = obj._Select("procUserDetails", "INSERT", obj).Tables[0];
            GlobalFunctions.SetUsersCookies(dt);
            return Json("");

        }
        public ActionResult GmailLogoutChecking()
        {
            string val = "";
            if (GlobalFunctions.GetUserId() > 0)
            {
                if (GlobalFunctions.GetLoginWithGmail() == "true")
                {
                    GlobalFunctions.ExpireAllCookies();
                    val = "logout";
                }
            }
            return Json(val);
        }
        public ActionResult validateUser(UserDetails obj)
        {
            string msg = obj._Select("procUserDetails", "validateUser", obj, true);
            return Json(msg);

        }
        public ActionResult CheckPassword(UserDetails obj)
        {
            string msg = obj._Select("procUserDetails", "CheckPassword", obj, true);
            return Json(msg);
        }
        public ActionResult ApproveDocument(UserDetails obj)
        {
            string msg = obj._Select("procUserDetails", "ApproveDocument", obj, true);
            return Json(msg);
        }
        public ActionResult DeclineDocument(UserDetails obj)
        {
            string msg = obj._Select("procUserDetails", "DeclineDocument", obj, true);
            return Json(msg);
        }

        public ActionResult IsUserExists(UserDetails obj)
        {
            string msg = obj._Select("procUserDetails", "IsUserExists", obj, true);
            return Json(msg);

        }
        public ActionResult ResetPassword(vmResetPassword obj)
        {
            OtpManagement objotp = new OtpManagement();
            objotp.MobileNo = obj.MobileNo;
            objotp.OTP = obj.OTP;
            string msg = objotp._Select("procOtpManagement", "VarifyOTP", objotp, true);
            if (msg == "Success")
            {
                UserDetails objUser = new UserDetails();
                objUser.MobileNo = obj.MobileNo;
                objUser.Password = obj.Password;
                msg = objUser._Select("procUserDetails", "ResetPassword", objUser, true);
                if (msg.Contains("^"))
                {
                    msg = msg.Split('^')[0];
                    int UserId = Convert.ToInt32(msg.Split('^')[1]);
                    string EmailId = Convert.ToString(msg.Split('^')[2]);
                    GlobalFunctions.AddAnnouncement("Password Changed", "Your password changed", 0, UserId);
                    if (!string.IsNullOrEmpty(EmailId))
                    {
                        vmEmail objmail = new vmEmail();
                        objmail.EmailTo = EmailId;
                        objmail.EmailTemplate = "Template6";
                        objmail.Password = obj.Password;
                        Email email = new Email();
                        email.SendMail(objmail);
                    }
                }
                return Json(msg);
            }
            else
            {
                return Json(msg);
            }



        }

        public int GetUserIdByEmailId(string emailId)
        {
            UserDetails obj = new UserDetails();
            obj.EmailID = emailId;
            obj.UserID = Convert.ToInt32(obj._Select("procUserDetails", "GetUserIdbyEmaildId", obj, true));
            return obj.UserID;
        }
    }
}
