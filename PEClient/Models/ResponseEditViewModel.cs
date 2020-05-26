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
            set;
        }
        public string SurveyName
        {
            get;
            set;
        }
        public int? TeamId
        {
            get;
            set;
        }
        public string TeamName
        {
            get;
            set;
        }
        public int? ReviewerId
        {
            get;
            set;
        }
        public string ReviewerName
        {
            get;
            set;
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
        public List<string> ResponseText
        {
            get;
            set;
        }
        public List<string> ResponseQuestionId
        {
            get;
            set;
        }
        public List<string> ResponseRevieweeId
        {
            get;
            set;
        }
        public List<string> GradeId
        {
            get;
            set;
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
        public bool save(string identity, bool submitFlag)
        {
            SaveErrorMessage = "";

            List<ResponseUpdate> responses = new List<ResponseUpdate>();
            try
            {
                int questionId;
                int reviewee;
                int reviewer;
                byte? gradeId;
                byte tmp;
                int teamId;

                if (ReviewerId == null)
                {
                    throw new Exception("Unknown user encountered");
                }

                for (int i = 0; i < ResponseQuestionId.Count; ++i)
                {
                    if (!int.TryParse(ResponseQuestionId[i], out questionId) &&
                        int.TryParse(ResponseRevieweeId[i], out reviewee) &&
                        int.TryParse(ResponseQuestionId[i], out teamId))
                    {
                        continue;
                    }

                    if (GradeId[i] == "")
                    {
                        gradeId = null;
                    }
                    else if (!byte.TryParse(GradeId[i], out tmp))
                    {
                        continue;
                    }
                    else if (tmp < 1 || tmp > 5)
                    {
                        continue;
                    }
                    else
                    {
                        gradeId = tmp;
                    }

                    if (int.TryParse(ResponseQuestionId[i], out questionId) &&
                        int.TryParse(ResponseRevieweeId[i], out reviewee) &&
                        int.TryParse(ResponseQuestionId[i], out teamId))
                    {
                        responses.Add(new ResponseUpdate
                        {
                            QuestionId = questionId,
                            Reviewee = reviewee,
                            Reviewer = (int)ReviewerId,
                            Text = ResponseText[i],
                            GradeId = gradeId,
                            TeamId = (int)TeamId
                        });
                    }
                }

                if (responses.Count > 0)
                {
                    using (var db = new PEClientContext())
                    {
                        db.spResponses_Update(identity, responses, submitFlag);
                    }
                }
                else
                {
                    throw new Exception("Invalid input encountered!");
                }

                return true;
            }
            catch (Exception ex)
            {
                SaveErrorMessage = ModelUtils.FormatExceptionMessage(ex);
                return false;
            }
        }
    }
}