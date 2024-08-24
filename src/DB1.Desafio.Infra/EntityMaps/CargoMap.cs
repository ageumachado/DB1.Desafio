using DB1.Desafio.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using DB1.Desafio.Domain.Entities;

namespace DB1.Desafio.Infra.EntityMaps
{
    internal class CargoMap : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.ToTable(nameof(Cargo));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(150);

            builder.Property(p => p.Status)
               .HasConversion(new EnumToStringConverter<Status>())
               .IsUnicode(false)
               .HasMaxLength(30);

            builder.HasQueryFilter(p => p.Status != Status.Removido);
        }
    }
}
