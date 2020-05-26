using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class ResponseUpdate
    {
        public int QuestionId { get; set; }
        public int Reviewee { get; set; }
        public int Reviewer { get; set; }
        public string Text { get; set; }
        public byte? GradeId { get; set; }
        public int TeamId { get; set; }
    }
}