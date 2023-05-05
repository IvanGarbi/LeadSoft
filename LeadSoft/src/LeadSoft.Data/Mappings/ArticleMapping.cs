using LeadSoft.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadSoft.Data.Mappings
{
    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Text)
                .IsRequired()
                .HasColumnType("TEXT");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.AuthorId)
                .IsRequired()
                .HasColumnType("UUID");

            builder.HasMany(x => x.Comments)
                .WithOne(y => y.Article)
                .HasForeignKey(f => f.ArticleId);

            builder.HasOne(x => x.Category)
                .WithOne(y => y.Article)
                .HasForeignKey<Category>(e => e.ArticleId);

            builder.ToTable("Articles");
        }
    }
}
