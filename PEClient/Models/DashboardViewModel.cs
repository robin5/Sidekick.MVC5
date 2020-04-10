// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: DashboardViewModel.cs
// *
// * Description: View model for the Template controller and view
// *
// * Author: Robin Murray
// *
// **********************************************************************************
// *
// * Granting License: The MIT License (MIT)
// * 
// *   Permission is hereby granted, free of charge, to any person obtaining a copy
// *   of this software and associated documentation files (the "Software"), to deal
// *   in the Software without restriction, including without limitation the rights
// *   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// *   copies of the Software, and to permit persons to whom the Software is
// *   furnished to do so, subject to the following conditions:
// *   The above copyright notice and this permission notice shall be included in
// *   all copies or substantial portions of the Software.
// *   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// *   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// *   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// *   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// *   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// *   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// *   THE SOFTWARE.
// * 
// **********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class DashboardViewModel
    {
        public List<EvaluationTemplate> _templates = new List<EvaluationTemplate>();
        public List<Team> _teams = new List<Team>();
        public List<EvaluationInstance> _instances = new List<EvaluationInstance>();
        private void LoadTemplates(string identity)
        {
            using (var db = new PEClientContext())
            {
                var templates = from s in db.tblSurveys
                                join u in db.AspNetUsers
                                    on s.OwnerId equals u.UserId
                                where u.Id == identity
                                orderby s.SurveyId
                                select new { Name = s.Name };

                foreach (var template in templates)
                {
                    _templates.Add(new EvaluationTemplate { Name = template.Name });
                }
            }
        }
        private void LoadTeams(string identity)
        {
            using (var db = new PEClientContext())
            {
                // Join tblTeams to AspNetUser and query for teams owned by this user user
                var teams =
                    from t in db.tblTeams join u in db.AspNetUsers
                        on t.OwnerId equals u.UserId
                    where u.Id == identity
                    orderby t.TeamId
                    select new { Name = t.Name};

                foreach (var team in teams)
                {
                    _teams.Add(new Team { Name = team.Name });
                }
            }
        }
        private void LoadSurveyInstances(string identity)
        {
            using (var db = new PEClientContext())
            {
                var surveyInstances = 
                    from si in db.tblSurveyInstances join s in db.tblSurveys 
                        on si.InstanceId equals s.InstanceId
                    where s.OwnerId == 1000
                    orderby s.InstanceId
                    select new { StartDate = si.StartDate, EndDate = si.EndDate, Released = si.Released, Name = s.Name};

                foreach (var surveyInstance in surveyInstances)
                {
                    _instances.Add(new EvaluationInstance
                    {
                        Name = surveyInstance.Name,
                        Start = surveyInstance.StartDate.ToString(),
                        End = surveyInstance.EndDate.ToString(),
                        Status = surveyInstance.Released == 0 ? "Not Released" : "Released"
                    }) ;
                }
            }
        }
        public DashboardViewModel(string identity)
        {
            LoadTemplates(identity);
            LoadTeams(identity);
            LoadSurveyInstances(identity);
        }
        public List<EvaluationTemplate> Templates
        {
            get
            {
                return _templates;
            }
        }
        public List<Team> Teams
        {
            get
            {
                return _teams;
            }
        }
        public List<EvaluationInstance> SurveyInstances
        {
            get
            {
                return _instances;
            }
        }
    }
}