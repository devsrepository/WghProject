using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WagharalkarMVCProject.Models;

namespace WagharalkarMVCProject.Controllers
{
    public class ActivityController : Controller
    {
        // GET: Activity
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetailIndex(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }


        public ActionResult ActivityList()
        {
            return View();
        }

        public ActionResult saveActivity(ActivityModel model)
        {
            try
            {
                HttpPostedFileBase fb = null;
                for(int i=0;i<Request.Files.Count;i++)
                {
                    fb = Request.Files[i];
                }
                return Json(new { model = (new ActivityModel()).saveActivity(fb,model) }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetActivityList()
        {
            try
           {
                // returns model to to activity.js
                return Json(new { model = (new ActivityModel()).GetActivityList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteActivity(int id)
        {
            try
            {
                return Json(new { model = (new ActivityModel()).DeleteActivity(id) }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditActivityRow(int id)
        {
            try
            {
              //here,id is passed to model method EditActivityRow(id)
                return Json(new { model = (new ActivityModel()).EditActivityRow(id) }, JsonRequestBehavior.AllowGet);
              //here ,the model object that come from Activitymodel,that will again send to the view.
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
                //here,id is passed to model method GetActivityDetailsList(id)
                return Json(new { model = (new ActivityModel().GetActivityDetailsList(id)) }, JsonRequestBehavior.AllowGet);
              //here ,the model object that come from Activitymodel,that will again send to the view.
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}