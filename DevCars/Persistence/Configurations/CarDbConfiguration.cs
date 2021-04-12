using DevCars.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevCars.Persistence.Configurations
{
    public class CarDbConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
             .HasKey(c => c.Id);

            builder
                 .Property(c => c.Brand)
                //.IsRequired()
                //.HasColumnName("Marca")
                .HasColumnType("VARCHAR(100)")
                .HasDefaultValue("PADRAO")
                .HasMaxLength(100);

            builder
                .Property(c => c.ProductionDate)
                .HasDefaultValueSql("getdate()");

            builder
               .ToTable("tb_Car");
        }
    }
}





