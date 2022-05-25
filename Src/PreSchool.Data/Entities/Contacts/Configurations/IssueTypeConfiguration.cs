
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PreSchool.Data.Entities.Contacts.Configurations
{

    public class IssueTypeConfiguration : IEntityTypeConfiguration<IssueType>
    {
        public void Configure(EntityTypeBuilder<IssueType> builder)
        {
            builder.ToTable("IssueType", "Contact");


            builder.ConfigureCommonProperties();
        }


    }
}

