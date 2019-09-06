using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone2Project.Models {
    public class Request {

        public Request() 
        {
            RequestLine = new HashSet<RequestLine>();
        }
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Description { get; set; }
        [Required]
        [MaxLength(80)]
        public string Justification { get; set; }
        [MaxLength(80)]
        public string RejectionReason { get; set; }
        [Required]
        [MaxLength(20)]
        public string DeliveryMode { get; set; }
        [Required]
        [MaxLength(10)]
        public string Status { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<RequestLine>RequestLine { get; set; }

    }
}
