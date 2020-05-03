using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class SurveyIndexViewModel
    {
        List<spSurvey_GetById_Result> _questions = new List<spSurvey_GetById_Result>();
        public int? Id { get; set; }
        public SurveyIndexViewModel()
        {
        }
        public SurveyIndexViewModel(string aspNetId, int? id)
        {
            this.Id = id;
            LoadQuestions(aspNetId, id);
        }

        public void LoadQuestions(string aspNetId, int? id)
        {
            if (null != id)
            {
                using (var db = new PEClientContext())
                {
                    // Query database for surveys for the given identity
                    var questions = db.spSurvey_GetById(aspNetId, id);

                    // Cycle through result of database query and load data into the model
                    foreach (var question in questions)
                    {
                        _questions.Add(question);
                    }
                }
            }
        }

        public List<spSurvey_GetById_Result> Questions
        {
            get
            {
                return _questions;
            }
        }
    }
}