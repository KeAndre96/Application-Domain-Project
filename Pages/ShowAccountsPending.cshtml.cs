using System;
using System.Collections.Generic;
using System.Linq;
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

            return Redirect("./ShowAccountsPending");
        }
        
        private bool UserInfoDataExists(string id)
        {
            return _context.UserInfoData.Any(e => e.ID == id);
        }
    }
}
