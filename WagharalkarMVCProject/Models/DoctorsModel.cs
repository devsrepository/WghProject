using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WagharalkarMVCProject.Data;

namespace WagharalkarMVCProject.Models
{
    public class DoctorsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }
        public string Education { get; set; }
        public string InstaGramLink { get; set; }
        public string FacebookLink { get; set; }
        public string LinkedInLink { get; set; }


        public string saveDoctors(HttpPostedFileBase fb, DoctorsModel model)
        {
            string msg = "";
            string filePath = "";
            string fileName = "";
            string sysFileName = "";

            if(fb !=null && fb.ContentLength >0)
            {
                filePath = HttpContext.Current.Server.MapPath("../Content/img");
                DirectoryInfo di = new DirectoryInfo(filePath);
                if(di.Exists)
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

            WagharalKarDBEntities db = new WagharalKarDBEntities();
            var getEditRecord = db.tblDoctors.Where(x => x.Id == model.Id).FirstOrDefault();
            if (getEditRecord == null)
            {
                var saveDoctor = new tblDoctor()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Image = sysFileName,
                    Designation = model.Designation,
                    Description = model.Description,
                    Education = model.Education,
                    InstaGramLink = model.InstaGramLink,
                    FacebookLink = model.FacebookLink,
                    LinkedInLink = model.LinkedInLink


                };
                db.tblDoctors.Add(saveDoctor);
                db.SaveChanges();
                return msg = "row saved successfully";

            }
            else
            {
                getEditRecord.Id = model.Id;
                getEditRecord.Name = model.Name;
                getEditRecord.Image = sysFileName;
                getEditRecord.Designation = model.Designation;
                getEditRecord.Description = model.Description;
                getEditRecord.Education = model.Education;
                getEditRecord.InstaGramLink = model.InstaGramLink;
                getEditRecord.FacebookLink = model.FacebookLink;
                getEditRecord.LinkedInLink = model.LinkedInLink;


            }
            db.SaveChanges();
            msg = "row edited successfully";
            return msg;
        }

        public List<DoctorsModel> GetDoctorsList()
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            var getList = db.tblDoctors.ToList();
            List<DoctorsModel> lstDoctor = new List<DoctorsModel>();
            if(getList !=null)
            {
                foreach(var list in getList)
                {
                    lstDoctor.Add(new DoctorsModel
                    {
                      Id=list.Id,
                      Name=list.Name,
                      Image=list.Image,
                      Designation=list.Designation,
                      Description=list.Description,
                      Education=list.Education,
                      InstaGramLink=list.InstaGramLink,
                      FacebookLink=list.FacebookLink,
                      LinkedInLink=list.LinkedInLink

                    });
                }
            }

            return lstDoctor;

        }
    }
}