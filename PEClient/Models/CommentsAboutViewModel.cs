using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class PeerAnswer
    {
        public string Peer { get; set; }
        public string Grade { get; set; }
        public string Answer { get; set; }
    }
    public class PeerQuestionAnswers
    {
        public PeerQuestionAnswers()
        {
            Answers = new List<PeerAnswer>();
        }
        public string Question 
        { 
            get;
            set;
        }
        public List<PeerAnswer> Answers 
        { 
            get;
            set;
        }
    }
    public class CommentsAboutViewModel
    {
        public CommentsAboutViewModel()
        {
            PeerQuestionAnswers = new List<PeerQuestionAnswers>();
            CreateFakeData();
        }
        public void CreateFakeData()
        {
            string q1 = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit.";
            string q2 = "Maecenas quis libero in risus maximus condimentum ut eu ligula.";

            PeerAnswer p1q1 = new PeerAnswer { Peer = "Richard Lint", Grade = "A", Answer = "Answer 1" };
            PeerAnswer p2q1 = new PeerAnswer { Peer = "Patrick McCulley", Grade = "B", Answer = "Answer 2" };
            PeerAnswer p3q1 = new PeerAnswer { Peer = "Andrey Demchenko", Grade = "C", Answer = "Answer 3" };

            PeerAnswer p1q2 = new PeerAnswer { Peer = "Richard lint", Grade = "A", Answer = "Answer 4" };
            PeerAnswer p2q2 = new PeerAnswer { Peer = "Patrick McCulley", Grade = "B", Answer = "Answer 5" };
            PeerAnswer p3q2 = new PeerAnswer { Peer = "Andrey Demchenko", Grade = "C", Answer = "Answer 6" };

            PeerQuestionAnswers pqa1 = new PeerQuestionAnswers();
            pqa1.Question = q1;
            pqa1.Answers.Add(p1q1);
            pqa1.Answers.Add(p2q1);
            pqa1.Answers.Add(p3q1);

            PeerQuestionAnswers pqa2 = new PeerQuestionAnswers();
            pqa2.Question = q2;
            pqa2.Answers.Add(p1q2);
            pqa2.Answers.Add(p2q2);
            pqa2.Answers.Add(p3q2);

            this.PeerQuestionAnswers.Add(pqa1);
            this.PeerQuestionAnswers.Add(pqa2);
        }
        public List<PeerQuestionAnswers> PeerQuestionAnswers 
        { 
            get;
            private set;
        }
    }
}