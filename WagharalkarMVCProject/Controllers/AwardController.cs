using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WagharalkarMVCProject.Models;

namespace WagharalkarMVCProject.Controllers
{
    public class AwardController : Controller
    {
        // GET: Award
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetailIndex(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public ActionResult AwardList()
        {
            return View();
        }

        public ActionResult saveAward(AwardModel model)
        {
            try
            {
                HttpPostedFileBase fb1 = null;
                HttpPostedFileBase fb2 = null;
                for(int i=0;i<Request.Files.Count;i++)
                {
                    fb1 = Request.Files[0];
                    fb2 = Request.Files[1];
                }
                return Json(new
                {
                    model = (new AwardModel().saveAward(fb1,fb2,model)),
                    JsonRequestBehavior.AllowGet,

                });
                //before = Message=
                //for edit operation change Message to model
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAwardList()
        {
            try
            {
                return Json(new { model = (new AwardModel()).GetAwardList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteAward(int id)
        {
            try
            {
                return Json(new { model = (new AwardModel()).DeleteAward(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditAwardRow(int id)
        {
            try
            {
                //HttpPostedFileBase fb = null;
                //for(int i=0; i<Request.Files.Count; i++)
                //{
                //    fb = Request.Files[i];
                //}
                return Json(new { model = (new AwardModel().EditAwardRow(id)) }, JsonRequestBehavior.AllowGet);
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

                return Json(new { model = (new AwardModel().GetAwardListById(id)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}