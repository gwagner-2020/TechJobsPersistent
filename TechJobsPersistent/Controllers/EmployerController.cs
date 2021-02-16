﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechJobsPersistent.Data;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsPersistent.Controllers
{
    public class EmployerController : Controller
    {
        private JobDbContext context;
        public EmployerController(JobDbContext context)
        {
            this.context = context;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Employer> employers = context.Employers.ToList();
            
            return View(employers);
        }

        public IActionResult Add()
        {
            AddEmployerViewModel viewModel = new AddEmployerViewModel();
            return View(viewModel);
        }

        //Handles employer entry and saves it to Employer database in context
        [HttpPost]
        [Route("Employer/Add")]
        public IActionResult ProcessAddEmployerForm(AddEmployerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Employer newEmployer = new Employer
                {
                    Name = viewModel.Name,
                    Location = viewModel.Location
                };

                context.Employers.Add(newEmployer);
                context.SaveChanges();

                return Redirect("Add");

            }
            
            return View("Add", viewModel);
        }

        public IActionResult About(int id)
        {
            Employer emp = context.Employers.Find(id);

            return View(emp);
        }
    }
}
