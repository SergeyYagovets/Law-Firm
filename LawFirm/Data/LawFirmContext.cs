using LawFirm.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawFirm.Data
{
    public class LawFirmContext : DbContext
    {
        public LawFirmContext(DbContextOptions<LawFirmContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Founder> Founders { get; set; }
    }
}
