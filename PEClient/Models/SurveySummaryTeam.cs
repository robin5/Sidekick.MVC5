using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class SurveySummaryTeam: Team
    {
        private List<SurveySummaryStudent> _students = new List<SurveySummaryStudent>();
        public SurveySummaryTeam(string name)
        {
            Name = name;
        }
        public List<SurveySummaryStudent> Students 
        { 
            get { return _students; } 
        }
    }
}