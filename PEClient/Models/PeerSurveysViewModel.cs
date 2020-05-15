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
        public List<LaunchedSurvey> _launchedSurveys = new List<LaunchedSurvey>();
        public PeerSurveysViewModel(string aspNetId)
        {
            _launchedSurveys.Add(new LaunchedSurvey
            {
                Id = 0,
                Name = "Survey #1",
                Start = "12/25/2020 00:00",
                End = "12/26/2020 11:59",
                Status = "Ready",
            });
            _launchedSurveys.Add(new LaunchedSurvey
            {
                Id = 0,
                Name = "Survey #2",
                Start = "12/25/2021 00:00",
                End = "12/26/2021 11:59",
                Status = "Ready",
            });
            _launchedSurveys.Add(new LaunchedSurvey
            {
                Id = 0,
                Name = "Survey #3",
                Start = "12/25/2022 00:00",
                End = "12/26/2022 11:59",
                Status = "Ready",
            });
        }
        public List<LaunchedSurvey> LaunchedSurveys
        {
            get
            {
                return _launchedSurveys;
            }
        }
    }
}