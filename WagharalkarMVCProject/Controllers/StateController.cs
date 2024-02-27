using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WagharalkarMVCProject.Models;

namespace WagharalkarMVCProject.Controllers
{
    public class StateController : Controller
    {
        // GET: State
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StatesList()
        {
            return View();
        }
        public ActionResult GetStatesList()
        {
            try
            {
                return Json(new { model = (new StateModel().GetStatesList()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}