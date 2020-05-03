using PEClient.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class SurveyEditViewModel
    {
        private string _surveyName;
        List<spSurvey_GetById_Result> _questions = new List<spSurvey_GetById_Result>();
        List<string> _editedQuestions = new List<string>();
        public string SaveErrorMessage { get; set; }
        public int? Id { get; set; }
        public SurveyEditViewModel()
        {
        }
        public SurveyEditViewModel(string aspNetId, int? id)
        {
            this.Id = id;
            LoadQuestions(aspNetId, id);
        }
        private void LoadQuestions(string aspNetId, int? id)
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
                        _surveyName = question.Name;
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
        public bool save(string identity)
        {
            return false;
        }

        /*****************************************************************
         *                   Fields returned from the view
         *****************************************************************/

        [NonNullEmptyOrWhiteSpace(ErrorMessage: "A Survey's name cannot be blank.")]
        public string SurveyName
        {
            get { return _surveyName; }
            set
            {
                // Remove white space from beginning and end of the template's name
                _surveyName = value.Trim();
            }
        }

        [Required(ErrorMessage = "Questions missing.")]
        [MinCount(1, "Peer Evaluations must have at least one question")]
        [NonNullEmptyOrWhiteSpace(ErrorMessage: "A Survey question cannot be blank.")]
        public List<string> EditedQuestions
        {
            get
            {
                return _editedQuestions;
            }
            set
            {
                _editedQuestions = value;
                _questions = new List<spSurvey_GetById_Result>();

                // Remove white space from beginning and end of each template question
                for (int i = 0; i < _editedQuestions.Count; ++i)
                {
                    _editedQuestions[i] = _editedQuestions[i].Trim();
                    _questions.Add(new spSurvey_GetById_Result { Name = _surveyName, QsIndex = i, Text = _editedQuestions[i] });
                }
            }
        }
    }
}