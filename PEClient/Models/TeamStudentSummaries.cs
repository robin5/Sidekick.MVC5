using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class TeamStudentSummaries : Team
    {
        public List<StudentSummary> StudentSummaries { get; set; }
    }
}