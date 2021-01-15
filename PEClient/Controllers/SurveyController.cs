// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: SurveyController.cs
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
using Microsoft.AspNet.Identity;
using PEClient.DAL;
using System.Linq;
using System;

namespace PEClient.Controllers
{
    [Authorize(Roles = "Admin,Instructor")]
    public class SurveyController : Controller
    {
        private IRepository repository = new SQLRepository();

        [HttpGet]
        [Route("Survey/Index/{id:int}")]
        public ActionResult Index(int id)
        {
            try
            {
                var survey = repository.GetSurvey(User.Identity.GetUserId(), id);

                if (null != survey)
                {
                    return View(new SurveyIndexViewModel
                    {
                        Id = id,
                        Questions = survey.Questions,
                        Name = survey.Name,
                    });
                }
            }
            catch (Exception ex)
            {
                // TODO: Log the exception
            }
            TempData.ErrorMessage($"Survey not found");
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                return View(new SurveyCreateViewModel());
            }
            catch (Exception ex)
            {
                // TODO: Log the exception
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SurveyCreateViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

                var survey = new Survey
                {
                    Name = vm.Name,
                    Questions = vm.Questions
                };

                repository.AddSurvey(User.Identity.GetUserId(), survey);
                TempData.SuccessMessage($"Successfully added survey");
            }
            catch (Exception ex)
            {
                TempData.ErrorMessage($"Failed adding survey: " + ex.Message);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        [Route("Survey/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            try
            {
                var survey = repository.GetSurvey(User.Identity.GetUserId(), id);

                if (null != survey)
                {
                    return View(new SurveyEditViewModel
                    {
                        Id = id,
                        Name = survey.Name,
                        Questions = survey.Questions,
                    });
                }
            }
            catch (Exception ex)
            {
                // TODO: Log the exception
            }
            TempData.ErrorMessage($"Survey not found");
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [Route("Survey/Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SurveyEditViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

                var survey = new Survey
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    Questions = vm.Questions
                };

                repository.UpdateSurvey(User.Identity.GetUserId(), survey);
                TempData.SuccessMessage($"Successfully updated {vm.Name}.");
            }
            catch (Exception ex)
            {
                TempData.ErrorMessage($"Failed updating {vm.Name}: " + ex.Message);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        [Route("Survey/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            try {
                var survey = repository.DeleteSurvey(User.Identity.GetUserId(), id);
                if (null != survey)
                {
                    TempData.SuccessMessage($"Successfully deleted {survey.Name}.");
                    return View(new SurveyDeleteViewModel
                    {
                        Name = survey.Name
                    });
                }
            }
            catch (Exception ex)
            {
                // TODO: Log the exception
            }
            TempData.ErrorMessage($"Survey not found");
            return RedirectToAction("Index", "Dashboard");
        }
    }
}