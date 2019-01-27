using DemoRestService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoRestService.Controllers
{
    public class BirdSightingController : ApiController
    {
        // GET: api/Bird
        public IEnumerable<BirdSighting> Get()
        {
            var BirdSightingList = new List<BirdSighting>();
            for(int i  = 0; i < 10; i++)
            {
                var BirdSighting = new BirdSighting
                {
                    Location = $"Location {i}",
                    Specie = "kondorikotka",
                    Amount = i * 3,
                    DateTime = DateTime.Now.ToUniversalTime()



                };
                BirdSightingList.Add(BirdSighting);
            }
            return BirdSightingList;
        }

        // GET: api/Bird/5
        public BirdSighting Get(int id)
        {
            return new BirdSighting
            {
                Location = $"Location {id}",
                Specie = "Specie",
                Amount = 3,
                DateTime = DateTime.Now.ToUniversalTime()
            };
        }

        
    }
}
