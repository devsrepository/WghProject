using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WagharalkarMVCProject.Data;

namespace WagharalkarMVCProject.Models
{
    public class CityModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public Nullable<int> StateId { get; set; }

        public List<CityModel> GetCityList(int StateId) //changes 14/02/2024
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            List<CityModel> lstCity = new List<CityModel>();
            var getCities = db.tblCities.Where(x => x.StateId == StateId).ToList(); //changes 14/02/2024
            if (getCities != null)
            {
                foreach(var list in getCities)
                {
                    lstCity.Add(new CityModel()
                    {
                        CityId = list.CityId,
                        CityName = list.CityName,
                        StateId = list.StateId
                    });
                }
            }
            return lstCity;
        }
    }
}