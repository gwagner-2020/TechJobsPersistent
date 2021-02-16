﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        //Method pulls employers and skills from database to popluate dropdown and checkboxes        
        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Employer> employers = context.Employers.ToList();
            List<Skill> skills = context.Skills.ToList();
            
            AddJobViewModel viewModel = new AddJobViewModel(employers, skills);
            return View(viewModel);
        }

        //POST request that handles and validates new job; saves job properties to the Jobs database in context; saves ids to join table in JobSkill database in context; if not valid returns empty form (using similar code to the constructor in AddJobViewModel)
        [HttpPost]
        public IActionResult ProcessAddJobForm(AddJobViewModel viewModel, string[] selectedSkills)
        {

            if (ModelState.IsValid)
            {
               
                Job newJob = new Job
                {
                    Name = viewModel.Name,
                    EmployerId = viewModel.EmployerId
                };
                
                for (var i = 0; i < selectedSkills.Length; i++)
                {
                    int jobId = newJob.Id;

                    JobSkill jobSkill = new JobSkill
                    {
                        Job = newJob,
                        SkillId = int.Parse(selectedSkills[i])
                    };
                    context.JobSkills.Add(jobSkill);
                }

                context.Jobs.Add(newJob);
                context.SaveChanges();

                return Redirect("/home");

            }
            List<Employer> employers = context.Employers.ToList();
            List<Skill> skills = context.Skills.ToList();

            viewModel.Employers = new List<SelectListItem>();
            foreach (var employer in employers)
            {
                viewModel.Employers.Add(new SelectListItem
                {
                    Value = employer.Id.ToString(),
                    Text = employer.Name
                });
            }
            viewModel.Skills = skills;
            
            return View("AddJob", viewModel);
        }

        //uses lambda expressions to query the databases to display Job detail
        [HttpGet]
        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
