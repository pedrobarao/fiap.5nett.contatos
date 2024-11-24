﻿// <auto-generated />
using System;
using Contatos.Cadastro.Api.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Contatos.Cadastro.Api.Infra.Data.Migrations
{
    [DbContext(typeof(ContatoDbContext))]
    [Migration("20241118115739_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Contatos.SharedKernel.Entities.Contato", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Contatos", (string)null);
                });

            modelBuilder.Entity("Contatos.SharedKernel.Entities.Contato", b =>
                {
                    b.OwnsOne("Contatos.SharedKernel.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("ContatoId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Endereco")
                                .IsRequired()
                                .HasMaxLength(254)
                                .HasColumnType("varchar(254)")
                                .HasColumnName("Email");

                            b1.HasKey("ContatoId");

                            b1.ToTable("Contatos");

                            b1.WithOwner()
                                .HasForeignKey("ContatoId");
                        });

                    b.OwnsOne("Contatos.SharedKernel.ValueObjects.Nome", "Nome", b1 =>
                        {
                            b1.Property<Guid>("ContatoId")
                                .HasColumnType("uuid");

                            b1.Property<string>("PrimeiroNome")
                                .IsRequired()
                                .HasColumnType("varchar(60)")
                                .HasColumnName("PrimeiroNome");

                            b1.Property<string>("Sobrenome")
                                .IsRequired()
                                .HasColumnType("varchar(60)")
                                .HasColumnName("Sobrenome");

                            b1.HasKey("ContatoId");

                            b1.ToTable("Contatos");

                            b1.WithOwner()
                                .HasForeignKey("ContatoId");
                        });

                    b.OwnsMany("Contatos.SharedKernel.ValueObjects.Telefone", "Telefones", b1 =>
                        {
                            b1.Property<Guid>("ContatoId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Ddd")
                                .HasColumnType("int");

                            b1.Property<string>("Numero")
                                .HasColumnType("varchar(10)");

                            b1.Property<int>("Tipo")
                                .HasColumnType("int");

                            b1.HasKey("ContatoId", "Ddd", "Numero", "Tipo");

                            b1.ToTable("Telefones", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ContatoId");
                        });

                    b.Navigation("Email");

                    b.Navigation("Nome")
                        .IsRequired();

                    b.Navigation("Telefones");
                });
#pragma warning restore 612, 618
        }
    }
}
