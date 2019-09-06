using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone2Project.Models {
    public class Product 
        {
        public Product() 
        {
            RequestLine = new HashSet<RequestLine>();
        }

        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string PartNumber { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength()]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(30)]
        public string Unit { get; set; }
        [MaxLength(255)]
        public string PhotoPath { get; set; }
        [Required]
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<RequestLine> RequestLine { get; set; }
    }
}
