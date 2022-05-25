using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities
{
    /// <summary>
    /// Column configuration for the common property of the column
    /// Inheriet this class and call ConfigureCommon(builder) to add configuration for common properties across all the table
    /// </summary>
    public static class CommonTypePropertiesConfiguration
    {
        public static void ConfigureCommonTypeProperties<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : CommonTypeProperties
        {
            builder.HasKey(e => e.Id);
            builder.Property(p => p.Id)
                           .ValueGeneratedNever();

            builder.Property(e => e.Name)
                 .IsRequired()
                .HasMaxLength(124);

            builder.Property(e => e.DisplayName)
                .IsRequired()
               .HasMaxLength(124);

            builder.Property(e => e.Description)
                .HasMaxLength(124);



        }
    }
}
