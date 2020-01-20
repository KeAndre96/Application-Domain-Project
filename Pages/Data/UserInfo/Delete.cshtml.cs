using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppDomainProject.Models;

namespace AppDomainProject.Pages.Data.UserInfo
{
    public class DeleteModel : PageModel
    {
        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public DeleteModel(AppDomainProject.Models.AppDomainProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserInfoData UserInfoData { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserInfoData = await _context.UserInfoData.FirstOrDefaultAsync(m => m.ID == id);

            if (UserInfoData == null)
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

            UserInfoData = await _context.UserInfoData.FindAsync(id);

            if (UserInfoData != null)
            {
                _context.UserInfoData.Remove(UserInfoData);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
