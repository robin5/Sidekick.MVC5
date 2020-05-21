using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class Response
    {
        public int SurveyId { get; set; }
        public string SurveyName { get; set; }
        public int SurveyQuestionId { get; set; }
        public int Index { get; set; }
        public string Question { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public Nullable<int> ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public int RevieweeId { get; set; }
        public string RevieweeName { get; set; }
        public string RevieweeUserName { get; set; }
        public Nullable<byte> GradeId { get; set; }
        public string Answer { get; set; }
    }
}