using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public AddModel(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<Product>("Products");
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            //collection.InsertOne(product);

            await collection.InsertOneAsync(product);

            return RedirectToPage("Index");
        }
    }
}
