using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppDomainProject.Models;

namespace AppDomainProject.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly AppDomainProjectContext _context;

        public IndexModel(AppDomainProjectContext context)
        {
            _context = context;
        }

        public IList<LoginData> LoginData { get;set; }

        public async Task OnGetAsync()
        {
            LoginData = await _context.LoginData.ToListAsync();
        }
    }
}
