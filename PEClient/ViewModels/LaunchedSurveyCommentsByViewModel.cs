using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class LaunchedSurveyCommentsByViewModel
    {
        public IEnumerable<CommentsByReviewer> CommentsByReviewer { get; set; }
    }
}