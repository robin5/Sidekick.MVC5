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
using PEClient.DAL;
using System;
using System.Linq;

namespace PEClient.Controllers
{
    [Authorize(Roles = "Admin,Instructor")]
    public class TeamController : Controller
    {
        private readonly IRepository repository = new SQLRepository();

        [HttpGet]
        [Route("Team/Index/{id:int}")]
        public ActionResult Index(int id)
        {
            try
            {
                var team = repository.GetTeam(User.Identity.GetUserId(), id);

                if (null != team)
                {
                    return View(new TeamIndexViewModel
                    {
                        Id = id,
                        Name = team.Name,
                        Members = team.Members
                    });
                }
            }
            catch (Exception)
            {
                // TODO: Log the exception
            }
            TempData.ErrorMessage($"Team not found");
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                return View(new TeamCreateViewModel
                {
                    Students = repository.GetAllStudents(User.Identity.GetUserId())
                });
            }
            catch (Exception)
            {
                // TODO: Log the exception
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamCreateViewModel model)
        {
            try
            {
                // Test for model validation.
                if (!ModelState.IsValid)
                {
                    // Note: The model does not automatically load the students list
                    // so it must be done here before returning the model to the view
                    model.Students = repository.GetAllStudents(User.Identity.GetUserId());
                    return View(model);
                }

                if (repository.AddTeam(User.Identity.GetUserId(), model.TeamName, model.PeerSelection))
                {
                    TempData.SuccessMessage($"Successfully added {model.TeamName} to peer groups.");
                }
                else
                {
                    TempData.ErrorMessage($"Failed adding {model.TeamName} to peer groups");
                }
            }
            catch (Exception)
            {
                // TODO: Log the exception
                TempData.ErrorMessage($"Failed adding team");
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        [Route("Team/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            try
            {
                var aspNetId = User.Identity.GetUserId();
                var team = repository.GetTeam(aspNetId, id);
                var students = repository.GetAllStudents(aspNetId);

                if (null != team)
                {
                    return View(new TeamEditViewModel
                    {
                        Id = team.Id,
                        TeamName = team.Name,
                        PeerSelection = team.Members.Select(x => x.UserId).ToList(),
                        Students = students
                    });
                }
            }
            catch (Exception)
            {
                // TODO: Log the exception
            }
            TempData.ErrorMessage($"Team not found");
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost]
        [Route("Team/Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamEditViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    vm.Students = repository.GetAllStudents(User.Identity.GetUserId());
                    return View(vm);
                }

                if (repository.UpdateTeam(User.Identity.GetUserId(), vm.Id, vm.TeamName, vm.PeerSelection)){
                    TempData.SuccessMessage($"Successfully updated {vm.TeamName}.");
                }
                else
                {
                    TempData.ErrorMessage($"Failed updating {vm.TeamName}");
                }
            }
            catch (Exception)
            {
                // TODO: Log the exception
                TempData.ErrorMessage($"Failed updating {vm.TeamName}");
            }

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        [Route("Team/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var team = repository.DeleteTeam(User.Identity.GetUserId(), id);
                if (null != team)
                {
                    TempData.SuccessMessage($"Successfully deleted {team.Name}.");
                    return View(new TeamDeleteViewModel
                    {
                        TeamName = team.Name
                    });
                }
            }
            catch (Exception)
            {
                // TODO: Log the exception
            }
            TempData.ErrorMessage($"Team not found");
            return RedirectToAction("Index", "Dashboard");
        }
    }
}