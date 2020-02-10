using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppDomainProject
{
    public class ShowAccountsPendingModel : PageModel
    {
        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public ShowAccountsPendingModel(AppDomainProject.Models.AppDomainProjectContext context)
        {
            _context = context;
        }
        [BindProperty]
        public List<UserInfoData> UserInfoData { get; set; }
        
        public async Task OnGet()
        {
            UserInfoData = await _context.UserInfoData.ToListAsync();

        }
        
        public async Task<IActionResult> OnPostActivate(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var UserInfoData = await _context.UserInfoData.ToListAsync();
            var tempUser = UserInfoData.Find(m => m.ID.Equals(id));
            tempUser.Status = AccountStatus.Active;
            _context.Attach(tempUser).State = EntityState.Modified;
            //PasswordData pw = new PasswordData { ID = tempUser.ID };
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInfoDataExists(tempUser.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("appdomtest@gmail.com", "nmaykkgwhhssohju");
            smtp.EnableSsl = true;

            MailMessage msg = new MailMessage();
            msg.Subject = "Create Password";
            msg.Body = "https://localhost:44378/Data/Password/Create";
            string ToAddress = tempUser.Email;
            //string ToAddress = "Admin <appdomtest@gmail.com>";
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

            return Redirect("./ShowAccountsPending");
        }
        
        public async Task<IActionResult> OnPostDelete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var UserInfoData = await _context.UserInfoData.ToListAsync();
            var tempUser = UserInfoData.Find(m => m.ID.Equals(id));
            

            if (tempUser != null)
            {
                _context.UserInfoData.Remove(tempUser);
                await _context.SaveChangesAsync();
            }

            var query = (from m in _context.PersonalInfoData select m).Where(n => n.ID.Equals(id));
            var personalinfo = (await query.ToListAsync()).FirstOrDefault();

            if(personalinfo != null)
            {
                _context.PersonalInfoData.Remove(personalinfo);
                await _context.SaveChangesAsync();
            }

            var query2 = (from m in _context.LoginData select m).Where(n => n.ID.Equals(id));
            var logininfo = (await query2.ToListAsync()).FirstOrDefault();

            if(logininfo != null)
            {
                _context.LoginData.Remove(logininfo);
                await _context.SaveChangesAsync();
            }

            return Redirect("./ShowAccountsPending");
        }
        
        private bool UserInfoDataExists(string id)
        {
            return _context.UserInfoData.Any(e => e.ID == id);
        }
    }
}
