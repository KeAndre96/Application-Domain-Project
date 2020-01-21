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
    public class DeleteModel : PageModel
    {
        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public DeleteModel(AppDomainProject.Models.AppDomainProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PasswordData PasswordData { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PasswordData = await _context.LoginData.FirstOrDefaultAsync(m => m.ID == id);

            if (PasswordData == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PasswordData = await _context.LoginData.FindAsync(id);

            if (PasswordData != null)
            {
                _context.LoginData.Remove(PasswordData);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
