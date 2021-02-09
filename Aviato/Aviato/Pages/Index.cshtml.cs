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

namespace Aviato.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMongoCollection<Product> _products;

        public Product lmao { get; set; }

        public IndexModel(IDatabaseSettings settings)
        {
            //_logger = logger;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _products = database.GetCollection<Product>("Products");
        }

        public void OnGet()
        {
            lmao = _products.Find(lol => true).First();
        }
    }
}
