// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: PeerSurveysController.cs
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
using Microsoft.AspNet.Identity;
using PEClient.Models;
using System.Web.Mvc;

namespace PEClient.Controllers
{
    [Authorize(Roles = "Student")]
    public class ResponseController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View(new ResponseViewModel(User.Identity.GetUserId()));
        }
        // GET: Edit peer survey response
        [Route("Response/Edit/{surveyId}/{teamId}")]
        public ActionResult Edit(int surveyId, int teamId)
        {
            return View(new ResponseEditViewModel(User.Identity.GetUserId(), surveyId, teamId));
        }
        // POST: Edit peer survey response
        [HttpPost]
        public ActionResult Edit(ResponseEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.save(User.Identity.GetUserId()))
            {
                TempData.SuccessMessage($"Successfully updated {model.SurveyName}.");
            }
            else
            {
                TempData.ErrorMessage($"Failed updating {model.SurveyName}: " + model.SaveErrorMessage);
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}