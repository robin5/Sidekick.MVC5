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
    public class PeerSurveysViewModel
    {
        public List<spPeerSurveys_GetByAspNetId_Result> _surveys = new List<spPeerSurveys_GetByAspNetId_Result>();
        public PeerSurveysViewModel(string aspNetId)
        {
            LoadData(aspNetId);

            //_launchedSurveys.Add(new LaunchedSurvey
            //{
            //    Id = 0,
            //    Name = "Survey #1",
            //    Start = "12/25/2020 00:00",
            //    End = "12/26/2020 11:59",
            //    Status = "Ready",
            //});
            //_launchedSurveys.Add(new LaunchedSurvey
            //{
            //    Id = 0,
            //    Name = "Survey #2",
            //    Start = "12/25/2021 00:00",
            //    End = "12/26/2021 11:59",
            //    Status = "Ready",
            //});
            //_launchedSurveys.Add(new LaunchedSurvey
            //{
            //    Id = 0,
            //    Name = "Survey #3",
            //    Start = "12/25/2022 00:00",
            //    End = "12/26/2022 11:59",
            //    Status = "Ready",
            //});
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