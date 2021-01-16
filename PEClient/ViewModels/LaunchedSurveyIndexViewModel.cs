// **********************************************************************************
// * Copyright (c) 2021 Robin Murray
// **********************************************************************************
// *
// * File: LaunchedSurveyIndexViewModel.cs
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
    public class LaunchedSurveyIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TeamStudentSummaries> Teams { get; set; } = new List<TeamStudentSummaries>();
        public LaunchedSurveyIndexViewModel(IEnumerable<StudentSummary> summaries)
        {
            // Obtain survey name and ID
            var first = summaries.First();
            Name = first.SurveyName;
            Id = first.SurveyId;

            TeamStudentSummaries team = null;

            // Cycle through result of database query and load data into the model
            foreach (var studentSummary in summaries)
            {
                // Add a new team each time the team's name changes
                if ((null == team) || (team.Id != studentSummary.TeamId))
                {
                    team = new TeamStudentSummaries
                    {
                        Name = studentSummary.TeamName,
                        Id = studentSummary.TeamId,
                        StudentSummaries = new List<StudentSummary>()
                    };
                    Teams.Add(team);
                }
                // Add the student to the current team
                team.StudentSummaries.Add(studentSummary);
            }
        }
    }
}