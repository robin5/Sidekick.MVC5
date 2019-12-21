// **********************************************************************************
// * Copyright (c) 2019 Robin Murray
// **********************************************************************************
// *
// * File: CreateTemplateController.cs
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

namespace PEClient.Controllers
{
    public class CreateTemplateController : Controller
    {
        // GET: CreateTemplate
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string template_name, List<string> template_questions)
        {
            // Validate user input fields
            if (string.IsNullOrWhiteSpace(template_name))
            {
                ViewBag.ErrorMessage = "Invalid submission:  Please enter a name for the template.";
            }
            else if ((template_questions == null) || (template_questions.Count == 0))
            {
                ViewBag.ErrorMessage = "Invalid submission:  A template must have at least one question.";
            } 
            else
            {
                // Remove white space from beginning and end of the template's name
                template_name = template_name.Trim();

                // Remove white space from beginning and end of each template question
                for (int i = 0; i < template_questions.Count; ++i)
                {
                    template_questions[i] = template_questions[i].Trim();
                    if (string.IsNullOrWhiteSpace(template_questions[i]))
                    {
                        ViewBag.ErrorMessage = "Invalid submission:  A template question cannot be blank.";
                    }
                }
            }

            // If any error message exist
            if (ViewBag.ErrorMessage != null)
            {
                ViewBag.TemplateName = template_name;
                ViewBag.TemplateQuestions = template_questions;
                return View();
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}