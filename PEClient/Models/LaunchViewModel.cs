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

namespace PEClient.Models
{
    public class LaunchViewModel
    {
        private int? _userId;
        private List<string> _templateNames = new List<string>();
        private List<SelectListItem> _peerGroups = new List<SelectListItem>();
        private IEnumerable<SelectListItem> _selectedPeerGroups = new List<SelectListItem>();
        public string LaunchName { get; set; }
        public List<string> TemplateNames
        { 
            get { return _templateNames; }
        }
        public IEnumerable<SelectListItem> PeerGroups 
        { 
            get { return _peerGroups; } 
        }
        public IEnumerable<SelectListItem> SelectedPeerGroups 
        {
            get { return _selectedPeerGroups; }
            set { _selectedPeerGroups = value; } 
        }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public LaunchViewModel() { }
        public LaunchViewModel(int userId)
        {
            UserId = userId;
        }
        public int? UserId
        {
            get 
            { 
                return _userId; 
            }

            set
            {
                _userId = value;

                // Load table data associated with this user
                using (var db = new StudentReviewsEntities())
                {
                    // Enumerate templates
                    var surveys = from s in db.tblSurveys
                                  where s.OwnerId == _userId
                                  orderby s.SurveyId
                                  select s;

                    foreach (var survey in surveys)
                    {
                        _templateNames.Add(survey.Name);
                    }

                    // Enumerate teams
                    var teams = from t in db.tblTeams
                                where t.OwnerId == _userId
                                orderby t.TeamId
                                select t;

                    foreach (var team in teams)
                    {
                        _peerGroups.Add(new SelectListItem { Text = team.Name, Value = team.TeamId.ToString() });
                    }
                }
            }
        }
    }
}