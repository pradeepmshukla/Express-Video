using CRM.Models;
using CRM.Models.Global;
using CRM.Models.ViewModel;
using DAL;
using PgSim.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class pgController : Controller
    {
        // GET: pg
        public ActionResult Index()
        {
            ViewData["Title"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult HandleRequest(formtorequest model)
        {
            CookiesDataHandle objcookie= new CookiesDataHandle();
            string customerid = GlobalFunctions.GetCookie("UserID");
            objcookie.UserId = Convert.ToInt32(customerid);

            string msg = "";
            if (!string.IsNullOrEmpty(customerid))
            {
                Basket obj = new Basket();
                obj.CustomerId = Convert.ToInt32(customerid);
                DataTable dt = obj._Select("procBasket", "CheckingBasket", obj).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (ConfigurationManager.AppSettings["returnUrl"].ToString() == "Actual")
                    {
                        model.orderAmount = Convert.ToString(dt.Rows[0]["FinalTotal"]);
                    }
                    else
                    {
                        model.orderAmount = "1";
                    }
                    objcookie.BasketId = Convert.ToInt32(dt.Rows[0]["BasketId"]);
                }

            }

            model.appId= ConfigurationManager.AppSettings["appid"].ToString();
            model.orderId = "EVNO"+Guid.NewGuid().ToString().ToUpper();
            model.orderCurrency = "INR";
            model.orderNote = "Express video order";
            model.customerName = GlobalFunctions.GetCookie("FirstName")+" "+ GlobalFunctions.GetCookie("LastName");
            model.customerEmail = GlobalFunctions.GetCookie("EmailID"); 
            model.customerPhone = GlobalFunctions.GetCookie("MobileNo");

            model.returnUrl = ConfigurationManager.AppSettings["returnUrl"].ToString();
            model.notifyUrl = ConfigurationManager.AppSettings["notifyUrl"].ToString();
            string secretKey = ConfigurationManager.AppSettings["secretkey"].ToString();
            string mode = ConfigurationManager.AppSettings["mode"].ToString();  //change mode to PROD for production
            string signatureData = "";
            PropertyInfo[] keys = model.GetType().GetProperties();
            keys = keys.OrderBy(key => key.Name).ToArray();

            foreach (PropertyInfo key in keys)
            {
                signatureData += key.Name + key.GetValue(model);
            }
            var hmacsha256 = new HMACSHA256(StringEncode(secretKey));
            byte[] gensignature = hmacsha256.ComputeHash(StringEncode(signatureData));
            string signature = Convert.ToBase64String(gensignature);
            ViewData["signature"] = signature;
            if (mode == "PROD")
            {
                ViewData["url"] = "https://www.cashfree.com/checkout/post/submit";
            }
            else
            {
                ViewData["url"] = "https://test.cashfree.com/billpay/checkout/post/submit";
            }
            objcookie.GeneratedOrderId = model.orderId;
            string oc = objcookie._Insert("procCookiesDataHandle", objcookie);
            return View(model);
        }

        [HttpPost]
        public ActionResult HandleResponse(FormCollection form)
        {
            string secretKey = ConfigurationManager.AppSettings["secretkey"].ToString();
            string orderId = Request.Form["orderId"];
            string orderAmount = Request.Form["orderAmount"];
            string referenceId = Request.Form["referenceId"];
            string txStatus = Request.Form["txStatus"];
            string paymentMode = Request.Form["paymentMode"];
            string txMsg = Request.Form["txMsg"];
            string txTime = Request.Form["txTime"];
            string signature = Request.Form["signature"];

            CookiesDataHandle objcookie = new CookiesDataHandle();
            objcookie.GeneratedOrderId = orderId;
            DataTable dtCDH = objcookie._Select("procCookiesDataHandle", "SELECT", objcookie).Tables[0];
            if (dtCDH.Rows.Count > 0)
            {
                UserDetails objuserdetails = new UserDetails();
                objuserdetails.UserID = Convert.ToInt32(dtCDH.Rows[0]["UserId"]);
                DataTable dtUser = objuserdetails._Select("procUserDetails", "CookieBackLogin", objuserdetails).Tables[0];
                GlobalFunctions.SetUsersCookies(dtUser);
            }


            PaymentTransaction objpt = new PaymentTransaction();
            objpt.OrderId = orderId;
            objpt.OrderAmount = orderAmount;
            objpt.ReferenceId= referenceId;
            objpt.TxStatus= txStatus;
            objpt.PaymentMode = paymentMode;
            objpt.TxMsg= txMsg;
            objpt.TxTime = txTime;
            objpt.Signature= signature;
            objpt.UserID = GlobalFunctions.GetCookie("UserID");
            string ptmsg=objpt._Insert("procPaymentTransaction", objpt);

            string signatureData = orderId + orderAmount + referenceId + txStatus + paymentMode + txMsg + txTime;

            var hmacsha256 = new HMACSHA256(StringEncode(secretKey));
            byte[] gensignature = hmacsha256.ComputeHash(StringEncode(signatureData));
            string computedsignature = Convert.ToBase64String(gensignature);
            if (signature == computedsignature)
            {
                ViewData["panel"] = "panel panel-success";
                ViewData["heading"] = "Signature Verification Successful";

            }
            else
            {
                ViewData["panel"] = "panel panel-danger";
                ViewData["heading"] = "Signature Verification Failed";

            }
            ViewData["orderId"] = orderId;
            ViewData["orderAmount"] = orderAmount;
            ViewData["referenceId"] = referenceId;
            ViewData["txStatus"] = txStatus;
            ViewData["txMsg"] = txMsg;
            ViewData["txTime"] = txTime;
            ViewData["paymentMode"] = paymentMode;
            if (txStatus == "SUCCESS")
            {
                string customerid = GlobalFunctions.GetCookie("UserID");
                if (!string.IsNullOrEmpty(customerid))
                {
                    Basket obj = new Basket();
                    obj.CustomerId = Convert.ToInt32(customerid);
                    DataTable dt = obj._Select("procBasket", "CheckingBasket", obj).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        Orders objorder = new Orders();
                        objorder.OrderTotal = Convert.ToDouble(dt.Rows[0]["OrderTotal"]);
                        objorder.CustomerId = obj.CustomerId;
                        objorder.BasketId = Convert.ToInt32(dt.Rows[0]["BasketId"]);
                        objorder.PaymentStatus = signatureData;
                        DataTable dtOrder = objorder._Select("procOrders", "OrderPlaced", objorder).Tables[0];

                        //Basket objBasket = GlobalFunctions.ConverDataTableToList<Basket>(dt).FirstOrDefault();
                        Orders objorderPlaced=GlobalFunctions.ConverDataTableToList<Orders>(dtOrder).FirstOrDefault();
                        int TotalDays = 1;
                        if (objorderPlaced.VideoDays == null)
                        {
                            objorderPlaced.VideoDays = 0;
                        }
                        if (objorderPlaced.ScriptDays == null)
                        {
                            objorderPlaced.ScriptDays = 0;
                        }
                        if (objorderPlaced.VODays == null)
                        {
                            objorderPlaced.VODays = 0;
                        }
                        TotalDays +=(int)objorderPlaced.VideoDays;
                        TotalDays += (int)objorderPlaced.ScriptDays;
                        TotalDays += (int)objorderPlaced.VODays;

                        /*Send Mail*/
                        vmEmail objmail = new vmEmail();
                        objmail.EmailTo = GlobalFunctions.GetEmaildId();
                        objmail.EmailTemplate = "Template3";
                        objmail.TotalDays = TotalDays.ToString();
                        objmail.OrderId = objorderPlaced.OrderId.ToString();
                        Email email = new Email();
                        email.SendMail(objmail);
                        GlobalFunctions.AddAnnouncement("New Order Placed", "Order Placed with Order No ("+ objmail.OrderId + ") and the order will be delivered in ("+ objmail.TotalDays + ") working days.", 0, 0);//admin
                        GlobalFunctions.AddAnnouncement("Your Order is Confirmed", "Your Order is confirmed with Order No ("+ objmail.OrderId + ") and the order will be delivered in ("+ objmail.TotalDays + ") working days.", 0, objorderPlaced.CustomerId);//customer
                    }                    
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult HandleRequestTopUp(formtorequest model)
        {
            CookiesDataHandle objcookie = new CookiesDataHandle();
            string customerid = GlobalFunctions.GetCookie("UserID");
            objcookie.UserId = Convert.ToInt32(customerid);

            string msg = "";
            if (!string.IsNullOrEmpty(customerid))
            {
                TopUpPayment obj = new TopUpPayment();
                obj.OrderId = Convert.ToInt32(model.orderId);
                
                if (ConfigurationManager.AppSettings["returnUrl"].ToString() == "Actual")
                {
                    string TotalAmount = obj._Select("procTopUpPayment", "TotalTopUpAmount", obj, true);
                    model.orderAmount = TotalAmount;
                }
                else
                {
                    model.orderAmount = "1"; // for temp
                }
            }

            model.appId = ConfigurationManager.AppSettings["appid"].ToString();
            model.orderId = "EVTU-" + Guid.NewGuid().ToString().ToUpper();
            model.orderCurrency = "INR";
            model.orderNote = "Express video order";
            model.customerName = GlobalFunctions.GetCookie("FirstName") + " " + GlobalFunctions.GetCookie("LastName");
            model.customerEmail = GlobalFunctions.GetCookie("EmailID");
            model.customerPhone = GlobalFunctions.GetCookie("MobileNo");

            model.returnUrl = ConfigurationManager.AppSettings["returnUrlTopup"].ToString();
            model.notifyUrl = ConfigurationManager.AppSettings["notifyUrlTopup"].ToString();
            string secretKey = ConfigurationManager.AppSettings["secretkey"].ToString();
            string mode = ConfigurationManager.AppSettings["mode"].ToString();  //change mode to PROD for production
            string signatureData = "";
            PropertyInfo[] keys = model.GetType().GetProperties();
            keys = keys.OrderBy(key => key.Name).ToArray();

            foreach (PropertyInfo key in keys)
            {
                signatureData += key.Name + key.GetValue(model);
            }
            var hmacsha256 = new HMACSHA256(StringEncode(secretKey));
            byte[] gensignature = hmacsha256.ComputeHash(StringEncode(signatureData));
            string signature = Convert.ToBase64String(gensignature);
            ViewData["signature"] = signature;
            if (mode == "PROD")
            {
                ViewData["url"] = "https://www.cashfree.com/checkout/post/submit";
            }
            else
            {
                ViewData["url"] = "https://test.cashfree.com/billpay/checkout/post/submit";
            }
            objcookie.GeneratedOrderId = model.orderId;
            objcookie.TopUpPaymentOrderId = Convert.ToInt32(GlobalFunctions.GetCookie("TopUpPaymentOrderId"));
            string oc = objcookie._Insert("procCookiesDataHandle", objcookie);
            return View(model);
        }

        [HttpPost]
        public ActionResult HandleResponseTopUp(FormCollection form)
        {
            string secretKey = ConfigurationManager.AppSettings["secretkey"].ToString();
            string orderId = Request.Form["orderId"];
            string orderAmount = Request.Form["orderAmount"];
            string referenceId = Request.Form["referenceId"];
            string txStatus = Request.Form["txStatus"];
            string paymentMode = Request.Form["paymentMode"];
            string txMsg = Request.Form["txMsg"];
            string txTime = Request.Form["txTime"];
            string signature = Request.Form["signature"];

            CookiesDataHandle objcookie = new CookiesDataHandle();
            objcookie.GeneratedOrderId = orderId;
            DataTable dtCDH = objcookie._Select("procCookiesDataHandle", "SELECT", objcookie).Tables[0];
            int TopUpPaymentOrderId = 0;
            if (dtCDH.Rows.Count > 0)
            {
                UserDetails objuserdetails = new UserDetails();
                objuserdetails.UserID = Convert.ToInt32(dtCDH.Rows[0]["UserId"]);
                DataTable dtUser = objuserdetails._Select("procUserDetails", "CookieBackLogin", objuserdetails).Tables[0];
                TopUpPaymentOrderId = Convert.ToInt32(dtCDH.Rows[0]["TopUpPaymentOrderId"]);
                GlobalFunctions.SetUsersCookies(dtUser);
            }


            PaymentTransaction objpt = new PaymentTransaction();
            objpt.OrderId = orderId;
            objpt.OrderAmount = orderAmount;
            objpt.ReferenceId = referenceId;
            objpt.TxStatus = txStatus;
            objpt.PaymentMode = paymentMode;
            objpt.TxMsg = txMsg;
            objpt.TxTime = txTime;
            objpt.Signature = signature;
            objpt.UserID = GlobalFunctions.GetCookie("UserID");
            string ptmsg = objpt._Insert("procPaymentTransaction", objpt);

            string signatureData = orderId + orderAmount + referenceId + txStatus + paymentMode + txMsg + txTime;

            var hmacsha256 = new HMACSHA256(StringEncode(secretKey));
            byte[] gensignature = hmacsha256.ComputeHash(StringEncode(signatureData));
            string computedsignature = Convert.ToBase64String(gensignature);
            if (signature == computedsignature)
            {
                ViewData["panel"] = "panel panel-success";
                ViewData["heading"] = "Signature Verification Successful";

            }
            else
            {
                ViewData["panel"] = "panel panel-danger";
                ViewData["heading"] = "Signature Verification Failed";

            }
            ViewData["orderId"] = orderId;
            ViewData["orderAmount"] = orderAmount;
            ViewData["referenceId"] = referenceId;
            ViewData["txStatus"] = txStatus;
            ViewData["txMsg"] = txMsg;
            ViewData["txTime"] = txTime;
            ViewData["paymentMode"] = paymentMode;

            TopUpPayment objt = new TopUpPayment();
            objt.OrderId = TopUpPaymentOrderId;
            objt.PaymentDetails = "OrderId:" + orderId + ",orderAmount:" + orderAmount + ",referenceId:" + referenceId + ",txStatus:" + txStatus + ",txMsg:" + txMsg + ",txTime:" + txTime + ",paymentMode:" + paymentMode;
            if (txStatus == "SUCCESS")
            {
                objt.PaymentStatus = true;
                string customerid = GlobalFunctions.GetCookie("UserID");
                if (!string.IsNullOrEmpty(customerid))
                {
                    GlobalFunctions.AddAnnouncement("Topup payment successfull", "Topup payment successfull of order no "+ TopUpPaymentOrderId, 0, 0);//admin
                    GlobalFunctions.AddAnnouncement("Topup payment successfull", "Topup payment successfull of order no " + TopUpPaymentOrderId, 0, GlobalFunctions.GetUserId());
                }
            }
            else
            {
                objt.PaymentStatus = false;
                GlobalFunctions.AddAnnouncement("Topup payment fail", "Topup payment fail of order no " + TopUpPaymentOrderId, 0, 0);//admin
                GlobalFunctions.AddAnnouncement("Topup payment fail", "Topup payment fail of order no " + TopUpPaymentOrderId, 0, GlobalFunctions.GetUserId());
            }
            string msg = objt._Select("procTopUpPayment", "UpdatePaymentStatus", objt, true);
            return View();
        }

        private static byte[] StringEncode(string text)
        {
            var encoding = new UTF8Encoding();
            return encoding.GetBytes(text);
        }

        public ActionResult Error()
        {
            //return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return View();
        }
    }
}