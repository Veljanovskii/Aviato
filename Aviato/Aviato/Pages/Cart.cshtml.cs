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
    public class CartModel : PageModel
    {
        private readonly IMongoCollection<Product> collection;
        public List<Product> cart { get; set; }

        public CartModel(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<Product>("Products");

            cart = new List<Product>();
        }

        public async Task OnGetAsync()
        {
            foreach (string id in CartHelper.Ids)
            {
                cart.Add(await (await collection.FindAsync<Product>(p => p.Id.Equals(id))).FirstOrDefaultAsync());
            }
        }

        public IActionResult OnPostDelete(string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            int index = CartHelper.Ids.IndexOf(id);

            CartHelper.Ids.RemoveAt(index);

            return RedirectToPage("Cart");
        }
    }
}
