using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppDomainProject
{
    public class TrialBalenceModel : PageModel
    {
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime TransactionStartDate { get; set; }

        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime TransactionEndDate { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost(DateTime TransactionStartDate, DateTime TransactionEndDate)
        {
            if (TransactionStartDate == null || TransactionEndDate == null)
            {
                return NotFound();
            }

            //var EventLogData = await _context.EventLogData.ToListAsync();

            //return Redirect($"./View_Trial_Balence/{TransactionStartDate}{TransactionEndDate}");
            return Redirect("./View_Trial_Balence");
        }
    }
}