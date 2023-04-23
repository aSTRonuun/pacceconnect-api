using Domain.ArticulatorDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ArticulatorData
{
    public class ArticulatorConfiguration : IEntityTypeConfiguration<Articulator>
    {
        public void Configure(EntityTypeBuilder<Articulator> builder)
        {
            builder.OwnsOne(x => x.StudentId)
                .Property(x => x.Course);
            builder.OwnsOne(x => x.StudentId)
                .Property(x => x.Course);
        }
    }
}
