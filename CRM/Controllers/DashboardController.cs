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
    public class DashboardController : BaseController
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            Dashboard obj = new Dashboard();
            return View();
        }
        public ActionResult dashboard()
        {
            try {
                string Roleid = GlobalFunctions.GetCookie("RoleId");
                if (Roleid == "2") // admin dashboard
                {
                    Dashboard obj = new Dashboard();
                    obj = GlobalFunctions.ConverDataTableToList<Dashboard>(obj._Select("procDashboard", "AdminDashboard").Tables[0]).FirstOrDefault();

                    Announcement objAnnouncement = new Announcement();
                    DataTable dt = objAnnouncement._Select("procAnnouncement", "ForAdmin", objAnnouncement).Tables[0];
                    List<vmAnnouncement> objlist = GlobalFunctions.ConverDataTableToList<vmAnnouncement>(dt);
                    ViewBag.Announcement = objlist;
                    return View("/Views/Dashboard/_AdminDashboard.cshtml",obj);
                }

                /*3-Staff, 5-Script Writer,6-Voice Artist,7-Video Artist*/
                if (Roleid == "3"|| Roleid == "5" || Roleid == "6" || Roleid == "7")
                {
                    Dashboard obj = new Dashboard();
                    obj = GlobalFunctions.ConverDataTableToList<Dashboard>(obj._Select("procDashboard", "ArtistDashboard").Tables[0]).FirstOrDefault();
                    return View("/Views/Dashboard/_ArtistDashboard.cshtml",obj);
                }
                if(Roleid=="4")//customer
                {
                    Orders objOrder = new Orders();
                    objOrder.CustomerId = Convert.ToInt32(GlobalFunctions.GetCookie("UserId"));
                    DataTable dt = objOrder._Select("procOrders", "CustomerLatestOrder", objOrder).Tables[0];
                    //List<vmCustomerLatestOrder> objLIst = GlobalFunctions.ConverDataTableToList<vmCustomerLatestOrder>(dt);
                    ViewBag.CustomerLatestOrder = dt;
                    return View("/Views/Dashboard/_CustomerDashboard.cshtml");
                }

            }
            catch(Exception ex)
            {
                ExceptionLogs objex = new ExceptionLogs();
                objex.Save(ex, "DashboardController" + ">" + "Dashboard");
            }
            return View();
        }
        public ActionResult AdminTotalDataForChart()
        {
            Dashboard obj = new Dashboard();
            DataTable dt= obj._Select("procDashboard", "AdminTotalDataForChart").Tables[0];
            List<int> chart = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                chart.Add(Convert.ToInt32(dt.Rows[i][0]));
            }
            return Json(chart);
        }
        public ActionResult _GetArtistDashboardByAdmin(Dashboard obj)
        {
            obj =GlobalFunctions.ConverDataTableToList<Dashboard>(obj._Select("procDashboard", "GetArtistDashboardByAdmin", obj).Tables[0]).FirstOrDefault();
            return View(obj);
        }
    }
}