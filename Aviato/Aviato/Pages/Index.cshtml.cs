using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Bson;
using Aviato.Models;
using Aviato.Helpers;

namespace Aviato.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMongoCollection<Product> collection;

        public IList<Product> products { get; set; }

        public IndexModel(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            collection = database.GetCollection<Product>("Products");

            products = new List<Product>();
        }

        public async Task OnGetAsync()
        {
            products = await (await collection.FindAsync(_ => true)).ToListAsync();
            Helper.Shuffle(products);
        }

        public async Task OnGetCategoryAsync(string category)
        {
            products = await (await collection.FindAsync(p => p.Category == category)).ToListAsync();
        }
        
    }
}
