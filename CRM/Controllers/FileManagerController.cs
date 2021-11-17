using CRM.Models.Global;
using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class FileManagerController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            string fname = "";
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string Newfilename = "";
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];


                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        string FileExtension = fname.Split('.')[1];
                        string fileGuid = Guid.NewGuid().ToString();
                        Newfilename = fileGuid + "." + FileExtension;
                        // Get the complete folder path and store the file inside it.  
                        string path = "~/Uploads/";
                        string savefname = Path.Combine(Server.MapPath(path), Newfilename);
                        file.SaveAs(savefname);

                        FileManager objfile = new FileManager();
                        objfile.FileGuid = Newfilename;
                        objfile.FileName = fname;
                        objfile.FilePath = path;
                        objfile.FileExtension = FileExtension;
                        objfile.FileSize = (file.ContentLength / 1024f) / 1024f;
                        objfile.UploadFrom = "";
                        objfile.UploadedBy = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));

                        string msg = objfile._Insert("procFileManager", objfile);
                    }
                    // Returns message that successfully uploaded  
                    return Json(Newfilename);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fname = "", Newfilename = "";
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                // Checking for Internet Explorer  
                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                }

                string FileExtension = fname.Split('.')[1];
                string fileGuid = Guid.NewGuid().ToString();
                Newfilename = fileGuid + "." + FileExtension;
                // Get the complete folder path and store the file inside it.  
                string path = "~/Uploads/";
                string savefname = Path.Combine(Server.MapPath(path), Newfilename);
                file.SaveAs(savefname);

                FileManager objfile = new FileManager();
                objfile.FileGuid = Newfilename;
                objfile.FileName = fname;
                objfile.FilePath = path;
                objfile.FileExtension = FileExtension;
                objfile.FileSize = (file.ContentLength / 1024f) / 1024f;
                objfile.UploadFrom = "";
                objfile.UploadedBy = Convert.ToInt32(GlobalFunctions.GetCookie("UserID"));

                string msg = objfile._Insert("procFileManager", objfile);
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = "File saved" });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        public ActionResult DeleteUploadedFile(FileManager obj)
        {
            string msg = "";
            try
            {
                string path = "~/Uploads/";
                string filepath = Path.Combine(Server.MapPath(path), obj.FileName);
                System.IO.File.Delete(filepath);
                msg = "success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Json(msg);
        }
    }
}