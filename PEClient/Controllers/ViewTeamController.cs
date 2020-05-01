using PEClient.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace PEClient.Controllers
{
    public class ViewTeamController : Controller
    {
        // GET: ViewTeam
        public ActionResult Index(int? id)
        {
            return View(new ViewTeamViewModel(User.Identity.GetUserId(), id));
        }
    }
}