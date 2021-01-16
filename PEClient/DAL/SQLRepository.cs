// **********************************************************************************
// * Copyright (c) 2021 Robin Murray
// **********************************************************************************
// *
// * File: SQLRepository.cs
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
using PEClient.Models;

namespace PEClient.DAL
{
    public class SQLRepository : IRepository
    {
        public IEnumerable<Survey> GetAllSurveys(string identity)
        {
            List<Survey> _surveys = new List<Survey>();

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
            return _surveys;
        }
        public IEnumerable<Team> GetAllTeams(string identity)
        {
            List<Team> _teams = new List<Team>();

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
            return _teams;
        }
        public IEnumerable<LaunchedSurvey> GetAllLaunchedSurveys(string identity)
        {
            List<LaunchedSurvey> _launchedSurveys = new List<LaunchedSurvey>();

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
                        Start = launchedSurvey.StartDate,
                        End = launchedSurvey.EndDate,
                        Status = launchedSurvey.Released ? "Not Released" : "Released"
                    }); ;
                }
            }
            return _launchedSurveys;
        }
        public IEnumerable<Student> GetAllStudents(string identity)
        {
            List<Student> students = new List<Student>();

            using (var db = new PEClientContext())
            {
                // Get students from database
                var users = db.spAspNetUsers_GetAllStudents();

                // Cycle through result of database query and load data into the model
                foreach (var user in users)
                {
                    students.Add(new Student
                    {
                        UserName = user.UserName,
                        Name = user.FullName,
                        Id = user.UserId,
                    });
                }
            }

            return students;
        }
        public IEnumerable<StudentSummary> GetStudentSummaries(string identity, int id)
        {
            List<StudentSummary> studentSummaries = new List<StudentSummary>();

            using (var db = new PEClientContext())
            {
                // Query database for launched survey name and team members given survey ID
                var launchedSurveyTeams = db.spLaunchedSurveyTeams_GetById(identity, id).ToList();

                if (launchedSurveyTeams.Count() == 0)
                {
                    return null;
                }

                // Cycle through result of database query and load data into the model
                foreach (var launchedSurveyTeam in launchedSurveyTeams)
                {
                    // create a new student
                    studentSummaries.Add(new StudentSummary
                    {
                        UserName = launchedSurveyTeam.UserName,
                        Name = launchedSurveyTeam.FullName,
                        Id = launchedSurveyTeam.UserId,
                        SurveyId = launchedSurveyTeam.SurveyId,
                        SurveyName = launchedSurveyTeam.SurveyName,
                        TeamId = launchedSurveyTeam.TeamId,
                        TeamName = launchedSurveyTeam.TeamName,
                        Complete = 0.667f
                    });
                }
                return studentSummaries;
            }
        }
        public Survey AddSurvey(string identity, Survey survey)
        {
            try
            {
                using (var db = new PEClientContext())
                {
                    db.spSurvey_Create(identity, survey.Name, survey.Questions);
                }

                // TODO: Must set Survey.Id at some point before returning result

                return survey;
            }
            catch (Exception ex)
            {
                throw new Exception(ModelUtils.FormatExceptionMessage(ex));
            }
        }
        public Survey GetSurvey(string identity, int id)
        {
            Survey survey = null;

            using (var db = new PEClientContext())
            {
                var result = db.spSurvey_GetById(identity, id).ToList();

                if (result.Count() > 0)
                {
                    survey = new Survey
                    {
                        Id = id,
                        Name = result.First().Name,
                        Questions = result.Select(x => x.Text)
                    };
                }
            }

            return survey;
        }
        public Survey UpdateSurvey(string identity, Survey survey)
        {
            try
            {
                using (var db = new PEClientContext())
                {
                    db.spSurvey_Update(identity, survey.Id, survey.Name, survey.Questions);
                }

                return survey;
            }
            catch (Exception ex)
            {
                throw new Exception(ModelUtils.FormatExceptionMessage(ex));
            }
        }
        public Survey DeleteSurvey(string identity, int id)
        {
            Survey survey;
            try
            {
                if (null != (survey = GetSurvey(identity, id)))
                {
                    using (var db = new PEClientContext())
                    {
                        db.spSurvey_Delete(identity, id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ModelUtils.FormatExceptionMessage(ex));
            }
            return survey;
        }
        public bool AddTeam(string identity, string name, IEnumerable<int> members)
        {
            bool success = false;
            //Team team = null;

            try
            {
                using (var db = new PEClientContext())
                {
                    db.spTeam_Create(identity, name, members);
                }

                //team = GetTeam(id);

                // TODO: Must set Team.Id at some point before returning result

                success = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ModelUtils.FormatExceptionMessage(ex));
            }
            return success;
        }
        public Team GetTeam(string identity, int id)
        {
            Team team = null;

            using (var db = new PEClientContext())
            {
                var result = db.spTeam_GetById(identity, id).ToList();

                if (result.Count() > 0)
                {
                    team = new Team
                    {
                        Id = id,
                        Name = result.First().TeamName,
                        Members = result
                    };
                }
            }

            return team;
        }
        public bool UpdateTeam(string identity, int id, string name, IEnumerable<int> members)
        {
            bool success;
            try
            {
                using (var db = new PEClientContext())
                {
                    db.spTeam_Update(identity, id, name, members);
                }

                success = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ModelUtils.FormatExceptionMessage(ex));
            }
            return success;
        }
        public Team DeleteTeam(string identity, int id)
        {
            Team team;
            try
            {
                if (null != (team = GetTeam(identity, id)))
                {
                    using (var db = new PEClientContext())
                    {
                        db.spTeam_Delete(identity, id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ModelUtils.FormatExceptionMessage(ex));
            }
            return team;
        }
        public bool AddLaunchedSurvey(string identity, LaunchedSurvey launchedSurvey)
        {
            try
            {
                using (var db = new PEClientContext())
                {
                    db.spLaunch_Create(identity,
                        launchedSurvey.Name,
                        launchedSurvey.SurveyId,
                        launchedSurvey.Start,
                        launchedSurvey.End,
                        launchedSurvey.Teams
                        );
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ModelUtils.FormatExceptionMessage(ex));
            }
        }
    }
}