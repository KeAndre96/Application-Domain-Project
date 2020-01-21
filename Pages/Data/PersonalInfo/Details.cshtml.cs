using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppDomainProject.Models;

namespace AppDomainProject.Pages.Data.PersonalInfo
{
    public class DetailsModel : PageModel
    {
        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public DetailsModel(AppDomainProject.Models.AppDomainProjectContext context)
        {
            _context = context;
        }

        public PersonalInfoData PersonalInfoData { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PersonalInfoData = await _context.PersonalInfoData.FirstOrDefaultAsync(m => m.ID == id);

            if (PersonalInfoData == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
