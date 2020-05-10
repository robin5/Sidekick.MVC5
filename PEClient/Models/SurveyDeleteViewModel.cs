using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class SurveyDeleteViewModel
    {
        public decimal SurveyId { get; set; }
        public string SurveyName { get; set; }
        public string SaveErrorMessage { get; set; }
        public SurveyDeleteViewModel(int surveyId)
        {
            this.SurveyId = surveyId;
        }
        public bool Delete(string aspNetId)
        {
            return true;
        }
    }
}