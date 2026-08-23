using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Mostra.Domain.Entities;

namespace Mostra.Infrastructure.Persistence
{
    public class MostraContext : DbContext   
    {
        public DbSet<Product> Products { get; set; }
        public MostraContext(DbContextOptions<MostraContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
        }
    }
}