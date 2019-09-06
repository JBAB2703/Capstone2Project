using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone2Project.Models {
    public class User 
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
        [Required]
        [MaxLength(30)]
        public string Firstname { get; set; }
        [Required]
        [MaxLength(30)]
        public string Lastname { get; set; }
        [Required]
        [MaxLength(12)]
        public string Phone { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        public bool IsReviewer { get; set; }
        [Required]
        public bool IsAdmin { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
