using DB1.Desafio.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB1.Desafio.Domain.Entities;

namespace DB1.Desafio.Infra.EntityMaps
{
    internal class EmpresaMap : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable(nameof(Empresa));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(150);

            builder.OwnsOne(p => p.Cnpj, documento =>
            {
                documento.Property("Numero")
                    .IsUnicode(false)
                    .HasColumnName("Cnpj")
                    .HasMaxLength(14);
            });

            builder.Property(p => p.Status)
               .HasConversion(new EnumToStringConverter<StatusEmpresa>())
               .IsUnicode(false)
               .HasMaxLength(30);
            
            builder.HasQueryFilter(p => p.Status != StatusEmpresa.Removido);
        }
    }
}
