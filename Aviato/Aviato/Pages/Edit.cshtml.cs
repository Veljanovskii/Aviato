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
    public class EditModel : PageModel
    {
        private readonly IMongoCollection<Product> collection;

        [BindProperty]
        public Product product { get; set; }
        public Employee Employee { get; set; }

        public EditModel(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<Product>("Products");
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            product = await(await collection.FindAsync<Product>(p => p.Id.Equals(id))).FirstOrDefaultAsync();

            Employee = SessionHelper.GetObjectFromJson<Employee>(HttpContext.Session, "loginEmployee");
            if (Employee == null)
            {
                return Redirect("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            await collection.ReplaceOneAsync(p => p.Id == id, product);

            return Redirect("Single?id=" + id);
        }
    }
}
