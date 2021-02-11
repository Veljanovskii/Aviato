using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aviato.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Price")]
        public double Price { get; set; }
        [BsonElement("Category")]
        public string Category { get; set; }
        [BsonElement("Colour")]
        public string Colour { get; set; }
        [BsonElement("Details")]
        public string Details { get; set; }
        [BsonElement("Brand")]
        public string Brand { get; set; }
    }
}
