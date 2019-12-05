using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Model
{
    public class User
    {

        [Key]
        [MaxLength(10)]
        public string UserId { get; set; }
        [MaxLength(75)]
        public string Email { get; set; }
        public string Image { get; set; }
        [MaxLength(30)]
        public string Username { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }
    }
}
