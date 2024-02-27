using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WagharalkarMVCProject.Data;
using WagharalkarMVCProject.Models;

namespace WagharalkarMVCProject.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetailIndex(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public ActionResult AppointmentList()
        {
            return View();
        }

        //public ActionResult SaveAppointment(AppointmentModel model)
        //{
        //    try
        //    {
        //        return Json(new { success = true, model = (new AppointmentModel()).saveAppointment(model) }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, error = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public string SaveAppointment(AppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                                                .SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage));
                return "Validation failed: " + errors;
            }

            try
            {
                using (var db = new WagharalKarDBEntities())
                {
                    var existingAppointment = db.tblAppointments.FirstOrDefault(x => x.Id == model.Id);

                    if (existingAppointment == null)
                    {
                        var newAppointment = new tblAppointment
                        {
                            Name = model.Name,
                            Email = model.Email,
                            City = model.City,
                            State = model.State,
                            MobileNo = model.MobileNo,
                            AppointmentDate = model.AppointmentDate,
                            Gender = model.Gender,
                            Message = model.Message,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now,
                            CreatedBy = model.CreatedBy,
                            UpdatedBy = model.UpdatedBy
                        };

                        db.tblAppointments.Add(newAppointment);
                        db.SaveChanges();

                        return "Appointment saved successfully";
                    }
                    else
                    {
                        existingAppointment.Name = model.Name;
                        existingAppointment.Email = model.Email;
                        existingAppointment.City = model.City;
                        existingAppointment.State = model.State;
                        existingAppointment.MobileNo = model.MobileNo;
                        existingAppointment.AppointmentDate = model.AppointmentDate;
                        existingAppointment.Gender = model.Gender;
                        existingAppointment.Message = model.Message;
                        existingAppointment.UpdateDate = DateTime.Now;
                        existingAppointment.UpdatedBy = model.UpdatedBy;

                        db.SaveChanges();

                        return "Appointment updated successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                return "An error occurred while saving the appointment: " + ex.Message;
            }
        }

        public ActionResult GetAppointmentList()
        {
            try
            {
                return Json(new { model = (new AppointmentModel()).GetAppointmentList() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteAppointmentRow(int id)
        {
            try
            {
                return Json(new { model = (new AppointmentModel().DeleteAppointment(id) )}, JsonRequestBehavior.AllowGet);

            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditAppointmentRow(int id)
        {
            try
            {
                return Json(new { model = (new AppointmentModel().EditAppointmentRow(id)) }, JsonRequestBehavior.AllowGet); 
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
                return Json(new { model = (new AppointmentModel().GetAppointmentListById(id)) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult AppointmentDetails(int id)
        //{
        //    try
        //    {
        //        return Json(new { model = (new AppointmentModel().AppointmentDetails(id)) },
        //            JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception Ex)
        //    {
        //        return Json(new { Ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}