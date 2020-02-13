using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject.Pages.Admin
{
    public class UsersModel : AdminPageModel
    {
        

        public UsersModel(AppDomainProjectContext context) : base (context)
        {

        }

        [BindProperty]
        public List<UserInfoData> Users { get; set; }

        public void OnGet()
        {
            var query = from u in _context.UserInfoData select u;
            
            Users = query.ToList();
        }

        public IActionResult OnPostAcctToggle(string id, int status)
        {
            var query = from u in _context.UserInfoData select u;
            query = query.Where(m => m.ID.Equals(id));
            UserInfoData data = query.FirstOrDefault();
            if (data != null)
            { 
                data.Status = (AccountStatus)status;
                _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }

            return Redirect("./Accounts");
        }

        public IActionResult OnPostAcctDetails(string id)
        {
            return Redirect($"./View_Edit?id={id}");
        }

        //PAGE HELPERS

        public int GetToggleStatus(UserInfoData u)
        {
            return u.Status == AccountStatus.Active ? (int)AccountStatus.Inactive : (int)AccountStatus.Active;
        }

        public string GetUpdateText(UserInfoData u)
        {
            return u.Status == AccountStatus.Active ? "Deactivate" : "Activate";
        }


        //Email items
        [Display(Name = "Email:")]
        [BindProperty]

        public string Email { get; set; }

        [Display(Name = "Subject:")]
        [BindProperty]

        public string Subject { get; set; }

        [Display(Name = "Body:")]
        [BindProperty]

        public string Body { get; set; }

        public IActionResult OnPostSend()
        {
            Send_Message(Body, Subject, Email);


            return RedirectToPage();
        }

            public void Send_Message(string message, string subject, string to)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("appdomtest@gmail.com", "nmaykkgwhhssohju");
            smtp.EnableSsl = true;

            MailMessage msg = new MailMessage();
            msg.Subject = message;
            msg.Body = subject;
            string ToAddress = to;
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
        }



    }
}
