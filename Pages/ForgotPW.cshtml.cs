using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using AppDomainProject.Models;

namespace AppDomainProject.Pages.Shared
{
    public class ForgotPWModel : PageModel
    {
        private readonly ILogger<ForgotPWModel> _logger;
        private readonly AppDomainProjectContext _context;

        [Display(Name = "User ID")]
        [BindProperty]
        [DataType(DataType.Text)]
        public string Id { get; set; }

        [Display(Name = "Email")]
        [BindProperty]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "Security Question 1")]
        [BindProperty]
        [DataType(DataType.Text)]
        public string SecQuestion1 { get; set; }

        [Display(Name = "Security Question 2")]
        [BindProperty]
        [DataType(DataType.Text)]
        public string SecQuestion2 { get; set; }

        [Display(Name = "Security Question 3")]
        [BindProperty]
        [DataType(DataType.Text)]
        public string SecQuestion3 { get; set; }

        [Display(Name = "New Password")]
        [BindProperty]
        [DataType(DataType.Password)]
        public string newPass { get; set; }

        [Display(Name = "New Password Confirmation")]
        [BindProperty]
        [DataType(DataType.Password)]
        public string newPassConf { get; set; }

        //public Boolean next = false;
        
        public ForgotPWModel(ILogger<ForgotPWModel> logger, AppDomainProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Id = "";
            email = "";
            SecQuestion1 = "";
            SecQuestion2 = "";
            SecQuestion3 = "";
            //ConfirmSent = true;
            ModelState.Clear();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (await ValidateAsync())
            {
                return RedirectToPage("./Login/Index");
            }
            else
            {
                return RedirectToPage("./ForgotPW");
            }
        }

        private async Task<bool> ValidateAsync()
        {
            var query = from u in _context.LoginData select u;
            if (!string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(email))
            {
                query = query.Where(m => m.ID.Equals(Id));
            }
            var users = await query.ToListAsync();
            if (users.Count != 1)
                return false;
            email user = users[0];
            return user.Email.Equals(email);
        }

    }
}
