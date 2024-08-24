using DB1.Desafio.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using DB1.Desafio.Domain.Entities;

namespace DB1.Desafio.Infra.EntityMaps
{
    internal class FuncionarioMap : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable(nameof(Funcionario));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(150);

            builder.Property(p => p.Cpf)
                .IsUnicode(false)
                .HasMaxLength(11);

            builder.Property(p => p.Status)
               .HasConversion(new EnumToStringConverter<Status>())
               .IsUnicode(false)
               .HasMaxLength(30);

            builder.HasOne(p => p.Empresa)
                .WithMany(p => p.Funcionarios)
                .HasForeignKey(p => p.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Cargos)
                .WithMany(p => p.Funcionarios)
                .UsingEntity<FuncionarioCargo>(
                    e => e.HasOne(x => x.Cargo).WithMany(),
                    e => e.HasOne(x => x.Funcionario).WithMany(),
                    join =>
                    {
                        join.ToTable($"{nameof(FuncionarioCargo)}");
                        join.HasKey($"{nameof(Funcionario)}Id", $"{nameof(Cargo)}Id");
                    });

            builder.HasQueryFilter(p => p.Status != Status.Removido);
        }
    }
}
