using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class QuestionAnswer
    {
        public string Question { get; set; }
        public string Grade { get; set; }
        public string Answer { get; set; }
    }
    public class SurveyResponse
    {
        public SurveyResponse()
        {
            QuestionsAnswers = new List<QuestionAnswer>();
        }
        public string Peer { get; set; }
        public List<QuestionAnswer> QuestionsAnswers;
    }
    public class LaunchedSurveyCommentsByViewModel
    {
        public LaunchedSurveyCommentsByViewModel()
        {
            SurveyResponses = new List<SurveyResponse>();
            CreateFakeData();
        }
        private void CreateFakeData()
        {
            string q1 = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit.";
            string q2 = "Maecenas quis libero in risus maximus condimentum ut eu ligula.";

            QuestionAnswer p1qa1 = new QuestionAnswer { Question = q1, Grade = "A", Answer = "Answer 1" };
            QuestionAnswer p1qa2 = new QuestionAnswer { Question = q2, Grade = "B", Answer = "Answer 2" };

            QuestionAnswer p2qa1 = new QuestionAnswer { Question = q1, Grade = "C", Answer = "Answer 3" };
            QuestionAnswer p2qa2 = new QuestionAnswer { Question = q2, Grade = "D", Answer = "Answer 4" };

            QuestionAnswer p3qa1 = new QuestionAnswer { Question = q1, Grade = "F", Answer = "Answer 3" };
            QuestionAnswer p3qa2 = new QuestionAnswer { Question = q2, Grade = "", Answer = "Answer 4" };

            SurveyResponse p1 = new SurveyResponse { Peer = "Richard Lint" };
            p1.QuestionsAnswers.Add(p1qa1);
            p1.QuestionsAnswers.Add(p1qa2);

            SurveyResponse p2 = new SurveyResponse { Peer = "Patrick McCulley" };
            p2.QuestionsAnswers.Add(p2qa1);
            p2.QuestionsAnswers.Add(p2qa2);

            SurveyResponse p3 = new SurveyResponse { Peer = "Andrey Demchenko" };
            p3.QuestionsAnswers.Add(p3qa1);
            p3.QuestionsAnswers.Add(p3qa2);

            SurveyResponses.Add(p1);
            SurveyResponses.Add(p2);
            SurveyResponses.Add(p3);

            Student = "Richard Lint";
        }
        public string Student { get; private set; }
        public List<SurveyResponse> SurveyResponses { get; set; }
    }


}