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
using System.Net;

namespace AppDomainProject.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        [Display(Name="security question 1:")]
        [BindProperty]

        public string Question1 { get; set; }

        [Display(Name = "security question 2:")]
        [BindProperty]

        public string Question2 { get; set; }

        [Display(Name = "security question 3:")]
        [BindProperty]

        public string Question3 { get; set; }

        [Display(Name = "Email:")]
        [BindProperty]

        public string Email { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostSendAsync()
        {
            
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("appdomtest@gmail.com", "nmaykkgwhhssohju");
            smtp.EnableSsl = true;

            MailMessage msg = new MailMessage();
            msg.Subject = "New Password";
            msg.Body = "Dumb Slut";
            string ToAddress = Email;
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

            

            return RedirectToPage();

        }


    }
}
