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
    public class IndexModel : PageModel
    {
        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public IndexModel(AppDomainProject.Models.AppDomainProjectContext context)
        {
            _context = context;
        }

        public IList<PersonalInfoData> PersonalInfoData { get;set; }

        public async Task OnGetAsync()
        {
            PersonalInfoData = await _context.PersonalInfoData.ToListAsync();
        }
    }
}
