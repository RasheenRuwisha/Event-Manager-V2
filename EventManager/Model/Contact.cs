using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Model
{
    public class Contact
    {

        [Key]
        public string ContactId { get; set; }
      

        public virtual User User { get; set; }
        [MaxLength(75)]
        public string Email { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; }
        public string Image { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
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


        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
