using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoRestService.Models
{
    public class Tower
    {
        
        public string id { get; set; }
        public string municipal { get; set; }
        public string towername { get; set; }
        public string longitudecoord { get; set; }
        public string latitudecoord { get; set; }

    }
}