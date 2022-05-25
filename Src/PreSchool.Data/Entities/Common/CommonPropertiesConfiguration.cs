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
    public static class CommonPropertiesConfiguration
    {
        public static void ConfigureCommonProperties<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : CommonProperties
        {
            builder.Property(e => e.CreatedBy)
                .IsRequired()
                ;

            builder.Property(e => e.CreatedOn)
               .IsRequired()
               .HasDefaultValueSql("getdate()");

            builder.Property(e => e.ModifiedBy)
                .IsRequired(false)
                .HasDefaultValue(null);

            builder.Property(e => e.ModifiedOn)
                .IsRequired(false)
                .HasDefaultValue(null);

            builder.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasQueryFilter(e => !e.IsDeleted);


        }
    }
}
