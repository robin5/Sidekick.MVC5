// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: CreateTeamController.cs
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
using System.Diagnostics;

namespace PEClient.Controllers
{
    [Authorize(Roles = "Admin,Instructor")]
    public class TeamController : Controller
    {
        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (null == id)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View(new TeamIndexViewModel(User.Identity.GetUserId(), id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new TeamCreateViewModel(User.Identity.GetUserId()));
        }

        [HttpPost]
        public ActionResult Create(TeamCreateViewModel model)
        {
            // Test for model validation.
            if (!ModelState.IsValid)
            {
                // Note: The model does not automatically load the students list
                // so it must be done here before returning the model to the view
                model.LoadStudents(User.Identity.GetUserId());
                return View(model);
            }

            if (model.save(User.Identity.GetUserId()))
            {
                TempData.SuccessMessage($"Successfully added {model.TeamName} to peer groups.");
            }
            else
            {
                TempData.ErrorMessage($"Failed adding {model.TeamName} to peer groups: " + model.SaveErrorMessage);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (null == id)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View(new TeamEditViewModel(User.Identity.GetUserId(), (int)id));
        }

        [HttpPost]
        public ActionResult Edit(TeamEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.save(User.Identity.GetUserId()))
                {
                    TempData.SuccessMessage($"Successfully updated {model.TeamName}.");
                }
                else
                {
                    TempData.ErrorMessage($"Failed updating {model.TeamName}: " + model.SaveErrorMessage);
                }
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                // Note: The model does not automatically load the students list
                // so it must be done here before returning the model to the view
                model.LoadStudents(User.Identity.GetUserId());
                return View(model);
            }
        }
        
        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                var model = new TeamDeleteViewModel(User.Identity.GetUserId(), (int)id);

                if (model.Delete())
                {
                    TempData.SuccessMessage($"Successfully deleted {model.TeamName}.");
                }
                else
                {
                    TempData.ErrorMessage($"Failed deleting {model.TeamName}: " + model.ErrorMessage);
                }
                return View(model);
            }
        }
    }
}