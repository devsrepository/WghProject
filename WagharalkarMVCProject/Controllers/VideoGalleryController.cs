using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WagharalkarMVCProject.Models;

namespace WagharalkarMVCProject.Controllers
{
    public class VideoGalleryController : Controller
    {
        // GET: VideoGallery
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VideoGalleryList()
        {
            return View();
        }

        public ActionResult DetailIndex(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public ActionResult saveVideoGallery(VideoGalleryModel model)
        {
            try
            {
                return Json(new { model = (new VideoGalleryModel().saveVideoGallery(model)) },
                JsonRequestBehavior.AllowGet);
            }
            catch(Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }

            
        }

        public ActionResult GetVideoGalleryList()
        {
            try
            {
                return Json(new { model = (new VideoGalleryModel().GetVideoGalleryList()) },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }


        public ActionResult DeleteVideoGalleryRow(int id)
        {
            try
            {
                return Json(new { model = (new VideoGalleryModel().DeleteVideoGalleryRow(id)) },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult EditVideoGalleryRow(int id)
        {
            try
            {
                return Json(new { model = (new VideoGalleryModel().EditVideoGalleryRow(id)) },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetListById(int id)
        {
            try
            {
                return Json(new { model = (new VideoGalleryModel().GetVideoGalleryListById(id)) },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}