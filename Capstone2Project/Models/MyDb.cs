using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone2Project.Models {
    public class MyDb : DbContext{
        public DbSet<User> Users { get; set; }
        public DbSet<Vendor>Vendors { get; set; }
        public DbSet<Product>Products { get; set; }
        public DbSet<Request>Requests { get; set; }
        public DbSet<RequestLine>RequestLines { get; set; }

        public MyDb(DbContextOptions options) : base(options) { }
    }
}
