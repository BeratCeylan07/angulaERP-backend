using ERPServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPServer.Infrastructure.Configurations;

internal sealed class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        // Recipe silme işlemi yaparken Product ile Recipe arasında bire çok ilişki var Ben Recipe silmek istersem o product silme! dedik
        builder.HasOne(p => p.Product).WithMany().OnDelete(DeleteBehavior.NoAction);
    }
}