using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DB1.Desafio.Domain.Entities;

namespace DB1.Desafio.Infra.EntityMaps
{
    internal class FuncionarioCargoMap : IEntityTypeConfiguration<FuncionarioCargo>
    {
        public void Configure(EntityTypeBuilder<FuncionarioCargo> builder)
        {
            builder.ToTable(nameof(FuncionarioCargo));

            builder.HasKey(p =>new { p.FuncionarioId, p.CargoId});

            builder.HasOne(p => p.Funcionario)
                .WithMany(p => p.FuncionarioCargos)
                .HasForeignKey(p => p.FuncionarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Cargo)
                .WithMany(p => p.FuncionarioCargos)
                .HasForeignKey(p => p.CargoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
