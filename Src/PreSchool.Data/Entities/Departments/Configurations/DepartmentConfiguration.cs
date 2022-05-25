using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Departments.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department", "Department");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.ConfigureCommonProperties();



        }
    }
}
