// **********************************************************************************
// * Copyright (c) 2021 Robin Murray
// **********************************************************************************
// *
// * File: IRepository.cs
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

using System.Collections.Generic;
using PEClient.Models;

namespace PEClient.DAL
{
    public interface IRepository
    {
        IEnumerable<Survey> GetAllSurveys(string identity);
        IEnumerable<Team> GetAllTeams(string identity);
        IEnumerable<LaunchedSurvey> GetAllLaunchedSurveys(string identity);
        IEnumerable<Student> GetAllStudents(string identity);
        IEnumerable<CommentsByReviewer> GetCommentsByReviewer(string identity, int launchedSurveyId, int teamId, int reviewerId);
        IEnumerable<CommentsAboutReviewee> GetCommentsAboutReviewee(string identity, int launchedSurveyId, int teamId, int revieweeId);
        Survey AddSurvey(string identity, Survey survey);
        Survey GetSurvey(string identity, int id);
        Survey UpdateSurvey(string identity, Survey survey);
        Survey DeleteSurvey(string identity, int id);
        bool AddTeam(string identity, string name, IEnumerable<int> members);
        Team GetTeam(string identity, int id);
        bool UpdateTeam(string identity, int id, string name, IEnumerable<int> members);
        Team DeleteTeam(string identity, int id);
        bool AddLaunchedSurvey(string identity, LaunchedSurvey launchedSurvey);
        IEnumerable<StudentSummary> GetStudentSummaries(string identity, int surveyId);
    }
}