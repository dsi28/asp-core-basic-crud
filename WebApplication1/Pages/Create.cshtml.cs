using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Pages
{
    public class CreateModel : PageModel
    {
        //instance of the in memory db Class
        private readonly AppDbConext _db;

        // instance of the Ilogger object
        private ILogger<CreateModel> _log;

        //instance of the Customer class that can be used in the view(Creaete.cshtml):
        //add the BindProperty to link the Cx to the form
        [BindProperty]
        public Customer Cx { get; set; }

        public CreateModel(AppDbConext db, ILogger<CreateModel> log)
        {
            _db = db;
            _log = log;
        }
        //string object that will make logg for when a Cx is created
        [TempData]
        public string Message { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                //if the state of the model is in valid return this page?
                return Page();
            }
            // add the customer from the form (cx) into the list in the in memory db
            _db.Customers.Add(Cx);
            await _db.SaveChangesAsync();

            //set the message string this can get caught by index
            Message = $"Cx {Cx.Name} added!";
            //sending the message to the logg
            _log.LogCritical(Message.ToString());
            //go to index when done 
            return RedirectToPage("/Index");
        }
    }
}