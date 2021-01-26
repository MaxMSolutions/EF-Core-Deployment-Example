using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MorrowSolutions.Web.EFCore.Example.Models;

namespace MorrowSolutions.Web.EFCore.Example.EntityConfiguration
{
   public class AgencyConfiguration : IEntityTypeConfiguration<Post>
   {
      public void Configure(EntityTypeBuilder<Post> builder)
      {
         builder.HasKey(c => c.Id);
         builder.Property(c => c.Id);
         builder.Property(c => c.Title).IsRequired(true).HasMaxLength(255);
         builder.Property(c => c.Content).IsRequired(true).HasMaxLength(4000);
      }
   }
}
