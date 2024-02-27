using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WagharalkarMVCProject.Models;

namespace WagharalkarMVCProject.Controllers
{
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CityList()
        {
            return View();
        }


        public ActionResult GetCityList(int StateId) //changes 14/02/2024
        {
            try
            {
                return Json(new { model = (new CityModel().GetCityList(StateId)) }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}