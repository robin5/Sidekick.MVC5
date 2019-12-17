using PEClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PEClient.Controllers
{
    public class CreateTeamController : Controller
    {
        private List<Student> _students = new List<Student>();

        public CreateTeamController()
        {
            _students.Add(new Student { Name = "rlint", UserName = "Richard Lint", id = 36 });
            _students.Add(new Student { Name = "pmcculley", UserName = "Patrick McCulley", id = 37 });
            _students.Add(new Student { Name = "ademchenko", UserName = "Andrey Demchenko", id = 38 });
            _students.Add(new Student { Name = "robin", UserName = "Robin Murray", id = 39 });
            _students.Add(new Student { Name = "dking", UserName = "Dave King", id = 40 });
            _students.Add(new Student { Name = "ajaeger", UserName = "Amy Jaeger", id = 41 });
            _students.Add(new Student { Name = "belgort", UserName = "Bruce Elgort", id = 42 });
            _students.Add(new Student { Name = "drichards", UserName = "David Richards", id = 43 });
            _students.Add(new Student { Name = "wwoods", UserName = "Wayne Woods", id = 44 });
            _students.Add(new Student { Name = "yshapovalov", UserName = "Yevgen Shapovalov", id = 45 });
            _students.Add(new Student { Name = "jruff", UserName = "Jacob Ruff", id = 46 });
            _students.Add(new Student { Name = "cmcguire", UserName = "Chris McGuire", id = 47 });
            _students.Add(new Student { Name = "mlehr", UserName = "Matt Lehr", id = 48 });
            _students.Add(new Student { Name = "bsejouk", UserName = "Bilal Sejouk", id = 49 });
            _students.Add(new Student { Name = "jglenn", UserName = "John Glenn", id = 50 });
            _students.Add(new Student { Name = "alevine", UserName = "Adam Levine", id = 51 });
            _students.Add(new Student { Name = "jlopez", UserName = "Jennifer Lopez", id = 52 });
            _students.Add(new Student { Name = "spenn", UserName = "Sean Penn", id = 53 });
            _students.Add(new Student { Name = "reaton", UserName = "Richard Eaton", id = 54 });
            _students.Add(new Student { Name = "test_student1", UserName = "First1 Last1", id = 55 });
            _students.Add(new Student { Name = "abunker", UserName = "Archie Bunker", id = 56 });
        }

        public ActionResult Index()
        {
            ViewBag.Students = _students;
            ViewBag.Message = null;

            return View();
        }

        [HttpPost]
        public ActionResult Index(string team_name, string team_user_ids)
        {
            if (string.IsNullOrWhiteSpace(team_name))
            {
                ViewBag.Message = "Invalid submission!  Please enter a name for the team.";
            }
            else if (team_user_ids.Length == 0)
            {
                ViewBag.Message = "Invalid submission!  A team must have at least one member";
            }

            if (ViewBag.Message != null)
            {
                ViewBag.Students = _students;
                ViewBag.TeamName = team_name;
                ViewBag.TeamUserIds = team_user_ids;
                return View();
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}