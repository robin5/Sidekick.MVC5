using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class SurveyTeam: Team
    {
        private List<SurveySummaryStudent> _users = new List<SurveySummaryStudent>();
        public SurveyTeam(string name, decimal id)
        {
            this.Name = name;
            this.Id = id;
        }
        public List<SurveySummaryStudent> Users 
        { 
            get { return _users; } 
        }
    }
}