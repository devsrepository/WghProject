using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WagharalkarMVCProject.Data;

namespace WagharalkarMVCProject.Models
{
    public class AwardModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Type { get; set; }
        public string Date { get; set; } // change from Nullable<System.DateTime> to string as we want the date in ddmmyyyy format
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }

        public string saveAward(HttpPostedFileBase fb1, HttpPostedFileBase fb2, AwardModel model)
        {
            string msg = "";
            string filePath1 = "";
            string fileName1 = "";
            string sysFileName1 = "";
            string filePath2 = "";
            string fileName2 = "";
            string sysFileName2 = "";
            if (fb1 != null && fb1.ContentLength > 0)
            {
                
                filePath1 = HttpContext.Current.Server.MapPath("../Content/img");
                DirectoryInfo di = new DirectoryInfo(filePath1);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName1 = fb1.FileName;
                sysFileName1 = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb1.FileName);
                fb1.SaveAs(filePath1 + "//" + sysFileName1);
                if (!string.IsNullOrWhiteSpace(fb1.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("/Content/img") + "/" + sysFileName1;

                }
            }
            if (fb2 != null && fb2.ContentLength > 0)
            {

                filePath2 = HttpContext.Current.Server.MapPath("../Content/img");
                DirectoryInfo di = new DirectoryInfo(filePath1);
                if (!di.Exists)
                {
                    di.Create();
                }
                fileName2 = fb1.FileName;
                sysFileName2 = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb2.FileName);
                fb2.SaveAs(filePath2 + "//" + sysFileName2);
                if (!string.IsNullOrWhiteSpace(fb2.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("/Content/img") + "/" + sysFileName2;

                }
            }
            WagharalKarDBEntities db = new WagharalKarDBEntities();

                var getEditRecord = db.tblAwards.Where(x => x.Id == model.Id).FirstOrDefault();//added for edit 
                if (getEditRecord == null)
                {
                    var saveAward = new tblAward()
                    {
                        Id = model.Id, // this is added in edit operation
                        Title = model.Title,
                        Details = model.Details,
                        Image1 = sysFileName1,
                        Image2 =sysFileName2,
                        Type = model.Type,
                        Date = Convert.ToDateTime(model.Date),
                        CreateDate = Convert.ToDateTime(model.CreateDate),
                        UpdateDate = Convert.ToDateTime(model.UpdateDate),
                        CreatedBy = model.CreatedBy,
                        UpdatedBy = model.UpdatedBy
                    };
                    db.tblAwards.Add(saveAward);
                    db.SaveChanges();
                    msg = "award saved successfully";
                    return msg;
                }
                else
                {
                    getEditRecord.Id = model.Id;
                    getEditRecord.Title = model.Title;
                    getEditRecord.Details = model.Details;
                    getEditRecord.Image1 = sysFileName1;
                    getEditRecord.Image2 = sysFileName2;
                    getEditRecord.Type = model.Type;
                    getEditRecord.Date = Convert.ToDateTime(model.Date); //here datatype of date is string hence convert string to datetime
                    getEditRecord.CreateDate = Convert.ToDateTime(model.CreateDate);
                    getEditRecord.UpdateDate = Convert.ToDateTime(model.UpdateDate);
                    getEditRecord.CreatedBy = model.CreatedBy;
                    getEditRecord.UpdatedBy = model.UpdatedBy;
                }
                db.SaveChanges();
                msg = "row updated successfully";
                return msg;

        }
        
        public List<AwardModel> GetAwardList()
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            List<AwardModel> lstAward = new List<AwardModel>();
            var getList = db.tblAwards.ToList();

            if (getList != null)
            {
                foreach (var list in getList)
                {
                    lstAward.Add(new AwardModel()
                    {
                        Id = list.Id,
                        Title = list.Title,
                        Details = list.Details,
                        Image1 = list.Image1,
                        Image2 = list.Image2,
                        Type =list.Type, 
                        Date =Convert.ToDateTime(list.Date).ToShortDateString(),
                        CreateDate = Convert.ToDateTime(list.CreateDate).ToShortDateString(),//here,we want to show date in input field and datetype is string.. and in database table its datatype is datetime
                        UpdateDate = Convert.ToDateTime(list.UpdateDate).ToShortDateString(), // hence,from database table , value is going to view. so need to convert from datetime to string.
                        CreatedBy = list.CreatedBy,
                        UpdatedBy = list.UpdatedBy
                    });
                }
            }
            return lstAward; // goes to controller
        }

        public string DeleteAward(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            var msg = "";
            var record = db.tblAwards.Where(x => x.Id == id).FirstOrDefault();

            if (record != null)
            {
                db.tblAwards.Remove(record);
            }

            db.SaveChanges();
            msg = "row deleted successfully";
            return msg;


        }

        public AwardModel EditAwardRow(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            var getEditRecord = db.tblAwards.Where(x => x.Id == id).FirstOrDefault();
            AwardModel awardModel = new AwardModel();

            if (getEditRecord != null)
            {
                awardModel.Id = getEditRecord.Id;
                awardModel.Title = getEditRecord.Title;
                awardModel.Details = getEditRecord.Details;
                awardModel.Image1 = getEditRecord.Image1;
                awardModel.Image2 = getEditRecord.Image2;
                awardModel.Type = getEditRecord.Type;
                awardModel.Date =Convert.ToDateTime(getEditRecord.Date).ToShortDateString();
                awardModel.CreateDate = Convert.ToDateTime(getEditRecord.CreateDate).ToShortDateString();
                awardModel.UpdateDate = Convert.ToDateTime(getEditRecord.UpdateDate).ToShortDateString();
                awardModel.CreatedBy = getEditRecord.CreatedBy;
                awardModel.UpdatedBy = getEditRecord.UpdatedBy;
            }

            return awardModel;
        }


        public List<AwardModel> GetAwardListById(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();

            var getList = db.tblAwards.Where(x => x.Id == id).ToList();
            List<AwardModel> lstAward = new List<AwardModel>();

            if(getList !=null)
            {
                foreach(var list in getList)
                {
                    lstAward.Add(new AwardModel()
                    {
                        Id = list.Id,
                        Title = list.Title,
                        Details = list.Details,
                        Image1 = list.Image1,
                        Image2 = list.Image2,
                        Type = list.Type,
                        Date =Convert.ToDateTime(list.Date).ToShortDateString(),
                        CreateDate =Convert.ToDateTime(list.CreateDate).ToShortDateString(),
                        UpdateDate=Convert.ToDateTime(list.UpdateDate).ToShortDateString(),
                        CreatedBy=list.CreatedBy,
                        UpdatedBy=list.UpdatedBy
                    });
                    
                }
            }

            return lstAward;
        }
    }
}