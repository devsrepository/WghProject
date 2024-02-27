using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WagharalkarMVCProject.Models;

namespace WagharalkarMVCProject.Controllers
{
    public class DoctorsController : Controller
    {
        // GET: Doctors
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DoctorsList()
        {
            return View();
        }

        public ActionResult saveDoctors(DoctorsModel model)
        {
            try
            {
                HttpPostedFileBase fb = null;
                for(int i=0;i< Request.Files.Count;i++)
                {
                    fb = Request.Files[i];
                }
                return Json(new { Message = new DoctorsModel().saveDoctors(fb,model)}, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetDoctorsList()
        {
            try
            {
                return Json(new { model = new DoctorsModel().GetDoctorsList() }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}