using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using WagharalkarMVCProject.Data;

namespace WagharalkarMVCProject.Models
{
    public class VideoGalleryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string YouTubeUrl { get; set; }
        public string Type { get; set; }
        public string CreateDate { get; set; } //Nullable<System.DateTime> to string
        public string UpdateDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }

        public string saveVideoGallery(VideoGalleryModel model)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            string msg = "";
            var getRecord = db.tblVideoGalleries.Where(x => x.Id == model.Id).FirstOrDefault();

            if(getRecord==null)
            {
                var saveVideoGallery = new tblVideoGallery()
                {
                    Id=model.Id,
                    Title = model.Title,
                    YouTubeUrl = model.YouTubeUrl,
                    Type = model.Type,
                    CreateDate = Convert.ToDateTime(model.CreateDate),
                    UpdateDate = Convert.ToDateTime(model.UpdateDate),
                    CreatedBy = model.CreatedBy,
                    UpdatedBy = model.UpdatedBy

                };

                db.tblVideoGalleries.Add(saveVideoGallery);
                db.SaveChanges();
                msg = "videos are saved successfully";
                return msg;
            }
            else
            {
                getRecord.Id = model.Id;
                getRecord.Title = model.Title;
                getRecord.YouTubeUrl = model.YouTubeUrl;
                getRecord.Type = model.Type;
                getRecord.CreateDate = Convert.ToDateTime(model.CreateDate);
                getRecord.UpdateDate = Convert.ToDateTime(model.UpdateDate);
                getRecord.CreatedBy = model.CreatedBy;
                getRecord.UpdatedBy = model.UpdatedBy;
            }

            db.SaveChanges();
            msg = "row updated successfully";
            return msg;
        }


        public List<VideoGalleryModel> GetVideoGalleryList()
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            List<VideoGalleryModel> lstVideoGallery = new List<VideoGalleryModel>();
            var getList = db.tblVideoGalleries.ToList();

            if (getList != null)
            {
                foreach (var list in getList)
                {
                    lstVideoGallery.Add(new VideoGalleryModel()
                    {
                        Id=list.Id,
                        Title = list.Title,
                        YouTubeUrl=list.YouTubeUrl,
                        Type = list.Type,
                        CreateDate = Convert.ToDateTime(list.CreateDate).ToShortDateString(),
                        UpdateDate = Convert.ToDateTime(list.UpdateDate).ToShortDateString(),
                        CreatedBy = list.CreatedBy,
                        UpdatedBy = list.UpdatedBy
                    });
                }
            }
            return lstVideoGallery; // goes to controller
        }

        public string DeleteVideoGalleryRow(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            string msg = "";
            var record = db.tblVideoGalleries.Where(x => x.Id == id).FirstOrDefault();
            if(record !=null)
            {
                db.tblVideoGalleries.Remove(record);
            }
            db.SaveChanges();
            msg = "record deleted successfully";
            return msg;
        }


        public VideoGalleryModel EditVideoGalleryRow(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            var getRecord = db.tblVideoGalleries.Where(x => x.Id == id).FirstOrDefault();
            VideoGalleryModel videoGalleryModel = new VideoGalleryModel();

            if(getRecord !=null)
            {
                videoGalleryModel.Id = getRecord.Id;
                videoGalleryModel.Title = getRecord.Title;
                videoGalleryModel.YouTubeUrl = getRecord.YouTubeUrl;
                videoGalleryModel.Type = getRecord.Type;
                videoGalleryModel.CreateDate = Convert.ToDateTime(getRecord.CreateDate).ToShortDateString();
                videoGalleryModel.UpdateDate = Convert.ToDateTime(getRecord.UpdateDate).ToShortDateString();
                videoGalleryModel.CreatedBy=getRecord.CreatedBy;
                videoGalleryModel.UpdatedBy = getRecord.UpdatedBy;
            }

            return videoGalleryModel;
        }

        public List<VideoGalleryModel> GetVideoGalleryListById(int id)
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            List<VideoGalleryModel> lstVideoGallery = new List<VideoGalleryModel>();
            var getList = db.tblVideoGalleries.Where(x=>x.Id==id).ToList();

            if (getList != null)
            {
                foreach (var list in getList)
                {
                    lstVideoGallery.Add(new VideoGalleryModel()
                    {
                        Id = list.Id,
                        Title = list.Title,
                        YouTubeUrl = list.YouTubeUrl,
                        Type = list.Type,
                        CreateDate = Convert.ToDateTime(list.CreateDate).ToShortDateString(),
                        UpdateDate = Convert.ToDateTime(list.UpdateDate).ToShortDateString(),
                        CreatedBy = list.CreatedBy,
                        UpdatedBy = list.UpdatedBy
                    });
                }
            }
            return lstVideoGallery; // goes to controller
        }
    }
}