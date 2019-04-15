﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Issues_Tracker.Controllers
{
    [Authorize(Roles = "Project Manager")]
    public class ProjectController : Controller
    {
        IssueTrackerEntities db = new IssueTrackerEntities();

        [HttpPost]
        public ActionResult CreateProject(string projectName)
        {
            try
            {
                if (!db.Projects.Select(p => p.Name).Contains(projectName))
                {
                    Project project = new Project();
                    project.Name = projectName;
                    db.Projects.Add(project);
                    db.SaveChanges();
                    ViewBag.failedMassage = string.Empty;
                }
                else
                {
                    ViewBag.failedMassage = "Project with such name already exist";
                }
            }
            catch (DbEntityValidationException)
            {

            }
            catch (EntityException)
            {

            }

            return RedirectToAction("Index", "Issue");
        }
    }
}