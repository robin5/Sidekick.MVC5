using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class ResultsViewModel
    {
        private List<Team> _teams = new List<Team>();
        private decimal _surveyId = -1;
        private string _surveyName = String.Empty;
        public string SurveyName { get { return _surveyName; } }
        public decimal SurveyId { get { return _surveyId; } }
        public IEnumerable<Team> Teams { get { return _teams; } }
        public ResultsViewModel(string aspNetId, int surveyId)
        {
            using (var db = new PEClientContext())
            {
                // Query database for surveys for the given identity
                var teams = db.spLaunchedSurveyTeams_GetById(aspNetId, surveyId);

                SurveyTeam team = null;

                // Cycle through result of database query and load data into the model
                foreach (var oTeam in teams)
                {
                    // create a new student
                    var user = new SurveySummaryStudent { 
                        UserName = oTeam.UserName, 
                        Name = oTeam.FullName, 
                        id = oTeam.UserId, 
                        Complete = 0.667f };

                    // The survey's name and ID will be the same for 
                    //  all students so set it's value once
                    if (_surveyName == String.Empty)
                    {
                        _surveyName = oTeam.SurveyName;
                        _surveyId = oTeam.SurveyId;
                    }

                    // Add a new team each time the team's name changes
                    if (null == team || team.Name != oTeam.TeamName)
                    {
                        team = new SurveyTeam(oTeam.TeamName, oTeam.TeamId);
                        _teams.Add(team);
                    }
                    // Add the student to the current team
                    team.Users.Add(user);
                }
            }
        }
        public string PieData()
        {
            return "[['A',2],['B',3],['Not Available',2]]";
        }
    }
}