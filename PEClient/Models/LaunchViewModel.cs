// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: LaunchViewModel.cs
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
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PEClient.Validation;

namespace PEClient.Models
{
    public class LaunchViewModel
    {
        private List<SelectListItem> _surveys = new List<SelectListItem>();
        private List<Team> _teams = new List<Team>();
        // for select list of all teams names
        private List<SelectListItem> _candidates = new List<SelectListItem>();
        // For hidden listbox containg all selected entries
        private List<SelectListItem> _liTeams = new List<SelectListItem>();

        // **************************************************************
        // *                        Constructors
        // **************************************************************

        public LaunchViewModel() { }
        public LaunchViewModel(string aspNetId)
        {
            LoadData(aspNetId);
        }

        // **************************************************************
        // *                        Properties
        // **************************************************************
        
        public string SaveErrorMessage { get; set; }

        //
        // Summary:
        //     Loads surveys and teams from the database into the model.
        public void LoadData(String aspNetId)
        {
            using (var db = new PEClientContext())
            {
                // Query database for surveys for the given identity
                var surveys = db.spSurvey_GetAll(aspNetId).OrderBy(s => s.Name);

                // Cycle through result of database query and load data into the model
                foreach (var survey in surveys)
                {
                    _surveys.Add(new SelectListItem { Text = survey.Name, Value = survey.SurveyId.ToString() });
                    //_candidates.Add(new SelectListItem { Text = student.Name + "  (" + student.UserName + ")", Value = (student.id).ToString() });
                }

                // Query database for teams owned by the given identity
                var teams = db.spTeam_GetAll(aspNetId);

                foreach (var team in teams)
                {
                    _teams.Add(new Team { Name = team.Name, Id = team.TeamId });
                    _candidates.Add(new SelectListItem { Text = team.Name, Value = team.TeamId.ToString() });
                    _liTeams.Add(new SelectListItem { Text = team.Name, Value = team.TeamId.ToString() });
                }
            }
        }
        public List<SelectListItem> liCandidates { get { return _candidates; } }
        public IEnumerable<int> SelectedCandidate { get; set; }
        public List<SelectListItem> liTeams { get { return _liTeams; } }
        public IEnumerable<Team> Teams { get { return _teams; } }

        [MinCount(1, ErrorMessage: "Please add one or more Peer Groups to send.")]
        public IEnumerable<int> SelectedTeams { get; set; }

        [NonNullEmptyOrWhiteSpace(ErrorMessage: "Please enter a Launch Name.")]
        public string LaunchName { get; set; }

        [NonNullEmptyOrWhiteSpace(ErrorMessage: "Please enter a start date and time.")]
        [DateTime]
        public string StartDateTime { get; set; }

        [NonNullEmptyOrWhiteSpace(ErrorMessage: "Please enter an end date and time.")]
        [DateTime]
        public string EndDateTime { get; set; }
        public IEnumerable<SelectListItem> Surveys
        { 
            get { return _surveys; }
        }
        
        [NonNullEmptyOrWhiteSpace(ErrorMessage: "Please select a Peer Evaluation Form")]
        public string Survey { get; set; }

        public IEnumerable<SelectListItem> PeerGroups 
        { 
            get { return _candidates; } 
        }

        public bool Save(string aspNetId)
        {
            SaveErrorMessage = "";

            try
            {
                int surveyId;
                DateTime startDate;
                DateTime endDate;

                if (Int32.TryParse(Survey, out surveyId) &&
                    DateTime.TryParse(StartDateTime, out startDate) &&
                    DateTime.TryParse(EndDateTime, out endDate))
                {
                    using (var db = new PEClientContext())
                    {
                        db.spLaunch_Create(aspNetId, LaunchName, surveyId, startDate, endDate, SelectedTeams);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                SaveErrorMessage = ex.Message;

                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    SaveErrorMessage += ("\n" + ex.InnerException.Message);
                    innerException = innerException.InnerException;
                }
                return false;
            }
        }
    }
}