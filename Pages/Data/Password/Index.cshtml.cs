using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppDomainProject.Models;

namespace AppDomainProject.Pages.Data.Password
{
    public class IndexModel : PageModel
    {
        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public IndexModel(AppDomainProject.Models.AppDomainProjectContext context)
        {
            _context = context;
        }

        public IList<PasswordData> PasswordData { get;set; }

        public async Task OnGetAsync()
        {
            PasswordData = await _context.LoginData.ToListAsync();
        }
    }
}
