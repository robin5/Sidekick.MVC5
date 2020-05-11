using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class TeamDeleteViewModel
    {
        private string _aspNetId;
        private decimal _teamId;
        public string TeamName { get; set; }
        public string ErrorMessage { get; set; }
        public TeamDeleteViewModel(string aspNetId, int surveyId)
        {
            this._aspNetId = aspNetId;
            this._teamId = surveyId;
        }
        public bool Delete()
        {
            ErrorMessage = "";

            try
            {
                using (var db = new PEClientContext())
                {
                    db.spTeam_Delete(_aspNetId, _teamId);
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ModelUtils.FormatExceptionMessage(ex);
                return false;
            }
        }
    }
}