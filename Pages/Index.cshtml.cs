using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppDomainProject.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;
        private readonly AppDomainProjectContext _context;

        [Display(Name = "Username:")]
        [BindProperty]
        public string Id { get; set; }

        [Display(Name = "Password:")]
        [BindProperty]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        public IndexModel(ILogger<IndexModel> logger, AppDomainProjectContext context)
        {
            //_logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Id = "";
            Pass = "";
            ModelState.Clear();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await ValidateAsync())
            {
                ClaimsIdentity claims = new ClaimsIdentity(new List<Claim>
                {
                   new Claim("ID", Id)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claims), properties);
                return GetDashboard();
            }
            else
            {
                return RedirectToPage("./Index");
            }
        }

            private async Task<bool> ValidateAsync()
        {
            var query = from u in _context.LoginData select u;
            if (!string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(Pass))
            {
                query = query.Where(m => m.ID.Equals(Id));
            }
            var users = await query.ToListAsync();
            if (users.Count != 1)
                return false;
            PasswordData user = users[0];

            if (!user.Password.Equals(Pass))
                return false;


            var query2 = from u in _context.UserInfoData select u;
            query2 = query2.Where(m => m.ID.Equals(Id));
            var info = await query2.FirstOrDefaultAsync();
            if (info.Status != AccountStatus.Active)
                return false;

            return true;
        }

        private IActionResult GetDashboard()
        {
            var query = from u in _context.UserInfoData select u;
            query = query.Where(m => m.ID.Equals(Id));
            UserInfoData info = query.FirstOrDefault();
            switch (info.Class)
            {
                case AccountType.Admin:  return Redirect("Admin"); 
                case AccountType.Manager: return Redirect("Manager"); 
                case AccountType.User: return Redirect("User"); 
                default: return NotFound();
            }
            
            
        }
    }
}
