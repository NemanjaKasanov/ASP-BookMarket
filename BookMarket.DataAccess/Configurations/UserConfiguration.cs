using BookMarket.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMarket.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(40);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(40);
            builder.HasAlternateKey(x => x.Username);
            builder.Property(x => x.Username).HasMaxLength(40);
            builder.HasAlternateKey(x => x.Email);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(22);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(22);
            builder.Property(x => x.Role).IsRequired().HasDefaultValue(0);
        }
    }
}
