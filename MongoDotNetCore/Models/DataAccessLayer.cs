using MangoDotCore.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDotNetCore.Models
{
    public class DataAccessLayer
    {
        public MongoClient _mongoClient;
        public IMongoDatabase mongoDatabase;
        public DataAccessLayer()
        {
            _mongoClient = new MongoClient("mongodb://localhost:27017");
            mongoDatabase = _mongoClient.GetDatabase("SonyDB");
        }

        public IEnumerable<Product> GetProducts()
        {
            var data =  mongoDatabase.GetCollection<Product>("products").Find(FilterDefinition<Product>.Empty).ToList();
            return data;
        }


        public Product GetProduct(ObjectId id)
        {
            var row = mongoDatabase.GetCollection<Product>("products").Find(x => x.Id == id).FirstOrDefault();
            return row;
        }

        public Product Create(Product product)
        {
            mongoDatabase.GetCollection<Product>("products").InsertOne(product);
            return product;
        }

        public Product update(Product product)
        {

           // var result =  mongoDatabase.GetCollection<Product>("product").UpdateOne(Builders<Product>.Filter.Eq("Id", product.Id),UpdateDefinition<Product> product());
            return product;
        }

        public DeleteResult Dalete(ObjectId id)
        {
            var result = mongoDatabase.GetCollection<Product>("products").DeleteOne(x => x.Id == id);           
            return result;
        }

    }
}
