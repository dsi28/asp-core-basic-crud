using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Pages
{
    public class EditModel : PageModel
    {
        private readonly AppDbConext _db;
        public EditModel(AppDbConext db)
        {
            _db = db;
        }
        [BindProperty]
        public Customer Cx { get; set; }
        // get the id of the  Customer object : if not found return to index
        public async Task<ActionResult> OnGetAsync(int id)
        {
            Cx = await _db.Customers.FindAsync(id);
            if (Cx == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
        //update the Customer object 
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }
            _db.Attach(Cx).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) {
                throw new Exception($"Cx : {Cx.Id} not found", e);
            }
            return RedirectToPage("./Index");
        }

    }
}