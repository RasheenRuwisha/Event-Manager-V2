using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Model
{
    public class UserEvent
    {

        [Key]
        public string EventId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(15)]
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RepeatType { get; set; }
        public string RepeatDuration { get; set; }
        public int RepeatCount { get; set; }
        public DateTime RepeatTill { get; set; }
        public virtual List<EventContact> EventContacts { get; set; }
        public String ParentId { get; set; }
        [MaxLength(50)]
        public string AddressLine1 { get; set; }
        [MaxLength(50)]
        public string AddressLine2 { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string State { get; set; }
        [MaxLength(10)]
        public string Zipcode { get; set; }
    }
}
