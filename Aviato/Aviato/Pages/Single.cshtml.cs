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
    public class SingleModel : PageModel
    {
        private readonly IMongoCollection<Product> collection;
        [BindProperty]
        public Product product { get; set; }
        [BindProperty]
        public List<Product> related { get; set; }
        public Employee Employee { get; set; }

        public SingleModel(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<Product>("Products");

            product = new Product();
            related = new List<Product>();
        }

        public async Task OnGetAsync(string id)
        {
            product = await (await collection.FindAsync<Product>(p => p.Id.Equals(id))).FirstOrDefaultAsync();

            var gender = product.Category.Split('\'')[0];
            related = await (await collection.FindAsync<Product>(p => p.Category.StartsWith(gender) && p.Id != id)).ToListAsync();
            Helper.Shuffle(related);

            Employee = SessionHelper.GetObjectFromJson<Employee>(HttpContext.Session, "loginEmployee");
        }

        public IActionResult OnPostAdd()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            CartHelper.Ids.Add(product.Id);

            return Redirect("Single?id=" + product.Id);
        }

        public async Task<IActionResult> OnPostRemoveAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            await collection.DeleteOneAsync(p => p.Id == product.Id);

            return RedirectToPage("Index");
        }

    }
}
