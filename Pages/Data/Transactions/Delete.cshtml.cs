using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppDomainProject.Models;

namespace AppDomainProject.Pages.Data.Transactions
{
    public class DeleteModel : PageModel
    {
        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public DeleteModel(AppDomainProject.Models.AppDomainProjectContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TransactionData = await _context.TransactionData.FindAsync(id);

            if (TransactionData != null)
            {
                _context.TransactionData.Remove(TransactionData);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
