﻿// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: TemplateController.cs
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
using System.Web;
using System.Web.Mvc;
using PEClient.Models;

namespace PEClient.Controllers
{
    public class TemplateController : Controller
    {
        // GET: CreateTemplate
        public ActionResult Index()
        {
            return View(new TemplateViewModel());
        }

        [HttpPost]
        public ActionResult Index(TemplateViewModel model)
        {
            // Validate user input fields
            if (string.IsNullOrWhiteSpace(model.TemplateName))
            {
                ViewBag.ErrorMessage = "Invalid submission:  Please enter a name for the survey.";
            }
            else if ((model.Questions == null) || (model.Questions.Count == 0))
            {
                ViewBag.ErrorMessage = "Invalid submission:  A survey must have at least one question.";
            } 
            else
            {
                // Remove white space from beginning and end of each template question
                foreach (var question in model.Questions)
                {
                    if (string.IsNullOrWhiteSpace(question))
                    {
                        ViewBag.ErrorMessage = "Invalid submission:  A template question cannot be blank.";
                        break;
                    }
                }
            }

            // If any error message exist
            if (ViewBag.ErrorMessage != null)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}