using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WagharalkarMVCProject.Data;

namespace WagharalkarMVCProject.Models
{
    public class ActivityModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Type { get; set; }
        public string Date { get; set; } //change from Nullable<System.DateTime> to string as we want the date in ddmmyyyy format
        public string CreateDate { get; set; } //all date datatype change to string
        public string UpdateDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }


        public string saveActivity(HttpPostedFileBase fb, ActivityModel model)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
           
            string msg ="" ;
            string filePath = "";
            string fileName = "";
            string sysFileName = "";
            if(fb !=null && fb.ContentLength > 0)
            {
                filePath = HttpContext.Current.Server.MapPath("../Content/img");
                DirectoryInfo di = new DirectoryInfo(filePath);
                if(!di.Exists)
                {
                    di.Create();
                }
                fileName = fb.FileName;
                sysFileName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb.FileName);
                fb.SaveAs(filePath + "//" + sysFileName);
                if(!string.IsNullOrWhiteSpace(fb.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("/Content/img") + "/" + sysFileName;
                }
            }

            var getEditData = db.tblActivities.Where(x => x.ID == model.ID).FirstOrDefault();

            if(getEditData==null)
            {
                var saveActivity = new tblActivity()
                {
                    ID=model.ID,
                    Title = model.Title,
                    Details = model.Details,
                    Image1 = sysFileName,
                    Image2 = model.Image2,
                    Type = model.Type,
                    Date = Convert.ToDateTime(model.Date),
                    CreateDate =Convert.ToDateTime(model.CreateDate),
                    UpdateDate =Convert.ToDateTime(model.UpdateDate),
                    CreatedBy = model.CreatedBy,
                    UpdatedBy = model.UpdatedBy
                };

                db.tblActivities.Add(saveActivity);
                db.SaveChanges();
                msg = "activities done successfully";
                return msg;
            }
            else
            {
                getEditData.ID = model.ID;
                getEditData.Title = model.Title;
                getEditData.Details=model.Details;
                getEditData.Image1 = model.Image1;
                getEditData.Image2 = model.Image2;
                getEditData.Type = model.Type;
                getEditData.Date = Convert.ToDateTime(model.Date);
                getEditData.CreateDate =Convert.ToDateTime(model.CreateDate);
                getEditData.UpdateDate = Convert.ToDateTime(model.UpdateDate);
                getEditData.CreatedBy = model.CreatedBy;
                getEditData.UpdatedBy = model.UpdatedBy;
            }

            db.SaveChanges();
            msg="row updated successfully";
            return msg;




        }

        //to get the list from model
        public List<ActivityModel> GetActivityList()
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            List<ActivityModel> lstActivity = new List<ActivityModel>();//make the list obj
            var getlist = db.tblActivities.ToList(); // store data to the variable getlist using linq ToList().
            if(getlist !=null)
            {
                foreach(var list in getlist) // to fetch each n every row which stores in lstActivity obj
                {
                    lstActivity.Add(new ActivityModel()
                    {
                        ID = list.ID,
                        Title = list.Title,
                        Details = list.Details,
                        Image1 = list.Image1,
                        Image2 = list.Image2,
                        Type = list.Type,
                        Date =Convert.ToDateTime(list.Date).ToShortDateString(),
                        CreateDate =Convert.ToDateTime(list.CreateDate).ToShortDateString(),
                        UpdateDate =Convert.ToDateTime(list.UpdateDate).ToShortDateString(),
                        CreatedBy = list.CreatedBy,
                        UpdatedBy = list.UpdatedBy

                    });
                }
                
            }
            
            return lstActivity;  // return to activitycontroller
        }

        public string DeleteActivity(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            string msg = "";

            var deleteRecord = db.tblActivities.Where(p => p.ID == id).FirstOrDefault();
            if(deleteRecord != null)
            {
                db.tblActivities.Remove(deleteRecord);
            }

            db.SaveChanges();
            msg = "row deleted successfully";
            return msg;
        }

        public ActivityModel EditActivityRow(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            ActivityModel model = new ActivityModel();
            //string msg = "";
            var Record = db.tblActivities.Where(x => x.ID == id).FirstOrDefault();

            if(Record != null)
            {
                //Here we are transfering the selected row which we are going to edit/update
                //from record object to model of the db.
                //here we transfer data to model because fisrt it needs to get that data in model so that we can return that model to controller.
                model.ID = Record.ID;
                model.Title = Record.Title;
                model.Details = Record.Details;
                model.Image1 = Record.Image1;
                model.Image2 = Record.Image2;
                model.Type = Record.Type;
                model.Date=Convert.ToDateTime(Record.Date).ToShortDateString();
                model.CreateDate =Convert.ToDateTime(Record.CreateDate).ToShortDateString();
                model.UpdateDate =Convert.ToDateTime(Record.UpdateDate).ToShortDateString();
                model.CreatedBy= Record.CreatedBy;
                model.UpdatedBy = Record.UpdatedBy;

               
            }

            //msg = "row is ready for edit";
            return model;



        }

        public List<ActivityModel> GetActivityDetailsList(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            List<ActivityModel> lstActivity = new List<ActivityModel>();//make the list obj
            var getlist = db.tblActivities.Where(x=>x.ID==id).ToList();// or FirstOrDefault() // store data to the variable getlist using linq ToList().
            if (getlist != null)
            {
                foreach (var list in getlist) // to fetch each n every row which stores in lstActivity obj
                {
                    lstActivity.Add(new ActivityModel()
                    {
                        ID = list.ID,
                        Title = list.Title,
                        Details = list.Details,
                        Image1 = list.Image1,
                        Image2 = list.Image2,
                        Type = list.Type,
                        Date = Convert.ToDateTime(list.Date).ToShortDateString(),
                        CreateDate = Convert.ToDateTime(list.CreateDate).ToShortDateString(),
                        UpdateDate = Convert.ToDateTime(list.UpdateDate).ToShortDateString(),
                        CreatedBy = list.CreatedBy,
                        UpdatedBy = list.UpdatedBy

                    });
                }

            }

            return lstActivity;  // return to activitycontroller
        }


    }
}