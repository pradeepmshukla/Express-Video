using CRM.Models.Global;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class VoiceArtistSoundsController : Controller
    {
        // GET: VoiceArtistSounds
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _ViewSample(VoiceArtistSounds obj)
        {
            DataTable dt = obj._Select("procVoiceArtistSounds", "_ViewSample", obj).Tables[0];
            List<VoiceArtistSounds> objList = GlobalFunctions.ConverDataTableToList<VoiceArtistSounds>(dt);
            return View(objList);
        }
        public ActionResult SaveArtistSound(VoiceArtistSounds obj)
        {
            obj.VoiceArtistUserId = GlobalFunctions.GetUserId();
            obj.Price = 0;
            string msg = obj._Insert("procVoiceArtistSounds", obj);
            return Json(msg);
        }
        public ActionResult DeleteSound(VoiceArtistSounds obj)
        {
            obj.VoiceArtistUserId = GlobalFunctions.GetUserId();
            string msg = obj._Select("procVoiceArtistSounds","DeleteSound",obj,true);
            return Json(msg);
        }
    }
}