﻿// <auto-generated />
using System;
using DB1.Desafio.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DB1.Desafio.Infra.Migrations
{
    [DbContext(typeof(Db1DataContext))]
    [Migration("20240825024746_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DB1.Desafio.Domain.Entities.Cargo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Cargo", (string)null);
                });

            modelBuilder.Entity("DB1.Desafio.Domain.Entities.Empresa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataFundacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Empresa", (string)null);
                });

            modelBuilder.Entity("DB1.Desafio.Domain.Entities.Funcionario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataContratacao")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Funcionario", (string)null);
                });

            modelBuilder.Entity("DB1.Desafio.Domain.Entities.FuncionarioCargo", b =>
                {
                    b.Property<Guid?>("FuncionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CargoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataVinculo")
                        .HasColumnType("datetime2");

                    b.HasKey("FuncionarioId", "CargoId");

                    b.HasIndex("CargoId");

                    b.ToTable("FuncionarioCargo", (string)null);
                });

            modelBuilder.Entity("DB1.Desafio.Domain.Entities.Empresa", b =>
                {
                    b.OwnsOne("DB1.Core.ValueObjects.Cnpj", "Cnpj", b1 =>
                        {
                            b1.Property<Guid>("EmpresaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Numero")
                                .IsRequired()
                                .HasMaxLength(14)
                                .IsUnicode(false)
                                .HasColumnType("varchar(14)")
                                .HasColumnName("Cnpj");

                            b1.HasKey("EmpresaId");

                            b1.ToTable("Empresa");

                            b1.WithOwner()
                                .HasForeignKey("EmpresaId");
                        });

                    b.Navigation("Cnpj")
                        .IsRequired();
                });

            modelBuilder.Entity("DB1.Desafio.Domain.Entities.Funcionario", b =>
                {
                    b.HasOne("DB1.Desafio.Domain.Entities.Empresa", "Empresa")
                        .WithMany("Funcionarios")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsOne("DB1.Core.ValueObjects.Cpf", "Cpf", b1 =>
                        {
                            b1.Property<Guid>("FuncionarioId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Numero")
                                .IsRequired()
                                .HasMaxLength(11)
                                .IsUnicode(false)
                                .HasColumnType("varchar(11)")
                                .HasColumnName("Cpf");

                            b1.HasKey("FuncionarioId");

                            b1.ToTable("Funcionario");

                            b1.WithOwner()
                                .HasForeignKey("FuncionarioId");
                        });

                    b.Navigation("Cpf")
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("DB1.Desafio.Domain.Entities.FuncionarioCargo", b =>
                {
                    b.HasOne("DB1.Desafio.Domain.Entities.Cargo", "Cargo")
                        .WithMany("FuncionarioCargos")
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DB1.Desafio.Domain.Entities.Funcionario", "Funcionario")
                        .WithMany("FuncionarioCargos")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("DB1.Desafio.Domain.Entities.Cargo", b =>
                {
                    b.Navigation("FuncionarioCargos");
                });

            modelBuilder.Entity("DB1.Desafio.Domain.Entities.Empresa", b =>
                {
                    b.Navigation("Funcionarios");
                });

            modelBuilder.Entity("DB1.Desafio.Domain.Entities.Funcionario", b =>
                {
                    b.Navigation("FuncionarioCargos");
                });
#pragma warning restore 612, 618
        }
    }
}
