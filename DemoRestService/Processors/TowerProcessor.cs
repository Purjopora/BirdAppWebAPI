using DemoRestService.DbConnections;
using DemoRestService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DemoRestService.Processors
{
    public class TowerProcessor
    {
        public static bool ProcessTower(Tower tower)
        {
            //processing, validating, formating
            return DbConnector.AddTowerToDB(tower);
        }

        public static DataTable RemoveWhiteSpace(string municipal)
        {
            municipal = municipal.Replace(" ", String.Empty);
            return DbConnector.GetTowersFromDB(municipal);
        }
    }
}