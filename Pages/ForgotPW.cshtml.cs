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
    public class ForgotPWModel : PageModel
    {
        //Primary constructor and local variables
        //private readonly ILogger<ForgotPWModel> _logger;
        private readonly AppDomainProjectContext _context;
        private string tempID;
        private string NewPassword;

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

        public void OnGet(int? pageState)
        {
            Id = "";
            Email = "";
            SecQuestion1 = "";
            SecQuestion2 = "";
            SecQuestion3 = "";
            ModelState.Clear();
            if (pageState != null)
            {
                PageState = pageState.Value;
            }
        }


        public ActionResult OnPostIdEmailCancel()
        {
            return Redirect("./Index");
        }
        public ActionResult OnPostIdEmailNext()
        {
            //TODO Insert email+Id auth here
            //then proceed to two lines below 
            tempID = Id;
            PageState++;

            if (String.IsNullOrEmpty(tempID)){
                return Redirect("./ForgotPW?pageState=" + 0);
            }
            else{
                return Redirect("./ForgotPW?pageState=" + PageState);
            }
            
        }

        public ActionResult OnPostSecQuestionsCancel()
        {
            PageState = 0;
            return Redirect("./Index");
        }
        public ActionResult OnPostSecQuestionsBack()
        {
            PageState = 0;
            return Redirect("./ForgotPW?pageState=" + PageState);
        }

        public ActionResult OnPostSecQuestionsNext()
        {
            //TODO Insert sec questions auth here
            //then proceed to two lines below
            PageState = 2;
            return Redirect("./ForgotPW?pageState=" + PageState);
        }

        public ActionResult OnPostFinishNewPWCancel()
        {
            PageState = 0;
            return Redirect("./Index");
        }

        /*public ActionResult OnPostFinishNewPW(string ID)
        {
            var query = from u in _context.PasswordData select u
            PasswordData pd = new PasswordData { ID = tempID, Password = NewPassword };
            _context.Attach(pd).State= EntityState.Modified;
            _context.SaveChanges();
            return Redirect("./Index");
        }
        */
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
