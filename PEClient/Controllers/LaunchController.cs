using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PEClient.Models;

namespace PEClient.Controllers
{
    public class LaunchController : Controller
    {
        // GET: Launch
        public ActionResult Index()
        {
            return View(new LaunchViewModel());
        }

        // POST: Launch
        [HttpPost]
        public ActionResult Index(LaunchViewModel request)
        {
            // Validate user input fields
            if (string.IsNullOrWhiteSpace(request.LaunchName))
            {
                ViewBag.ErrorMessage = "Invalid submission:  Please enter a name for the launch.";
            }
            else if (request.StartDateTime == null)
            {
                ViewBag.ErrorMessage = "Invalid submission:  Please enter the start date and time of the launch.";
            }
            else if (request.EndDateTime == null)
            {
                ViewBag.ErrorMessage = "Invalid submission:  Please enter the end date and time of the launch.";
            }

            // If any error message exist
            if (ViewBag.ErrorMessage != null)
            {
                return View(request);
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}