using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AppDomainProject.Pages.Admin
{
    
    public class IndexModel : AdminPageModel
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
