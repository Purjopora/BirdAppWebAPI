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
        public bool SaveSighting(string user, string sightings)
        {

            return DbConnector.UpdateSightingsToDB(user, sightings);

        }


        //GET Sightings
        [HttpGet]
        [Route("api/GetSightings")]
        public string  GetSightings(string user)
        {

            DataTable resultdt = DbConnector.GetSightingsFromDB(user);
            if (resultdt == null)
            {
                return null;
            }

            DataRow row = resultdt.Rows[0];
            return row["sightinglist"].ToString();

        }





    }
}
