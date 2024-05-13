using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Company.Models;

namespace Company.Data
{
    public class CompanyContext : DbContext
    {
        public CompanyContext (DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public DbSet<Company.Models.Customer> Customer { get; set; } = default!;
        public DbSet<Company.Models.Order> Order { get; set; } = default!;
        public DbSet<Company.Models.OrderItem> OrderItem { get; set; } = default!;
    }
}
