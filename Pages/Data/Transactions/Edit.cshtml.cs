using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppDomainProject.Models;

namespace AppDomainProject.Pages.Data.Transactions
{
    public class EditModel : PageModel
    {
        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public EditModel(AppDomainProject.Models.AppDomainProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TransactionData TransactionData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TransactionData = await _context.TransactionData.FirstOrDefaultAsync(m => m.ID == id);

            if (TransactionData == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TransactionData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionDataExists(TransactionData.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TransactionDataExists(int id)
        {
            return _context.TransactionData.Any(e => e.ID == id);
        }
    }
}
