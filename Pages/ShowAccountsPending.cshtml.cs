using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject
{
    public class ShowAccountsPendingModel : PageModel
    {
        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public ShowAccountsPendingModel(AppDomainProject.Models.AppDomainProjectContext context)
        {
            _context = context;
        }
        [BindProperty]
        public UserInfoData UserInfoData { get; set; }

        public void OnGet()
        {
        }
        /*
        public async Task<IActionResult> OnPostActivate(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            UserInfoData = await _context.UserInfoData.FindAsync(id);
            _context.UserInfoData.Update();
        }
        */
        /*
        public void OnPostActivate(string Id)
        {
            UserInfoData = await _context.UserInfoData.FindAsync(id);
        }
        */
        public void OnPostDelete()
        {

        }
    }
}
