using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        //instance of the in memory db Class
        private readonly AppDbConext _db;
        public IndexModel(AppDbConext db)
        {
            _db = db;
        }
        //Customer list to store all the customer from the db
        public IList<Customer> Cxs { get; private set; }

        //catches the message from create
        [TempData]
        public string Message { get; set; }


        //get all the customers from db and add them to the Cxs list
        public async Task OnGetAsync()
        {
            Cxs = await _db.Customers.AsNoTracking().ToListAsync();
        }
        //Important OnPost"Delete"Async is the name of the handler. which is how asp-page-handler can access this
        //this looks for the Customer object in the db with the id
        //if found removes and saves the chanages then reloads the page
        public async Task<IActionResult> OnPostDeleteAsync(int id) {
            var cx = await _db.Customers.FindAsync(id);
            if (cx != null)
            {
                _db.Customers.Remove(cx);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        // this was a test to go to create page with the GOCREATE asp-page-handler 
        public async Task<IActionResult> OnPostGOCREATEAsync()
        {
            return RedirectToPage("/Create");
        }
    }
}
