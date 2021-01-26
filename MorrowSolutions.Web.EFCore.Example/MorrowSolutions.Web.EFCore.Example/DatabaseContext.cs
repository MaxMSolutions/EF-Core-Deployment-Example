using Microsoft.EntityFrameworkCore;
using MorrowSolutions.Web.EFCore.Example.Models;

namespace MorrowSolutions.Web.EFCore.Example
{
   public class DatabaseContext : DbContext
   {
      
      public virtual DbSet<Post> Posts { get; set; }

      public DatabaseContext() { }

      public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
      {
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
      }
   }
}
