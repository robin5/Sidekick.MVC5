// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: CreateTeamModel.cs
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
using System.Web.Mvc;
using PEClient.Validation;

namespace PEClient.Models
{
    public class CreateTeamModel
    {
        public string SaveErrorMessage { get; set; }
        private string _teamName;
        private List<decimal> _teamMemberIds = new List<decimal>();

        private List<Student> _students = new List<Student>();
        private List<SelectListItem> _candidates = new List<SelectListItem>();
        private List<SelectListItem> _peers = new List<SelectListItem>();
        private List<SelectListItem> _selectedStudents = new List<SelectListItem>();
        public CreateTeamModel()
        {
        }

        [NonNullEmptyOrWhiteSpace(ErrorMessage: "A team name cannot be blank.  Please enter a team name.")]
        public string TeamName
        {
            get { return _teamName; }
            set
            {
                // Remove white space from beginning and end of the template's name
                _teamName = value.Trim();
            }
        }
        public string UserId { get; set; }
        public CreateTeamModel(string userId)
        {
            UserId = userId;
            LoadStudents();
        }
        public void LoadStudents()
        {
            using (var db = new PEClientContext())
            {
                // Get students from database
                var students = db.spAspNetUsers_GetAllStudents();

                // Cycle through result of database query and load data into the model
                foreach (var student in students)
                {
                    _students.Add(new Student { UserName = student.UserName, Name = student.FullName, id = student.UserId });
                }
            }

            foreach (var student in _students)
            {
                _candidates.Add(new SelectListItem { Text = student.Name + "  (" + student.UserName + ")", Value = (student.id).ToString() });
                _peers.Add(new SelectListItem { Text = student.Name + "  (" + student.UserName + ")", Value = (student.id).ToString() });
            }
        }

        public List<SelectListItem> Candidates { get { return _candidates; } }
        public IEnumerable<int> CandidateSelection { get; set; }
        public List<SelectListItem> Peers { get { return _peers; } }

        [MinCount(1, ErrorMessage: "Please add one or more users to the peer group.")]
        public IEnumerable<int> PeerSelection { get; set; }
        public IEnumerable<Student> PeerDetails { get { return _students; } }

        public bool save()
        {
            SaveErrorMessage = "";

            try
            {
                using (var db = new PEClientContext())
                {
                    db.spTeam_Create(UserId, _teamName, PeerSelection);
                }

                return true;
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