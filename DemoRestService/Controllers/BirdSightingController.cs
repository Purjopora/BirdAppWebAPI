using DemoRestService.DbConnections;
using DemoRestService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoRestService.Controllers
{
    public class BirdSightingController : ApiController
    {
        //POST NEW Sighting
        [HttpPost]
        [Route("api/SaveSighting")]
        public bool SaveSighting(BirdSighting birdsighting)
        {


            return DbConnector.UpdateSightingsToDB(birdsighting);

        }


        //GET Sightings
        [HttpGet]
        [Route("api/GetSightings")]
        public IEnumerable<BirdSighting> GetSightingsFromUser(string user)
        {

            DataTable resultdt = DbConnector.GetSightingsFromDB(user);
            if (resultdt == null)
            {
                return null;
            }

            var SightingList = new List<BirdSighting>();
            foreach (DataRow row in resultdt.Rows)
            {

                var sighting = new BirdSighting
                {
                    username = row["username"].ToString(),
                    specie = row["specie"].ToString(),
                    longitudecoord = Convert.ToDouble(row["longitudecoord"]),
                    latitudecoord = Convert.ToDouble(row["latitudecoord"]),
                    comment = row["comment"].ToString(),
                    timestamp = Convert.ToDateTime(row["timestamp"])

                };
                SightingList.Add(sighting);
            }
            return SightingList;

        }

        [HttpGet]
        [Route("api/GetSightings/Bird")]
        public IEnumerable<BirdSighting> GetSightingsForBird(string bird)
        {
            DataTable resultdt = DbConnector.GetSightingsForBird(bird);
            var SightingList = new List<BirdSighting>();
            foreach (DataRow row in resultdt.Rows)
            {
                var sighting = new BirdSighting
                {
                    username = "",
                    specie = "",
                    longitudecoord = Double.Parse(row["longtitude"].ToString()),
                    latitudecoord = Double.Parse(row["latitude"].ToString()),
                    comment = "",
                    timestamp = DateTime.Now

                };
                SightingList.Add(sighting);
            }
            return SightingList;
        }

        [HttpGet]
        [Route("api/updateSightings")]
        public bool updateSightings()
        {
            return DbConnector.updateSightings();
        }
    }
}
