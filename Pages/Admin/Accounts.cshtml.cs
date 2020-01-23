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
    }
}
