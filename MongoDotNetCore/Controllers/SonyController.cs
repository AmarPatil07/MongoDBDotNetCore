using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDotNetCore.Models;

namespace MongoDotNetCore.Controllers
{
    [Route("api/sony")]
    [ApiController]
    public class SonyController : ControllerBase
    {
        DataAccessLayer dataAccessLayer;

        public SonyController()
        {
            dataAccessLayer = new DataAccessLayer();
        }
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {         
            return dataAccessLayer.GetProducts();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product p)
        {
            dataAccessLayer.Create(p);
            return Ok(p);
        }
        
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var result = dataAccessLayer.Dalete(new MongoDB.Bson.ObjectId(id));
            return Ok(result);
        }
    }
}