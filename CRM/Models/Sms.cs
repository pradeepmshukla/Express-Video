using System;
using System.Collections.Generic;
using System.Net;
using System.Collections.Specialized;
using System.Web;
using System.Configuration;

namespace CRM.Models
{
    public class SMS
    {
        public string getSenderName()
        {
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.textlocal.in/get_sender_names/", new NameValueCollection()
                {
                {"apikey" , ConfigurationManager.AppSettings["sms_apikey"]}
                });

                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }
        public string sendSMS(string mobileno, string sms)
        {
            String message = HttpUtility.UrlEncode(sms);
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , ConfigurationManager.AppSettings["sms_apikey"]},
                {"numbers" , mobileno},
                {"message" , message},
                {"sender" , "MFPEXP"}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }
    }

    public class SMSResponse
    {
        public string balance { get; set; }
        public string batch_id { get; set; }
        public string cost { get; set; }
        public string num_messages { get; set; }
        public message message { get; set; }
        public List<messages> messages { get; set; }
        public string status { get; set; }
    }
    public class message
    {
        public string num_parts { get; set; }
        public string sender { get; set; }
        public string content { get; set; }
    }
    public class messages
    {
        public string id { get; set; }
        public string recipient { get; set; }
    }
}