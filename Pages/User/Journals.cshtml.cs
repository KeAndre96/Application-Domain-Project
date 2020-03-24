using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppDomainProject
{
    public class JournalsModel : AuthenticatedPageModel
    {
        public JournalsModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        public List<TableRow> Journals { get; set; }
        [BindProperty]
        public List<JournalData> Journal { get; set; }
        public void OnGet(string? searchString)
        {
            /*var query = from m in _context.JournalData select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.ID.ToString().Contains(searchString));
                Journal = query.ToList();
            }
            */
                Journals = new List<TableRow>();
                var q = from j in _context.JournalData join u in _context.PersonalInfoData on j.usetID equals u.ID where j.JournalStatus == JournalData.Status.approved select new { Journal = j.ID, First = u.FirstName, Last = u.LastName, User = u.ID };
                foreach (var i in q)
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