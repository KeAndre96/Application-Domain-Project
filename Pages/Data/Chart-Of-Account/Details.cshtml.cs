using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppDomainProject.Models;

namespace AppDomainProject
{
    public class DetailsModel : PageModel
    {
        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public DetailsModel(AppDomainProject.Models.AppDomainProjectContext context)
        {
            _context = context;
        }

        public AccountData AccountData { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AccountData = await _context.AccountData.FirstOrDefaultAsync(m => m.AccountNumber == id);

            if (AccountData == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
