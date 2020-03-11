using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AppDomainProject.Models;

namespace AppDomainProject
{
    public class EventLogModel : PageModel
    {
        private readonly AppDomainProject.Models.AppDomainProjectContext _context;

        public EventLogModel(AppDomainProject.Models.AppDomainProjectContext context)
        {
            _context = context;
        }

        public IList<EventLogData> EventLogData { get;set; }

        public async Task OnGetAsync()
        {
            EventLogData = await _context.EventLogData.ToListAsync();
            
        }

        public async Task<IActionResult> OnPostDelete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var EventLogData = await _context.EventLogData.ToListAsync();
            var temp = EventLogData.Find(m => m.id.Equals(id));

            if (temp != null)
            {
                _context.EventLogData.Remove(temp);
                await _context.SaveChangesAsync();
            }
            return Redirect("./EventLog");
        }
        public async Task<IActionResult> OnPostView(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EventLogData = await _context.EventLogData.ToListAsync();

            return Redirect($"./EventLogDetails/{id}");
        }
        public IActionResult OnPost(string acct)
        {
            return Redirect($"./EventLog/{acct}");
        }
    }
}
