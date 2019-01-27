using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DemoRestService.Models
{
    [DataContract]
    public class BirdSighting
    {
        [DataMember(Name = "location")]
        public string Location { get; set; }

        [DataMember(Name = "specie")]
        public string Specie { get; set; }

        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "dateTime")]
        public DateTime DateTime { get; set; }
    }
}