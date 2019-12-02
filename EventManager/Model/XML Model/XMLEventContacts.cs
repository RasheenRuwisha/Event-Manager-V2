using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EventManager.Model.XML_Model
{
    [XmlType("contact")]
    public class XMLEventContacts
    {
        [XmlElement("userId")]
        public string UserId { get; set; }

        [XmlElement("contactId")]
        public string ContactId { get; set; }
        [XmlElement("eventId")]
        public string EventId { get; set; }

    }
}
