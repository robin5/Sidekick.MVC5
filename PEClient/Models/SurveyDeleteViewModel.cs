using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class SurveyDeleteViewModel
    {
        private string _aspNetId;
        private decimal _surveyId;
        public string SurveyName { get; set; }
        public string SaveErrorMessage { get; set; }
        public SurveyDeleteViewModel(string aspNetId, int surveyId)
        {
            this._aspNetId = aspNetId;
            this._surveyId = surveyId;
        }
        public bool Delete()
        {
            return true;
        }
    }
}