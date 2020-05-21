using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class ResponseEditViewModel
    {
        public int? SurveyId
        {
            get;
            private set;
        }
        public string SurveyName
        {
            get;
            private set;
        }
        public int? TeamId
        {
            get;
            private set;
        }
        public string TeamName
        {
            get;
            private set;
        }
        public int? ReviewerId
        {
            get;
            private set;
        }
        public string ReviewerName
        {
            get;
            private set;
        }
        public List<Response> Responses
        {
            get;
            private set;
        }
        public string SaveErrorMessage
        {
            get;
            private set;
        }
        public ResponseEditViewModel()
        {
        }
        public ResponseEditViewModel(string aspNetId, int surveyId, int teamId)
        {
            LoadData(aspNetId, surveyId, teamId);
        }
        private void LoadData(string aspNetId, int surveyId, int teamId)
        {
            using (var db = new PEClientContext())
            {
                this.Responses = new List<Response>();

                foreach (var response in db.spResponses_GetBySurveyIdTeamId(aspNetId, surveyId, teamId))
                {
                    if (SurveyId == null) { SurveyId = response.SurveyId; }
                    if (SurveyName == null) { SurveyName = response.SurveyName; }
                    if (TeamId == null) { TeamId = response.TeamId; }
                    if (TeamName == null) { TeamName = response.TeamName; }
                    if (ReviewerId == null) { ReviewerId = response.ReviewerId; }
                    if (ReviewerName == null) { ReviewerName = response.ReviewerName; }

                    this.Responses.Add(new Response
                    {
                        SurveyId = response.SurveyId,
                        SurveyName = response.SurveyName,
                        SurveyQuestionId = response.SurveyQuestionId,
                        Index = response.Index,
                        Question = response.Question,
                        TeamId = response.TeamId,
                        TeamName = response.TeamName,
                        ReviewerId = response.ReviewerId,
                        ReviewerName = response.ReviewerName,
                        RevieweeId = response.RevieweeId,
                        RevieweeName = response.RevieweeName,
                        RevieweeUserName = response.RevieweeUserName,
                        GradeId = response.GradeId,
                        Answer = response.Answer
                    });
                }
            }
        }
        public bool save(string identity)
        {
            SaveErrorMessage = "";

            //try
            //{
            //    using (var db = new PEClientContext())
            //    {
            //        db.spSurvey_Update(identity, Id, _surveyName, _editedQuestions);
            //    }

            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    SaveErrorMessage = ModelUtils.FormatExceptionMessage(ex);
            //    return false;
            //}
            return false;
        }
    }
}