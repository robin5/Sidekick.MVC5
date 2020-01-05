// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: DashboardController.cs
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

using System.Web.Mvc;
using PEClient.Models;

namespace PEClient.Controllers
{
    [Authorize(Roles = "Admin,Instructor")]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            var SurveyInstances = new EvaluationInstance[5];

            SurveyInstances[0] = new EvaluationInstance
            {
                Name = "Survey-1-1",
                Start = "2017-06-17 00:00:00",
                End = "2017-06-18 23:59:00",
                Status = "Completed"
            };
            SurveyInstances[1] = new EvaluationInstance
            {
                Name = "Survey-2-1",
                Start = "2017-06-17 00:00:00",
                End = "2017-06-18 23:59:00",
                Status = "Completed"
            };
            SurveyInstances[2] = new EvaluationInstance
            {
                Name = "No Questions 1",
                Start = "2017-06-19 13:00:00",
                End = "2017-06-26 13:00:00",
                Status = "Completed"
            };
            SurveyInstances[3] = new EvaluationInstance
            {
                Name = "CTEC-227 Spring 2017",
                Start = "2017-06-16 16:00:00",
                End = "2017-06-19 21:00:00",
                Status = "Completed"
            };
            SurveyInstances[4] = new EvaluationInstance
            {
                Name = "Final Test Launch 1",
                Start = "2017-06-19 09:00:00",
                End = "2017-06-20 13:00:00",
                Status = "Released"
            };

            //SurveyInstances = new EvaluationInstance[0];
            ViewBag.SurveyInstances = SurveyInstances;

            // =================================================

            var evaluationTemplates = new EvaluationTemplate[6];

            evaluationTemplates[0] = new EvaluationTemplate
            {
                Name = "Survey #1"
            };
            evaluationTemplates[1] = new EvaluationTemplate
            {
                Name = "Survey #2"
            };
            evaluationTemplates[2] = new EvaluationTemplate
            {
                Name = "No Questions"
            };
            evaluationTemplates[3] = new EvaluationTemplate
            {
                Name = "Survey #3"
            };
            evaluationTemplates[4] = new EvaluationTemplate
            {
                Name = "CTEC-227 Final Project (Fall 2019)"
            };
            evaluationTemplates[5] = new EvaluationTemplate
            {
                Name = "Survey-1"
            };

            //evaluationTemplates = new EvaluationTemplate[0];
            ViewBag.evaluationTemplates = evaluationTemplates;

            // =================================================

            var teams = new Team[5];

            teams[0] = new Team
            {
                Name = "CTEC-227 Team #1"
            };
            teams[1] = new Team
            {
                Name = "CTEC-227 Team #2"
            };
            teams[2] = new Team
            {
                Name = "CTEC-227 Team #3"
            };
            teams[3] = new Team
            {
                Name = "CTEC-227 Team #4"
            };
            teams[4] = new Team
            {
                Name = "Celebs"
            };

            //teams = new Team[0];
            ViewBag.teams = teams;

            return View();
        }
    }
}