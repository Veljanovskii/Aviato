using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aviato.Helpers;
using Aviato.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;

namespace Aviato.Pages
{
    public class AddModel : PageModel
    {
        private readonly IMongoCollection<Product> collection;

        [BindProperty]
        public Product product { get; set; }
        public Employee Employee { get; set; }

        public AddModel(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<Product>("Products");
        }

        public IActionResult OnGet()
        {
            Employee = SessionHelper.GetObjectFromJson<Employee>(HttpContext.Session, "loginEmployee");
            if (Employee == null)
            {
                return Redirect("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            await collection.InsertOneAsync(product);

            return RedirectToPage("Index");
        }
    }
}
