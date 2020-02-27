using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject
{
    public class JournalsModel : AuthenticatedPageModel
    {
        public JournalsModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        public List<TableRow> Journals { get; set; }

        public void OnGet()
        {
            Journals = new List<TableRow>();
            var q = from j in _context.JournalData join u in _context.PersonalInfoData on j.usetID equals u.ID where j.JournalStatus == JournalData.Status.approved select new { Journal = j.ID, First = u.FirstName, Last = u.LastName, User = u.ID };
            foreach(var i in q)
            {
                Journals.Add(new TableRow
                {
                    Journal = i.Journal,
                    Author = $"{i.First} {i.Last} ({i.User})"
                });
            }
        }

        public class TableRow
        {
            public int Journal { get; set; }
            public string Author { get; set; }
        }
    }
}