﻿using System.ComponentModel.DataAnnotations;
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
        public string Id { get; set; }


        [Display(Name = "First Name")]
        [BindProperty]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [BindProperty]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [BindProperty]
        public string DOB { get; set; }

        [Display(Name = "Address")]
        [BindProperty]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public PersonalInfoData PersonalInfoData { get; set; }

        public async Task<IActionResult> OnPostSendAsync()
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

            _context.PersonalInfoData.Add(PersonalInfoData);
            await _context.SaveChangesAsync();
            return RedirectToPage();

        }
    }
}