using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using AppDomainProject.Models;

namespace AppDomainProject
{
    public class UserPersonalInformationModel : PageModel
    {

        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public UserPersonalInformationModel(AppDomainProject.Models.AppDomainProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [Display(Name = "ID")]
        [BindProperty]
        //[Required]
        public string Id { get; set; }


        [Display(Name = "First Name")]
        [BindProperty]
        //[Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [BindProperty]
        //[Required]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime DOB { get; set; }

        [Display(Name = "Address")]
        [BindProperty]
        //[Required]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [BindProperty]
        //[Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [BindProperty]
        //[Required]
        public string Password { get; set; }

        
        public PersonalInfoData PersonalInfoData { get; set; }

        public UserInfoData UserInfoData {get; set;}

        //public PasswordData PasswordData { get; set; }
        public async Task<IActionResult> OnPostSendAsync()
        {
            if(ModelState.IsValid)
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("appdomtest@gmail.com", "nmaykkgwhhssohju");
                smtp.EnableSsl = true;

                MailMessage msg = new MailMessage();
                msg.Subject = "New User Registration";
                msg.Body = "https://localhost:44378/ShowAccountsPending";
                //string ToAddress = Email;
                string ToAddress = "Admin <appdomtest@gmail.com>";
                msg.To.Add(ToAddress);
                string FromAddress = " Admin <appdomtest@gmail.com>";
                msg.From = new MailAddress(FromAddress);

                try
                {
                    smtp.Send(msg);
                }
                catch
                {
                    throw;
                }

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                PersonalInfoData pi = new PersonalInfoData { ID = FirstName[0] + LastName  + DOB.ToString("MM") + DOB.ToString("yy"), FirstName = FirstName, LastName = LastName, DOB = DOB, Address = Address };
                _context.PersonalInfoData.Add(pi);

                UserInfoData temp = new UserInfoData { ID = pi.ID, Status = AccountStatus.Pending, Email = Email, Class = AccountType.User, PasswordSetDate = DateTime.Today, PasswordExpirationDate = (DateTime.Today.AddDays(180)) };
                _context.UserInfoData.Add(temp);

                PasswordData pd = new PasswordData { ID = pi.ID, Password = Password};
                _context.LoginData.Add(pd);


                await _context.SaveChangesAsync();
                return RedirectToPage();

            }
            return Page();
        }
            
    }
}