using BookMarket.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.DataAccess.Configurations
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique(true);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.HasMany(x => x.Books)
                .WithOne(x => x.Publisher)
                .HasForeignKey(x => x.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
