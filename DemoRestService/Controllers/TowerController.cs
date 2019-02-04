using DemoRestService.DbConnections;
using DemoRestService.Models;
using DemoRestService.Processors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoRestService.Controllers
{
    public class TowerController : ApiController
    {


        // POST api/SaveTower
        [HttpPost]
        [Route("api/SaveTower")]
        public bool SaveTower(Tower tower)
        {
            if(tower == null)
            {
                return false;
            }
            return TowerProcessor.ProcessTower(tower);

        }


        // GET api/GetTowers
        [HttpGet]
        [Route("api/GetTowers")]
        public IEnumerable<Tower> GetTowers()
        {
            DataTable resultdt = DbConnector.GetTowersFromDB(null);
            if(resultdt == null)
            {
                return null;
            }
            var TowerList = new List<Tower>();
            foreach(DataRow row in resultdt.Rows)
            {
                var tower = new Tower
                {
                    id = row["id"].ToString(),
                    municipal = row["municipal"].ToString(),
                    towername = row["towername"].ToString(),
                    latitudecoord = row["latitudecoord"].ToString().Replace(",","."),
                    
                    longitudecoord = row["longitudecoord"].ToString().Replace(",", ".")
                };
                TowerList.Add(tower);
            }
            return TowerList;
        }


        [HttpGet]
        [Route("api/GetTowers/municipal")]        
        public IEnumerable<Tower> Get(string municipal)
        {
            DataTable resultdt = TowerProcessor.RemoveWhiteSpace(municipal);
            if(resultdt == null)
            {
                return null;
            }
            var TowerList = new List<Tower>();
            foreach(DataRow row in resultdt.Rows)
            {
                var tower = new Tower
                {
                    id = row["id"].ToString(),
                    municipal = row["municipal"].ToString(),
                    towername = row["towername"].ToString(),
                    latitudecoord = row["latitudecoord"].ToString().Replace(",", "."),
                    longitudecoord = row["longitudecoord"].ToString().Replace(",", ".")
                };
                TowerList.Add(tower);
            }
            return TowerList;
        }
    }

 
}
