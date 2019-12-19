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