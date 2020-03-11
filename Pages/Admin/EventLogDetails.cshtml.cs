using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDomainProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AppDomainProject
{
   
    public class ViewEventLogDetailsModel : AdminPageModel
    {
        public AccountData After { get; set; }
        public AccountData Before { get; set; }
        public EventLogData Event { get; set; }

        public string Side { get; set; }

        public string[] Sides = new string[] { "Credit", "Debit" };

        public ViewEventLogDetailsModel(AppDomainProjectContext context) : base(context)
        {
        }

        public IActionResult OnGet(string id)
        {
            var query = from m in _context.EventLogData select m;
            query = query.Where(n => n.id.Equals(id));

            EventLogData log = query.FirstOrDefault();

            if (log == null)
                return NotFound();

            After = JsonConvert.DeserializeObject<AccountData>(log.after_image);
            Before = JsonConvert.DeserializeObject<AccountData>(log.before_image);
            return Page();
        }

    }
}