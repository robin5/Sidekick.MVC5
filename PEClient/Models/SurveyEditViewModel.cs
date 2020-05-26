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
        List<spSurvey_GetById_Result> _savedQuestions = new List<spSurvey_GetById_Result>();
        List<string> _questions = new List<string>();
        public string SaveErrorMessage { get; set; }
        public int Id { get; set; }
        public SurveyEditViewModel()
        {
        }
        public SurveyEditViewModel(string aspNetId, int id)
        {
            this.Id = id;
            LoadQuestions(aspNetId, id);
        }
        private void LoadQuestions(string aspNetId, int id)
        {
            using (var db = new PEClientContext())
            {
                // Query database for surveys for the given identity
                var questions = db.spSurvey_GetById(aspNetId, id);

                // Cycle through result of database query and load data into the model
                foreach (var question in questions)
                {
                    _surveyName = question.Name;
                    _savedQuestions.Add(question);
                }
            }
        }
        public List<spSurvey_GetById_Result> SavedQuestions
        {
            get
            {
                return _savedQuestions;
            }
        }
        public bool save(string identity)
        {
            SaveErrorMessage = "";

            try
            {
                using (var db = new PEClientContext())
                {
                    db.spSurvey_Update(identity, Id, _surveyName, _questions);
                }

                return true;
            }
            catch (Exception ex)
            {
                SaveErrorMessage = ModelUtils.FormatExceptionMessage(ex);
                return false;
            }
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
        public List<string> Questions
        {
            get
            {
                return _questions;
            }
            set
            {
                _questions = value;
                _savedQuestions = new List<spSurvey_GetById_Result>();

                // Remove white space from beginning and end of each template question
                for (int i = 0; i < _questions.Count; ++i)
                {
                    _questions[i] = _questions[i].Trim();
                    _savedQuestions.Add(new spSurvey_GetById_Result { Name = _surveyName, QsIndex = i, Text = _questions[i] });
                }
            }
        }
    }
}