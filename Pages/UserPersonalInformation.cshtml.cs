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

        public string Email { get; set; }


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



            return RedirectToPage();

        }
        public void OnGet()
        {
            
        }
    }
}