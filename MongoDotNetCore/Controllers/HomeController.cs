using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MangoDotCore.Models;
using MongoDB.Driver;

namespace MangoDotCore.Controllers
{
    public class HomeController : Controller
    {
        private IMongoDatabase mongoDatabase;

        //Generic method to get the mongodb database details  
        public IMongoDatabase GetMongoDatabase()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            return mongoClient.GetDatabase("EmployeeDB");
        }
        public IActionResult Index()
        {
            //Get the database connection  
            mongoDatabase = GetMongoDatabase();
            //fetch the details from CustomerDB and pass into view  
            var result = mongoDatabase.GetCollection<User>("user").Find(FilterDefinition<User>.Empty).ToList();
            return View(result);
            //return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User userdata)
        {
            try
            {
                //Get the database connection  
                mongoDatabase = GetMongoDatabase();
                mongoDatabase.GetCollection<User>("user").InsertOne(userdata);
            }
            catch (Exception ex)
            {
                throw;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Get the database connection  
            mongoDatabase = GetMongoDatabase();
            //fetch the details from CustomerDB and pass into view  
            User userdata = mongoDatabase.GetCollection<User>("user").Find<User>(k => k.pincode == id).FirstOrDefault();
            if (userdata == null)
            {
                return NotFound();
            }
            return View(userdata);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Get the database connection  
            mongoDatabase = GetMongoDatabase();
            //fetch the details from CustomerDB and pass into view  
            User customer = mongoDatabase.GetCollection<User>("user").Find<User>(k => k.pincode == id).FirstOrDefault();
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(User data)
        {
            try
            {
                //Get the database connection  
                mongoDatabase = GetMongoDatabase();
                //Delete the customer record  
                var result = mongoDatabase.GetCollection<User>("user").DeleteOne<User>(k => k.pincode == data.pincode);
                if (result.IsAcknowledged == false)
                {
                    return BadRequest("Unable to Delete Customer " + data.pincode);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Get the database connection  
            mongoDatabase = GetMongoDatabase();
            //fetch the details from CustomerDB based on id and pass into view  
            var customer = mongoDatabase.GetCollection<User>("user").Find<User>(k => k.pincode == id).FirstOrDefault();
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(User customer)
        {
            try
            {
                //Get the database connection  
                mongoDatabase = GetMongoDatabase();
                //Build the where condition  
                var filter = Builders<User>.Filter.Eq("pincode", customer.pincode);
                //Build the update statement   
                var updatestatement = Builders<User>.Update.Set("pincode", customer.pincode);
                updatestatement = updatestatement.Set("user", customer.user);
                updatestatement = updatestatement.Set("address", customer.address);
                updatestatement = updatestatement.Set("rank", customer.rank);             
                //fetch the details from CustomerDB based on id and pass into view  
                var result = mongoDatabase.GetCollection<User>("user").UpdateOne(filter, updatestatement);
                if (result.IsAcknowledged == false)
                {
                    return BadRequest("Unable to update Customer  " + customer.pincode);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
