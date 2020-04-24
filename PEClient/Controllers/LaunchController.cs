// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: LaunchController.cs
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
    public class LaunchController : Controller
    {
        // GET: Launch
        public ActionResult Index()
        {
            return View(new LaunchViewModel(User.Identity.GetUserId()));
        }

        // POST: Launch
        [HttpPost]
        public ActionResult Index(LaunchViewModel model)
        {
            // The model needs the user's identity inorder to load students
            // and to save the data to the rightful owner
            model.UserId = User.Identity.GetUserId();

            // Test for model validation.
            if (!ModelState.IsValid)
            {
                model.LoadData();
                return View(model);
            }

            if (model.Save())
            {
                TempData.SuccessMessage($"Successfully launched {model.LaunchName}.");
            }
            else
            {
                TempData.ErrorMessage($"Failed to launch {model.LaunchName}. " + model.SaveErrorMessage);
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}