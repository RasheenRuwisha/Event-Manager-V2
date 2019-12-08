using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Model
{
    public class EventContact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        [MaxLength(10)]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Contact")]
        [MaxLength(10)]
        public string ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        public string ContactName { get; set; }

        [ForeignKey("Event")]
        [MaxLength(10)]
        public string EventId { get; set; }
        public virtual UserEvent Event { get; set; }


    }
}
