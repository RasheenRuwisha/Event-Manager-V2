using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Model
{
    class EventEdits
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("UserEvent")]
        public string eventid { get; set; }
        public virtual UserEvent UserEvent { get; set; }


        [ForeignKey("User")]
        public string userid { get; set; }
        public virtual User User { get; set; }



        [MaxLength(100)]
        public string title { get; set; }
        [MaxLength(500)]
        public string description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RepeatType { get; set; }
        public string RepeatDuration { get; set; }
        public int RepeatCount { get; set; }
        public DateTime RepeatTill { get; set; }
        public virtual List<EventContact> EventContacts { get; set; }
        [MaxLength(50)]
        public string AddressLine1 { get; set; }
        [MaxLength(50)]
        public string AddressLine2 { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string State { get; set; }
        [MaxLength(50)]
        public string Zipcode { get; set; }

    }
}
