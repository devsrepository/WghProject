using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WagharalkarMVCProject.Data;

namespace WagharalkarMVCProject.Models
{
    public class PhotoGalleryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Type { get; set; }
        public string CreateDate { get; set; } //10/01/2024 change to Nullable<System.DateTime> to string
        public string UpdateDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }


        public string savePhotoGallery(HttpPostedFileBase fb1,HttpPostedFileBase fb2, PhotoGalleryModel model)
        {
            string fileName1 = "";
            string filePath1 = "";
            string sysFileName1 = "";
            string fileName2 = "";
            string filePath2= "";
            string sysFileName2 = "";

            if(fb1!=null && fb1.ContentLength > 0)
            {
                filePath1 = HttpContext.Current.Server.MapPath("../Content/img");
                DirectoryInfo di = new DirectoryInfo(filePath1);
                if(!di.Exists)
                {
                    di.Create();
                }
                fileName1 = fb1.FileName;
                sysFileName1 = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb1.FileName);
                fb1.SaveAs(filePath1 + "//" + sysFileName1);
                if(!string.IsNullOrWhiteSpace(fb1.FileName))
                {
                    string afileName=HttpContext.Current.Server.MapPath("/Content/img") + "/" + sysFileName1;
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
                fileName2 = fb2.FileName;
                sysFileName2 = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(fb2.FileName);
                fb2.SaveAs(filePath2 + "//" + sysFileName2);
                if (!string.IsNullOrWhiteSpace(fb2.FileName))
                {
                    string afileName = HttpContext.Current.Server.MapPath("/Content/img") + "/" + sysFileName2;
                }
            }
            //6. here, make obj of entity.then save this model field data to the database table photogallery field.
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            string msg = "";
            var getEditRecord = db.tblPhotoGalleries.Where(x => x.Id == model.Id).FirstOrDefault();
            if (getEditRecord == null)
            {
                var savePhotoGal = new tblPhotoGallery()
                {
                    Id = model.Id,
                    Title = model.Title,
                    //Image1 = model.Image1, here image is taken .so this code is commented.
                    //Image2 = model.Image2,
                    Image1 = sysFileName1,
                    Image2=sysFileName2,
                    Type = model.Type,
                    CreateDate =  Convert.ToDateTime(model.CreateDate),
                    UpdateDate = Convert.ToDateTime(model.UpdateDate),
                    CreatedBy = model.CreatedBy,
                    UpdatedBy = model.UpdatedBy
                };
                db.tblPhotoGalleries.Add(savePhotoGal);
                db.SaveChanges();
                msg = "photogallery saved successfully";
                return msg;
            }
            else
            {
                getEditRecord.Id = model.Id;
                getEditRecord.Title = model.Title;
                //getEditRecord.Image1 = model.Image1; here image is taken .so this code is commented.
                //getEditRecord.Image2 = model.Image2;
                getEditRecord.Image1 = sysFileName1;
                getEditRecord.Image2 = sysFileName2;
                getEditRecord.Type = model.Type;
                getEditRecord.CreateDate = Convert.ToDateTime(model.CreateDate);
                getEditRecord.UpdateDate = Convert.ToDateTime(model.UpdateDate);
                getEditRecord.CreatedBy = model.CreatedBy;
                getEditRecord.UpdatedBy = model.UpdatedBy;
            }

            db.SaveChanges();
            msg = "row updated successfully";
            return msg;
           
        }

        public List<PhotoGalleryModel> GetPhotoGalleryList()
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            List<PhotoGalleryModel> lstPhotoGallery = new List<PhotoGalleryModel>();
            var getList = db.tblPhotoGalleries.ToList();

            if (getList != null)
            {
                foreach (var list in getList)
                {
                    lstPhotoGallery.Add(new PhotoGalleryModel()
                    {
                        Id=list.Id,
                        Title = list.Title,
                        Image1 = list.Image1,
                        Image2 = list.Image2,
                        Type = list.Type,
                        CreateDate = Convert.ToDateTime(list.CreateDate).ToShortDateString(),//(we are using string as we want date in this dd/MM/yyyy format)
                        UpdateDate = Convert.ToDateTime(list.UpdateDate).ToShortDateString(),
                        CreatedBy = list.CreatedBy,
                        UpdatedBy = list.UpdatedBy
                    });
                }
            }
            return lstPhotoGallery; // goes to controller
        }


        public string DeletePhotoGallery(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            string msg = "";
            var record = db.tblPhotoGalleries.Where(x => x.Id == id).FirstOrDefault();

            if(record != null)
            {
                db.tblPhotoGalleries.Remove(record);
            }

            db.SaveChanges();
            msg = "row deleted successfully";
            return msg;
        }


        public PhotoGalleryModel EditPhotGalleryRow(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();

            var EditRecord = db.tblPhotoGalleries.Where(x => x.Id == id).FirstOrDefault();
            PhotoGalleryModel photoGalleryModel = new PhotoGalleryModel();
            if(EditRecord !=null)
            {
                photoGalleryModel.Id = EditRecord.Id;
                photoGalleryModel.Title = EditRecord.Title;
                photoGalleryModel.Image1 = EditRecord.Image1;
                photoGalleryModel.Image2 = EditRecord.Image2;
                photoGalleryModel.Type = EditRecord.Type;
                photoGalleryModel.CreateDate =Convert.ToDateTime(EditRecord.CreateDate).ToShortDateString();
                photoGalleryModel.UpdateDate = Convert.ToDateTime(EditRecord.UpdateDate).ToShortDateString();
                photoGalleryModel.CreatedBy = EditRecord.CreatedBy;
                photoGalleryModel.UpdatedBy = EditRecord.UpdatedBy;

            }
            return photoGalleryModel;
        }

        public List<PhotoGalleryModel> GetPhotoGalleryListById(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            var getList = db.tblPhotoGalleries.Where(x => x.Id == id).ToList();
            List<PhotoGalleryModel> lstPhotoGalleries=new List<PhotoGalleryModel>();

            if(getList != null)
            {
                foreach(var list in getList)
                {
                    lstPhotoGalleries.Add(new PhotoGalleryModel()
                    {
                        Id=list.Id,
                        Title=list.Title,
                        Image1=list.Image1,
                        Image2=list.Image2,
                        Type=list.Type,
                        CreateDate=Convert.ToDateTime(list.CreateDate).ToShortDateString(),
                        UpdateDate=Convert.ToDateTime(list.UpdateDate).ToShortDateString(),
                        CreatedBy=list.CreatedBy,
                        UpdatedBy=list.UpdatedBy


                    });
                }
            }
            return lstPhotoGalleries;

        }
    }
}