using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AppDomainProject.Models;

namespace AppDomainProject.Pages.Admin
{
    public class View_EditModel : PageModel
    {

        private AppDomainProjectContext _context;

        public View_EditModel(AppDomainProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public bool EditMode { get; set; }

        [BindProperty]
        public PersonalInfoData Info { get; set; }

        public IActionResult OnGet(string id, bool? edit)
        {
            if (edit == null)
                EditMode = false;
            else
                EditMode = edit.Value;

            var query = from u in _context.PersonalInfoData select u;
            query = query.Where(m => m.ID.Equals(id));
            Info = query.FirstOrDefault();
            if (Info == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPostSave()
        {
            _context.Attach(Info).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Redirect($"./View_Edit?id={Info.ID}");
        }

        public IActionResult OnPostCancel()
        {
            return Redirect($"./View_Edit?id={Info.ID}");
        }

        public IActionResult OnPostEdit()
        {
            return Redirect($"./View_Edit?id={Info.ID}&edit=true");
        }
    }
}