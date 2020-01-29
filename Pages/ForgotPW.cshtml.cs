using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using AppDomainProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AppDomainProject.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly ILogger<ForgotPasswordModel> _logger;
        private readonly AppDomainProjectContext _context;
        [BindProperty]
        public int PageState { get; set; } = 0;
        

        // Set all the input fields to display the listed text and set types if required
        [Display(Name = "User ID:")]
        [BindProperty]
        public string Id { get; set; }

        [Display(Name = "Email:")]
        [BindProperty]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "New Password:")]
        [BindProperty]
        [DataType(DataType.Password)]
        public string NewPass { get; set; }

        [Display(Name = "Confirm New Password:")]
        [BindProperty]
        [DataType(DataType.Password)]
        public string NewPassConfirm { get; set; }

        [Display(Name = "Security Question 1")]
        [BindProperty]
        public string SecQuestion1 { get; set; }

        [Display(Name = "Security Question 2")]
        [BindProperty]
        public string SecQuestion2 { get; set; }

        [Display(Name = "Security Question 3")]
        [BindProperty]
        public string SecQuestion3 { get; set; }

        public ForgotPasswordModel(ILogger<ForgotPasswordModel> logger, AppDomainProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        
        public void OnGet(int? pageState)
        {
            Id = "";
            Email = "";
            SecQuestion1 = "";
            SecQuestion2 = "";
            SecQuestion3 = "";
            ModelState.Clear();
            if(pageState != null)
            {
                PageState = pageState.Value;
            }
        }



        public ActionResult OnPostIdEmailNext()
        {
            PageState++;
            return Redirect("./ForgotPW?pageState="+PageState);
        }

        public ActionResult OnPostSecQuestionsNext()
        {
            PageState++;
            return Redirect("./ForgotPW?pageState=" + PageState);
        }


        /*
          public async Task<IActionResult> OnPost()
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
             var query = from u in _context.UserInfoData select u;
             if (!string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(Email))
             {
                 query = query.Where(m => m.ID.Equals(Id));
             }
             var users = await query.ToListAsync();
             if (users.Count != 1)
                 return false;
             UserInfoData user = users[0];
             return user.Email.Equals(Email);
         }
         */
    }
}
