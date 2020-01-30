using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject.Pages.Manager
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy = "user")]
    public class IndexModel : PageModel
    {
        private AppDomainProjectContext _context;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Username { get; set; }


        public IndexModel(AppDomainProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            string id = HttpContext.User.FindFirst("ID").Value;

            var query = from u in _context.PersonalInfoData select u;
            if (!string.IsNullOrEmpty(id))
            {
                query = query.Where(m => m.ID.Equals(id));
            }
            PersonalInfoData info = query.FirstOrDefault();
            if (info == null)
                return NotFound();

            Name = $"{info.FirstName} {info.LastName}";
            Username = info.ID;

            var query2 = from u in _context.UserInfoData select u;
            if (!string.IsNullOrEmpty(id))
            {
                query2 = query2.Where(m => m.ID.Equals(id));
            }
            UserInfoData uinfo = query2.FirstOrDefault();
            if (uinfo == null)
                return NotFound();
            if (uinfo.Class != AccountType.Manager)
                return NotFound();

            return Page();
        }
    }
}
