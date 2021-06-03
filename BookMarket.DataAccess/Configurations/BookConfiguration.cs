using BookMarket.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.DataAccess.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Year).HasMaxLength(4);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Pages).IsRequired().HasMaxLength(10000);
            builder.HasMany(x => x.BookGenres)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
