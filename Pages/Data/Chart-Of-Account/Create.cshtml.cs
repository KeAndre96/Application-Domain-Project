using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AppDomainProject.Models;

namespace AppDomainProject
{
    public class CreateModel : PageModel
    {
        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public CreateModel(AppDomainProject.Models.AppDomainProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AccountData AccountData { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AccountData.Add(AccountData);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
