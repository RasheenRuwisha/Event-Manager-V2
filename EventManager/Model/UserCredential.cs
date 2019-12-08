using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Model
{
    public class UserCredential
    {

        [Key]
        [MaxLength(75)]
        public string Email { get; set; }
        [MaxLength(30)]
        public string Username { get; set; }
        public string Password { get; set; }


        [ForeignKey("User")]
        [MaxLength(10)]
        public string UserId { get; set; }
        public virtual User User { get; set; }

    }
}
