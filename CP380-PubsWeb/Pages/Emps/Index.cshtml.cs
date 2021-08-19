using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models = CP380_PubsLab.Models;

namespace CP380_PubsWeb.Pages.Emps
{
    public class IndexModel : PageModel
    {
        private readonly Models.PubsDbContext pubsDb = new Models.PubsDbContext();

        [BindProperty]
        public List<Models.Employee> ViewEmpLists { get; set; }
        public async Task<IActionResult> OnGet(Int16 jobId)
        {
            ViewEmpLists = await pubsDb.Employees.Where(e => e.Jobs.job_id == jobId).ToListAsync();
            return Page();
        }
    }
}
