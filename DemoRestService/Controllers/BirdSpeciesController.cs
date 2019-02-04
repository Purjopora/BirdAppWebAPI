using DemoRestService.DbConnections;
using DemoRestService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DemoRestService.Controllers
{
    public class BirdSpeciesController : ApiController
    {


        [HttpGet]
        [Route("api/GetSpecies")]
        public IEnumerable<Specie> GetSpecies()
        {
            DataTable resultdt = DbConnector.GetSpeciesFromDB();
            if (resultdt == null)
            {
                return null;
            }
            var SpecieList = new List<Specie>();
            foreach (DataRow row in resultdt.Rows)
            {
                var specie = new Specie
                {
                    id = row["id"].ToString(),
                    speciename = row["speciename"].ToString()
                };
                SpecieList.Add(specie);
            }
            return SpecieList;
        }
    }
}