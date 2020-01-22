using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppDomainProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDomainProjectContext _context;

        [Display(Name = "Username")]
        [BindProperty]
        public string Id { get; set; }

        [Display(Name = "Password")]
        [BindProperty]
        public string Pass { get; set; }

        public IndexModel(ILogger<IndexModel> logger, AppDomainProjectContext context)
        {
            _logger = logger;
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
            return user.Password.Equals(Pass);
        }

        private IActionResult GetDashboard()
        {
            var query = from u in _context.UserInfoData select u;
            query = query.Where(m => m.ID.Equals(Id));
            UserInfoData info = query.FirstOrDefault();
            switch (info.Class)
            {
                case AccountType.Admin:  return Redirect("Admin/" + Id); 
                case AccountType.Manager: return Redirect("Manager/" + Id); 
                case AccountType.User: return Redirect("User/" + Id); 
                default: return NotFound();
            }
            
            
        }
    }
}
