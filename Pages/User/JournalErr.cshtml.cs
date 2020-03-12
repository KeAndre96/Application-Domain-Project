using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject
{
    public class JournalErrModel : AuthenticatedPageModel
    {
        public JournalErrModel(AppDomainProjectContext context) : base(context)
        {
        }

        [BindProperty]
        [DataType(DataType.Currency)]
        public double Credits { get; set; }

        [BindProperty]
        [DataType(DataType.Currency)]
        public double Debits { get; set; }

        [BindProperty]
        public int Journal { get; set; }

        public void OnGet(int journal)
        {
            Journal = journal;
            Credits = 0;
            Debits = 0;
            var q = from t in _context.TransactionData where t.Journal == journal select t;
            foreach(TransactionData t in q)
            {
                Credits += t.Credits;
                Debits += t.Debits;
            }
        }
    }
}