using PEClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace PEClient.Controllers
{
    public class ViewSurveyController : Controller
    {
        // GET: ViewSurvey
        public ActionResult Index(int? id)
        {
            if (null == id)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View(new ViewSurveyViewModel(User.Identity.GetUserId(), id));
        }
    }
}