﻿// **********************************************************************************
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
    public class CreateTeamController : Controller
    {
        // GET: CreateTeam
        public ActionResult Index()
        {
            return View(new CreateTeamModel(User.Identity.GetUserId()));
        }

        // POST: CreateTeam
        [HttpPost]
        public ActionResult Index(CreateTeamModel model)
        {
            // The model needs the user's identity inorder to load students
            // and to save the data to the rightful owner
            model.UserId = User.Identity.GetUserId();

            // Test for model validation.
            if (!ModelState.IsValid)
            {
                // Note: The model does not automatically load the students list
                // so it must be done here before returning the model to the view
                model.LoadStudents();
                return View(model);
            }

            if (model.save())
            {
                TempData.SuccessMessage($"Successfully added {model.TeamName} to peer groups.");
            }
            else
            {
                TempData.ErrorMessage($"Failed adding {model.TeamName} to peer groups: " + model.SaveErrorMessage);
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}