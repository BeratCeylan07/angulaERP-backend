using ERPServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPServer.Infrastructure.Configurations;

internal sealed class RecipeDetailConfiguration : IEntityTypeConfiguration<RecipeDetail>
{
    public void Configure(EntityTypeBuilder<RecipeDetail> builder)
    {
        builder.Property(p => p.Quantity).HasColumnType("decimal(7,2)"); // Toplam 7 karakter yazılabilir ! 00000,00 (virgülden sonra 2 karakter yazılabilir)
    }
}