﻿using LeadSoft.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeadSoft.Data.Mappings
{
    public class AuthorMapping : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(x => x.Id);


            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.DateOfBirth)
                .IsRequired()
                .HasColumnType("DATE");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.HasMany(x => x.Articles)
                .WithOne(y => y.Author)
                .HasForeignKey(f => f.AuthorId);

            builder.ToTable("Authors");
        }
    }
}
