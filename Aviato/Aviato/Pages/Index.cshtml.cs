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
        //private readonly ILogger<IndexModel> _logger;
        private readonly IMongoCollection<Product> _products;

        public IList<Product> products { get; set; }

        public Product lmao { get; set; }

        public IndexModel(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _products = database.GetCollection<Product>("Products");

            products = new List<Product>();
        }

        public async Task OnGetAsync()
        {
            products = await (await _products.FindAsync(_ => true)).ToListAsync();
            Helper.Shuffle(products);
        }

        public async Task OnGetCategoryAsync(string category)
        {
            products = await (await _products.FindAsync(p => p.Category == category)).ToListAsync();
        }
        
    }
}
