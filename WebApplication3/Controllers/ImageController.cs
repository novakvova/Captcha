using BLL.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase Image)
        {
            Bitmap image=WorkImage.CreateImage(Image,600,400);
            if(image!=null)
            {
                string path = Server.MapPath(ConfigurationManager.AppSettings["ImagePathSave"]);
                string fileName = path + Image.FileName;// + Path.GetExtension(Image.FileName);
                image.Save(fileName, ImageFormat.Jpeg);
            }
            //if (Image != null && Image.ContentLength != 0 && Image.ContentLength <= 10000000)
            //{
            //    Bitmap file = new Bitmap(Image.InputStream, true);
            //    
            //}

            return View();
        }
    }
}