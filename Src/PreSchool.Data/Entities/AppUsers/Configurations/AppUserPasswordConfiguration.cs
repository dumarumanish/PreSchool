﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.AppUsers.Configurations
{
    public class AppUserPasswordConfiguration : IEntityTypeConfiguration<AppUserPassword>
    {
        public void Configure(EntityTypeBuilder<AppUserPassword> builder)
        {
            builder.ToTable("Password", "AppUser");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.PasswordHash)
             .IsRequired();

            builder.Property(e => e.PasswordSalt)
                .IsRequired();
        }
    }
}
