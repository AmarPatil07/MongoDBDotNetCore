using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDotNetCore.Models
{
    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("ProductId")]
        public double ProductId { get; set; }
        [BsonElement("ProductName")]
        public string ProductName { get; set; }
        [BsonElement("Price")]
        public double Price { get; set; }
        [BsonElement("Category")]
        public string Category { get; set; }
    }
}
