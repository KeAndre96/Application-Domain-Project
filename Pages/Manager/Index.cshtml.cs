using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject.Pages.Manager
{
    public class IndexModel : ManagerPageModel
    {

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Username { get; set; }


        public IndexModel(AppDomainProjectContext context) : base(context)
        {
        }

        public IActionResult OnGet()
        {
            PersonalInfoData info = PersonalInfo;
            if (info == null)
                return NotFound();

            Name = $"{info.FirstName} {info.LastName}";
            Username = info.ID;
            return Page();
        }
    }
}
