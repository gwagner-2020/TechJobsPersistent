using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        [Required (ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Employer is required")]
        public int EmployerId {get; set;}

        public List<SelectListItem> Employers { get; set; }

        public int SkillId { get; set; }
        public List<Skill> Skills { get; set; }

        public AddJobViewModel() { }

        public AddJobViewModel(List<Employer> possibleEmployers, List<Skill> possibleSkills)
        {
            Employers = new List<SelectListItem>();
            Skills = new List<Skill>();

            foreach (var employer in possibleEmployers)
            {
                Employers.Add(new SelectListItem
                {
                    Value = employer.Id.ToString(),
                    Text = employer.Name
                }); 
            }

            Skills = possibleSkills;

            //foreach (var skill in possibleSkills)
            //{
            //    Skills.Add(new SelectListItem
            //    {
            //        Value = skill.Id.ToString(),
            //        Text = skill.Name
            //    });
            //}


        }
    }
}
