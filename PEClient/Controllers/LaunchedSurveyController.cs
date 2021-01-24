// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: LaunchedSurveyController.cs
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
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PEClient.DAL;
using PEClient.Models;

namespace PEClient.Controllers
{
    [Authorize(Roles = "Admin,Instructor")]
    public class LaunchedSurveyController : Controller
    {
        private readonly IRepository repository = new SQLRepository();

        [HttpGet]
        [Route("LaunchedSurvey/Index/{id:int}")]
        public ActionResult Index(int id)
        {
            try
            {
                var studentSummaries = repository.GetStudentSummaries(User.Identity.GetUserId(), id);
                if (null != studentSummaries)
                {
                    return View(new LaunchedSurveyIndexViewModel(studentSummaries));
                }
            }
            catch (Exception)
            {
                // TODO: Log the exception
            }
            TempData.ErrorMessage($"Launched survey not found");
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                string identity = User.Identity.GetUserId();

                return View(new LaunchedSurveyCreateViewModel
                {
                    Surveys = repository.GetAllSurveys(identity),
                    Teams = repository.GetAllTeams(identity)
                });
            }
            catch (Exception)
            {
                // TODO: Log the exception
                return RedirectToAction("Index", "Dashboard");
            }
        }

        [HttpPost]
        public ActionResult Create(LaunchedSurveyCreateViewModel vm)
        {
            try
            {
                string identity = User.Identity.GetUserId();

                if (!ModelState.IsValid)
                {
                    vm.Surveys = repository.GetAllSurveys(identity);
                    vm.Teams = repository.GetAllTeams(identity);
                    return View(vm);
                }

                var launchedSurvey = new LaunchedSurvey
                {
                    Name = vm.LaunchName,
                    SurveyId = vm.SurveyId,
                    Start = DateTime.Parse(vm.StartDateTime),
                    End = DateTime.Parse(vm.EndDateTime),
                    Teams = vm.SelectedTeams
                };
                repository.AddLaunchedSurvey(identity, launchedSurvey);
                TempData.SuccessMessage($"Successfully launched survey");
            }
            catch (Exception)
            {
                // TODO: Log the exception
                TempData.ErrorMessage($"Failed launching survey");
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        [Route("LaunchedSurvey/CommentsAbout/{surveyId}/{teamId}/{revieweeId}")]
        public ActionResult CommentsAbout(int surveyId, int teamId, int revieweeId)
        {
            return View(new LaunchedSurveyCommentsAboutViewModel
            {
                CommentsAboutReviewee = repository.GetCommentsAboutReviewee(User.Identity.GetUserId(), surveyId, teamId, revieweeId)
            });
        }

        [HttpGet]
        [Route("LaunchedSurvey/CommentsBy/{surveyId}/{teamId}/{reviewerId}")]
        public ActionResult CommentsBy(int surveyId, int teamId, int reviewerId)
        {
            return View(new LaunchedSurveyCommentsByViewModel
            {
                CommentsByReviewer = repository.GetCommentsByReviewer(User.Identity.GetUserId(), surveyId, teamId, reviewerId)
            });
        }
    }
}