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

namespace PEClient.Controllers
{
    [Authorize(Roles = "Admin,Instructor")]
    public class SurveyController : Controller
    {
        // GET: Displays a survey with the "Index" view
        public ActionResult Index(int? id)
        {
            if (null == id)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View(new SurveyIndexViewModel(User.Identity.GetUserId(), id));
        }
        // GET: Create a Survey with the "Create" view
        public ActionResult Create()
        {
            return View(new SurveyCreateViewModel());
        }
        // POST: Create a Survey with the "Create" view
        [HttpPost]
        public ActionResult Create(SurveyCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.save(User.Identity.GetUserId()))
            {
                TempData.SuccessMessage($"Successfully added {model.SurveyName} to peer evaluation templates.");
            }
            else
            {
                TempData.ErrorMessage($"Failed adding {model.SurveyName} to peer evaluation templates: " + model.SaveErrorMessage);
            }

            return RedirectToAction("Index", "Dashboard");
        }
        // GET: Create a Survey with the "Edit" view
        public ActionResult Edit(int? id)
        {
            if (null == id)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View(new SurveyEditViewModel(User.Identity.GetUserId(), (int) id));
        }
        // POST: Create a Survey with the "Edit" view
        [HttpPost]
        public ActionResult Edit(SurveyEditViewModel model)
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