using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject
{
    public class PendingJournalsModel : AuthenticatedPageModel
    {
        public PendingJournalsModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        public List<JournalsModel.TableRow> Pending { get; set; }

        [BindProperty]
        public List<JournalsModel.TableRow> InProgress { get; set; }
        
        public void OnGet()
        {
            Pending = new List<JournalsModel.TableRow>();
            var q1 = from j in _context.JournalData join u in _context.PersonalInfoData on j.usetID equals u.ID where j.JournalStatus == JournalData.Status.pending select new { Journal = j.ID, First = u.FirstName, Last = u.LastName, User = u.ID };
            foreach(var i in q1)
            {
                Pending.Add(new JournalsModel.TableRow
                {
                    Journal = i.Journal,
                    Author = $"{i.First} {i.Last} ({i.User})"
                });
            }

            InProgress = new List<JournalsModel.TableRow>();
            var q2 = from j in _context.JournalData join u in _context.PersonalInfoData on j.usetID equals u.ID where j.JournalStatus == JournalData.Status.incomplete where u.ID == UserInfo.ID select new { Journal = j.ID, First = u.FirstName, Last = u.LastName, User = u.ID };
            foreach (var i in q2)
            {
                InProgress.Add(new JournalsModel.TableRow
                {
                    Journal = i.Journal,
                    Author = $"{i.First} {i.Last} ({i.User})"
                });
            }
        }
    }
}