using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WagharalkarMVCProject.Data;

namespace WagharalkarMVCProject.Models
{
    public class StateModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }

        public List<StateModel> GetStatesList() 
        {
            WagharalKarDBEntities db = new WagharalKarDBEntities();
            List<StateModel> lstState = new List<StateModel>();
            var getStates = db.tblStates.ToList();
            if(getStates != null)
            {
                foreach(var list in getStates)
                {
                    lstState.Add(new StateModel()
                    {
                        StateId=list.StateId,
                        StateName=list.StateName
                    });
                }
            }
            return lstState;
        }
    }
}