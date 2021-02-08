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
        [BsonRepresentation(BsonType.ObjectId)]         //to allow passing the parameter as type string instead of an ObjectId. Mongo handles the conversion from string to ObjectId.
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
