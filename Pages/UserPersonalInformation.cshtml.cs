using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppDomainProject
{
    public class UserPersonalInformationModel : PageModel
    {

        [Display(Name = "First Name")]
        [BindProperty]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [BindProperty]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [BindProperty]
        public string DOB { get; set; }

        [Display(Name = "Address")]
        [BindProperty]
        public string Address { get; set; }

        public void OnGet()
        {

        }
    }
}