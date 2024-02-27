using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WagharalkarMVCProject.Models;

namespace WagharalkarMVCProject.Controllers
{
    public class PhotoGalleryController : Controller
    {
        // GET: PhotoGallery
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PhotoGalleryList()
        {
            return View();
        }

        public ActionResult DetailIndex(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public ActionResult savePhotoGallery(PhotoGalleryModel model)
        {
            try
            {
                HttpPostedFileBase fb1 = null;
                HttpPostedFileBase fb2 = null;

                for(int i = 0; i < Request.Files.Count; i++){
                    fb1 = Request.Files[0];
                    fb2 = Request.Files[1];
                }
                //5.data sends to savePhotoGallery(model) medthod of PhotoGalleryModel.
                return Json(new { Message = (new PhotoGalleryModel().savePhotoGallery(fb1,fb2,model)) },
                  //6. after save,it return to controller
               JsonRequestBehavior.AllowGet);
            }
            catch(Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetPhotoGalleryList()
        {
            try
            {
                
                return Json(new { model = (new PhotoGalleryModel().GetPhotoGalleryList()) },JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeletePhotoGallery(int id)
        {
            try
            {

                return Json(new { model = (new PhotoGalleryModel().DeletePhotoGallery(id)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditPhotGalleryRow(int id)
        {
            try
            {
                return Json(new { model = (new PhotoGalleryModel().EditPhotGalleryRow(id)) }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetListById(int id)
        {
            try
            {
                return Json(new { model = (new PhotoGalleryModel().GetPhotoGalleryListById(id)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}