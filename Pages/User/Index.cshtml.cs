using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject.Pages.User
{
    
    public class IndexModel : UserPageModel
    {

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public bool Expired { get; set; }

        [BindProperty]
        public DateTime expdate { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public IndexModel(AppDomainProjectContext context) : base(context)
        {
        }

        public IActionResult OnGet()
        {
            PersonalInfoData info = PersonalInfo;
            if (info == null)
                return NotFound();

            UserInfoData date = UserInfo;
            DateTime today = DateTime.Today.AddDays(5);
            int res = DateTime.Compare(today, date.PasswordExpirationDate);
            expdate = date.PasswordExpirationDate;

            if(res >= 0)
            {
                Expired = true;
            }
            else { Expired = false; }

            Name = $"{info.FirstName} {info.LastName}";
            Username = info.ID;
            CheckJournals();
            return Page();
        }

        private void CheckJournals()
        {
            var q = from j in _context.JournalData where j.JournalStatus == JournalData.Status.incomplete where j.usetID == UserInfo.ID select j;
            int unfinishedJournals = q.Count();
            if(unfinishedJournals > 0)
            {
                Message = $"You have {unfinishedJournals} unsubmitted journal{(unfinishedJournals > 1 ? "s" : "")}";
            }
            else
            {
                Message = "";
            }
        }
    }
}
