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
        public List<Survey> _surveys = new List<Survey>();
        public List<Team> _teams = new List<Team>();
        public List<LaunchedSurvey> _launchedSurveys = new List<LaunchedSurvey>();
        public DashboardViewModel(string aspNetId)
        {
            LoadSurveys(aspNetId);
            LoadTeams(aspNetId);
            LoadLaunchedSurveys(aspNetId);
        }
        //
        // Summary:
        //     Loads surveys from the database into the model.
        private void LoadSurveys(string identity)
        {
            using (var db = new PEClientContext())
            {
                // Query database for surveys for the given identity
                var surveys = db.spSurvey_GetAll(identity);

                // Cycle through result of database query and load data into the model
                foreach (var survey in surveys)
                {
                    _surveys.Add(new Survey { Name = survey.Name, Id = survey.SurveyId });
                }
            }
        }
        //
        // Summary:
        //     Loads teams from the database into the model.
        private void LoadTeams(string identity)
        {
            using (var db = new PEClientContext())
            {
                // Query database for teams owned by the given identity
                var teams = db.spTeam_GetAll(identity);

                // Cycle through result of database query and load data into the model
                foreach (var team in teams)
                {
                    _teams.Add(new Team { Name = team.Name, Id = team.TeamId });
                }
            }
        }
        //
        // Summary:
        //     Loads launched surveys from the database into the model.
        private void LoadLaunchedSurveys(string identity)
        {
            using (var db = new PEClientContext())
            {
                // Query database for launched surveys owned by the given identity
                var launchedSurveys = db.spLaunchedSurveys_GetAll(identity);

                // Cycle through result of database query and load data into the model
                foreach (var launchedSurvey in launchedSurveys)
                {
                    _launchedSurveys.Add(new LaunchedSurvey
                    {
                        Id = launchedSurvey.SurveyId,
                        Name = launchedSurvey.Name,
                        Start = launchedSurvey.StartDate.ToString(),
                        End = launchedSurvey.EndDate.ToString(),
                        Status = launchedSurvey.Released == 0 ? "Not Released" : "Released"
                    }); ;
                }
            }
        }
        public List<Survey> Surveys
        {
            get
            {
                return _surveys;
            }
        }
        public List<Team> Teams
        {
            get
            {
                return _teams;
            }
        }
        public List<LaunchedSurvey> LaunchedSurveys
        {
            get
            {
                return _launchedSurveys;
            }
        }
    }
}