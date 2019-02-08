using DemoRestService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoRestService.DbConnections;

namespace DemoRestService.Processors
{
    public class BirdSightingProcessor
    {
        public static bool ProcessSighting(BirdSighting sighting)
        {
            //TODO: add some validation here in future??
            return DbConnector.UpdateSightingsToDB(sighting);
        }

     
    }
}