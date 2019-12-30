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
        private int? _userId = null;
        private List<Student> _students = new List<Student>();
        private List<SelectListItem> _candidates = new List<SelectListItem>();
        private List<SelectListItem> _peers = new List<SelectListItem>();
        private List<SelectListItem> _selectedStudents = new List<SelectListItem>();
        public CreateTeamModel() { }
        public CreateTeamModel(int userId)
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
                if (null == (_userId = value))
                {
                    _candidates = new List<SelectListItem>();
                    _peers = new List<SelectListItem>();
                }
                else // Load students from database
                {
                    _students = new List<Student>();
                    _students.Add(new Student { UserName = "rlint", Name = "Richard Lint", id = 36 });
                    _students.Add(new Student { UserName = "pmcculley", Name = "Patrick McCulley", id = 37 });
                    _students.Add(new Student { UserName = "ademchenko", Name = "Andrey Demchenko", id = 38 });
                    _students.Add(new Student { UserName = "robin", Name = "Robin Murray", id = 39 });
                    _students.Add(new Student { UserName = "dking", Name = "Dave King", id = 40 });
                    _students.Add(new Student { UserName = "ajaeger", Name = "Amy Jaeger", id = 41 });
                    _students.Add(new Student { UserName = "belgort", Name = "Bruce Elgort", id = 42 });
                    _students.Add(new Student { UserName = "drichards", Name = "David Richards", id = 43 });
                    _students.Add(new Student { UserName = "wwoods", Name = "Wayne Woods", id = 44 });
                    _students.Add(new Student { UserName = "yshapovalov", Name = "Yevgen Shapovalov", id = 45 });
                    _students.Add(new Student { UserName = "jruff", Name = "Jacob Ruff", id = 46 });
                    _students.Add(new Student { UserName = "cmcguire", Name = "Chris McGuire", id = 47 });
                    _students.Add(new Student { UserName = "mlehr", Name = "Matt Lehr", id = 48 });
                    _students.Add(new Student { UserName = "bsejouk", Name = "Bilal Sejouk", id = 49 });
                    _students.Add(new Student { UserName = "jglenn", Name = "John Glenn", id = 50 });
                    _students.Add(new Student { UserName = "alevine", Name = "Adam Levine", id = 51 });
                    _students.Add(new Student { UserName = "jlopez", Name = "Jennifer Lopez", id = 52 });
                    _students.Add(new Student { UserName = "spenn", Name = "Sean Penn", id = 53 });
                    _students.Add(new Student { UserName = "reaton", Name = "Richard Eaton", id = 54 });
                    _students.Add(new Student { UserName = "test_student1", Name = "First1 Last1", id = 55 });
                    _students.Add(new Student { UserName = "abunker", Name = "Archie Bunker", id = 56 });

                    foreach (var student in _students)
                    {
                        _candidates.Add(new SelectListItem { Text = student.Name + "  (" + student.UserName + ")", Value = (student.id).ToString() });
                        _peers.Add(new SelectListItem { Text = student.Name + "  (" + student.UserName + ")", Value = (student.id).ToString() });
                    }
                }
            }
        }

        [NonNullEmptyOrWhiteSpace(ErrorMessage: "Please enter a team name.")]
        public string TeamName { get; set; }
        public List<SelectListItem> Candidates { get { return _candidates; } }
        public IEnumerable<int> CandidateSelection { get; set; }
        public List<SelectListItem> Peers { get { return _peers; } }

        [MinCount(1, ErrorMessage: "Please add one or more users to the peer group.")]
        public IEnumerable<int> PeerSelection { get; set; }
        public IEnumerable<Student> PeerDetails { get { return _students; } }
    }
}