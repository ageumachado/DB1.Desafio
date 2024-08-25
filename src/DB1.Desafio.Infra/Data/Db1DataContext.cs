using DB1.Core.Data;
using DB1.Core.Mediator;
using DB1.Core.Messages;
using DB1.Desafio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;
using DB1.Desafio.Infra.Extensions;

namespace DB1.Desafio.Infra.Data
{
    internal class Db1DataContext : DbContext, IUnitOfWork
    {
        #region DbSet
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<FuncionarioCargo> FuncionarioCargos { get; set; }
        #endregion

        private readonly IMediatorHandler _mediatorHandler;

        public Db1DataContext(
            DbContextOptions<Db1DataContext> options,
            IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Db1DataContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder
                .LogTo(Console.WriteLine)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
#endif

            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<string>()
                .AreUnicode(false)
                .HaveMaxLength(150);

            base.ConfigureConventions(configurationBuilder);
        }

        public async Task<bool> Commit()
        {
            var sucesso = await SaveChangesAsync() > 0;
            if (sucesso) await _mediatorHandler.PublicarEventos(this);

            return sucesso;
        }
    }
}
