using PEClient.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class ResponseViewModel
    {
        public List<spPeerSurveys_GetByAspNetId_Result> _surveys = new List<spPeerSurveys_GetByAspNetId_Result>();
        public ResponseViewModel(string aspNetId)
        {
            LoadData(aspNetId);
        }
        private void LoadData(string aspNetId)
        {
            using (var db = new PEClientContext())
            {
                // Query database for surveys for the given identity
                var surveys = db.spPeerSurveys_GetByAspNetId(aspNetId);

                // Cycle through result of database query and load data into the model
                foreach (var survey in surveys)
                {
                    _surveys.Add(survey);
                }
            }
        }
        public IList<spPeerSurveys_GetByAspNetId_Result> Surveys
        {
            get
            {
                return _surveys.AsReadOnly();
            }
        }
    }
}