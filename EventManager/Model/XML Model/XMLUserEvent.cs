using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EventManager.Model.XML_Model
{
    [XmlType("Event")]
    public class XMLUserEvent
    {

        [XmlElement("id")]
        public string eventid { get; set; }
        public string userid { get; set; }
        public virtual User User { get; set; }
        [XmlElement("title")]
        public string title { get; set; }
        [XmlElement("description")]
        public string description { get; set; }
        [XmlElement("type")]
        public string type { get; set; }
        [XmlElement("startdate")]
        public DateTime StartDate { get; set; }
        [XmlElement("enddate")]
        public DateTime EndDate { get; set; }
        [XmlElement("repeatType")]
        public string RepeatType { get; set; }
        [XmlElement("repeatDuration")]
        public string RepeatDuration { get; set; }
        [XmlElement("repeatCount")]
        public int RepeatCount { get; set; }
        [XmlElement("repeatTill")]
        public DateTime RepeatTill { get; set; }
        [XmlArray("contacts")]
        public virtual List<XMLEventContacts> EventContacts { get; set; }
        public string HasExceptions { get; set; }
        [XmlElement("addressline1")]
        public string AddressLine1 { get; set; }
        [XmlElement("addressline2")]
        public string AddressLine2 { get; set; }
        [XmlElement("city")]
        public string City { get; set; }
        [XmlElement("state")]
        public string State { get; set; }
        [XmlElement("zip")]
        public string Zipcode { get; set; }


    }
}
