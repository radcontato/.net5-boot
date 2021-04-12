using DevCars.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevCars.Persistence.Configurations
{
    public class ExtraOrderItemDbConfiguration : IEntityTypeConfiguration<ExtraOrderItem>
    {
        public void Configure(EntityTypeBuilder<ExtraOrderItem> builder)
        {
            builder
                 .HasKey(e => e.Id);         

            builder
                   .ToTable("tb_ExtraOrderItem");
        }
    }
}
