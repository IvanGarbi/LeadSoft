using LeadSoft.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadSoft.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.ArticleId)
                .IsRequired()
                .HasColumnType("UUID");

            builder.ToTable("Categories");
        }
    }
}
