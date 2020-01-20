using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppDomainProject.Models;

namespace AppDomainProject.Pages.Data.Login
{
    public class DetailsModel : PageModel
    {
        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public DetailsModel(AppDomainProject.Models.AppDomainProjectContext context)
        {
            _context = context;
        }

        public LoginData LoginData { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LoginData = await _context.LoginData.FirstOrDefaultAsync(m => m.ID == id);

            if (LoginData == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
