using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject.Pages.Admin
{
    public class AccountsModel : PageModel
    {
        private AppDomainProjectContext _context;

        public AccountsModel(AppDomainProjectContext context)
        {
            _context = context;
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

        public int GetToggleStatus(UserInfoData u)
        {
            return u.Status == AccountStatus.Active ? (int)AccountStatus.Inactive : (int)AccountStatus.Active;
        }

        public string GetUpdateText(UserInfoData u)
        {
            return u.Status == AccountStatus.Active ? "Deactivate" : "Activate";
        }
    }
}
