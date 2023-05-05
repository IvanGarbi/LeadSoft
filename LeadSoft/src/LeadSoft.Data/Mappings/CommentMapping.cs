using LeadSoft.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadSoft.Data.Mappings
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Text)
                .IsRequired()
                .HasColumnType("NVARCHAR()");

            builder.Property(x => x.ArticleId)
                .IsRequired()
                .HasColumnType("UNIQUEIDENTIFIER");

            builder.ToTable("Comments");
        }
    }
}
