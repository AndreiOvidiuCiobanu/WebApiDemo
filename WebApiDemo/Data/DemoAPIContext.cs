using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.Data
{
    public class DemoAPIContext : DbContext
    {
        public DemoAPIContext(DbContextOptions<DemoAPIContext> opt) : base(opt)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasMany(t => t.Books).WithOne(t => t.Author).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
