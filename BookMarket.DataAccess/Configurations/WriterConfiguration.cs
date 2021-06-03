using BookMarket.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.DataAccess.Configurations
{
    public class WriterConfiguration : IEntityTypeConfiguration<Writer>
    {
        public void Configure(EntityTypeBuilder<Writer> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique(true);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasMany(x => x.Books)
                .WithOne(x => x.Writer)
                .HasForeignKey(x => x.WriterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
