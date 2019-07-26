using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangoDotCore.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement]
        public string user { get; set; }
        [BsonElement]
        public string address { get; set; }
        [BsonElement]
        public double pincode { get; set; }
        [BsonElement]
        public double rank { get; set; }
    }
}
