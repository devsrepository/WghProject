using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WagharalkarMVCProject.Data;

namespace WagharalkarMVCProject.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^\+[0-9]{1,3}-[0-9]{3,14}$", ErrorMessage = "Please enter a valid phone number.")]
        public string MobileNo { get; set; }
        public string AppointmentDate { get; set; }
        public string Gender { get; set; }
        public string Message { get; set; }
        public string UpdateDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string CreateDate { get; set; }
        public Nullable<int> State { get; set; }
        public int? City { get; set; }
    
        public string CityName { get; set; } //20/02/2024
        public string StateName { get; set; } //20/02/2024




        //public string saveAppointment(AppointmentModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        // If the ModelState is not valid, return validation errors
        //        var errors = string.Join("; ", ModelState.Values
        //                                        .SelectMany(v => v.Errors)
        //                                        .Select(e => e.ErrorMessage));
        //        return "Validation failed: " + errors;
        //    }
        //    WagharalKarDBEntities db = new WagharalKarDBEntities();
        //    string msg = "";
        //    var getEditRecord = db.tblAppointments.Where(x => x.Id == model.Id).FirstOrDefault();

        //    if (getEditRecord == null)
        //    {
        //        var savAppt = new tblAppointment()
        //        {
        //            Id = model.Id,
        //            Name = model.Name,
        //            Email = model.Email,
        //            City = model.City,
        //            State = model.State,
        //            MobileNo = model.MobileNo,
        //            AppointmentDate = model.AppointmentDate,
        //            Gender = model.Gender,
        //            Message = model.Message,
        //            CreateDate = Convert.ToDateTime(model.CreateDate),
        //            UpdateDate = Convert.ToDateTime(model.UpdateDate),
        //            CreatedBy = model.CreatedBy,
        //            UpdatedBy = model.UpdatedBy
        //        };

        //        db.tblAppointments.Add(savAppt);
        //        db.SaveChanges();
        //        msg = "record saved successfully";
        //        return msg;
        //    }

        //    else
        //    {
        //        getEditRecord.Id = model.Id;
        //        getEditRecord.Name = model.Name;
        //        getEditRecord.Email = model.Email;
        //        getEditRecord.City = model.City;
        //        getEditRecord.State = model.State;
        //        getEditRecord.MobileNo = model.MobileNo;
        //        getEditRecord.AppointmentDate = model.AppointmentDate;
        //        getEditRecord.Gender = model.Gender;
        //        getEditRecord.Message = model.Message;
        //        getEditRecord.CreateDate = Convert.ToDateTime(model.CreateDate);
        //        getEditRecord.UpdateDate = Convert.ToDateTime(model.UpdateDate);
        //        getEditRecord.CreatedBy = model.CreatedBy;
        //        getEditRecord.UpdatedBy = model.UpdatedBy;

        //    }

        //    db.SaveChanges();
        //    msg = "row updated successfully";
        //    return msg;


        //}
        //1. get data from db through model in table.
        public List<AppointmentModel> GetAppointmentList()
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            List<AppointmentModel> lstAppointment = new List<AppointmentModel>();
            //var getList = db.tblAppointments.ToList();
            var getList = (from a in db.tblAppointments
                           join c in db.tblCities on a.City equals c.CityId
                           join s in db.tblStates on a.State equals s.StateId
                           select new
                           {
                               a.Id,
                               a.Name,
                               a.Email,
                               c.CityName,
                               s.StateName,
                               a.MobileNo,
                               a.AppointmentDate,
                               a.Gender,
                               a.Message,
                               a.CreateDate,
                               a.UpdateDate,
                               a.CreatedBy,
                               a.UpdatedBy
                           }).ToList();
            if(getList != null)
            {
                foreach(var list in getList)
                {// here, add each row data in list obj
                    lstAppointment.Add(new AppointmentModel()
                    { //view table=db model table
                        Id=list.Id,
                        Name = list.Name,
                        Email = list.Email,
                        CityName = list.CityName, //20/02/2024
                        StateName=list.StateName,
                        MobileNo = list.MobileNo,
                        AppointmentDate = list.AppointmentDate,
                        Gender = list.Gender,
                        Message = list.Message,
                        CreateDate = Convert.ToDateTime(list.CreateDate).ToShortDateString(),
                        UpdateDate = Convert.ToDateTime(list.UpdateDate).ToShortDateString(),
                        CreatedBy = list.CreatedBy,
                        UpdatedBy = list.UpdatedBy
                    });
                }
            }
            return lstAppointment; // return to appointmentcontroller
        }
        //from the list of record, there is delete button. on cliccking on it,row should be deleted.
        //
        public string DeleteAppointment(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            var msg = "";
            var record = db.tblAppointments.Where(x => x.Id == id).FirstOrDefault();

            if(record != null)
            {
                db.tblAppointments.Remove(record);
            }

            db.SaveChanges();
            msg = "record deleted successfully";
            return msg;
        }

        public AppointmentModel EditAppointmentRow(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            var getEditRecord = db.tblAppointments.Where(x => x.Id == id).FirstOrDefault();
            AppointmentModel appointmentModel = new AppointmentModel();

            if(getEditRecord!=null)
            {
                appointmentModel.Id= getEditRecord.Id;
                appointmentModel.Name = getEditRecord.Name;
                appointmentModel.Email = getEditRecord.Email;
                appointmentModel.City = getEditRecord.City;
                appointmentModel.State = getEditRecord.State;
                appointmentModel.MobileNo = getEditRecord.MobileNo;
                appointmentModel.AppointmentDate = getEditRecord.AppointmentDate;
                appointmentModel.Gender = getEditRecord.Gender;
                appointmentModel.Message = getEditRecord.Message;
                appointmentModel.CreateDate = Convert.ToDateTime(getEditRecord.CreateDate).ToShortDateString();
                appointmentModel.UpdateDate = Convert.ToDateTime(getEditRecord.UpdateDate).ToShortDateString();
                appointmentModel.CreatedBy = getEditRecord.CreatedBy;
                appointmentModel.UpdatedBy = getEditRecord.UpdatedBy;

            }

        
            return appointmentModel;



        }

        public List<AppointmentModel> GetAppointmentListById(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            var getList = db.tblAppointments.Where(x => x.Id == id).ToList();
            List<AppointmentModel> lstAppointment = new List<AppointmentModel>();
            if(getList != null)
            {
                foreach(var list in getList)
                {
                    lstAppointment.Add(new AppointmentModel()
                    {
                        Id=list.Id,
                        Name=list.Name,
                        Email = list.Email,
                        City =list.City,
                        State=list.State,
                        MobileNo=list.MobileNo,
                        AppointmentDate=list.AppointmentDate,
                        Gender=list.Gender,
                        Message=list.Message,
                        CreateDate=Convert.ToDateTime(list.CreateDate).ToShortDateString(),
                        UpdateDate=Convert.ToDateTime(list.UpdateDate).ToShortDateString(),
                        CreatedBy=list.CreatedBy,
                        UpdatedBy=list.UpdatedBy


                    });
                }
            }
            return lstAppointment;

        }

        
    }    
}