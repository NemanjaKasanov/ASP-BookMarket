using BookMarket.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.DataAccess.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique(true);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.HasMany(x => x.BookGenres)
                .WithOne(x => x.Genre)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
