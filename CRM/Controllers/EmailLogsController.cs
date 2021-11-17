using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class EmailLogsController : Controller
    {
        // GET: EmailLogs
        public ActionResult Index()
        {
            return View();
        }
    }
}