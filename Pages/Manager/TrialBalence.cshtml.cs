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
    }
}