﻿// <auto-generated />
using System;
using GrupoEstudos.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GrupoEstudos.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GrupoEstudos.Domain.Entities.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("GrupoEstudos.Domain.Entities.Estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("Sigla");

                    b.HasKey("Id");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("GrupoEstudos.Domain.Entities.Municipio", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.ToTable("Municipios");
                });

            modelBuilder.Entity("GrupoEstudos.Domain.Entities.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CPF")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Telefone")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.HasKey("Id");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("GrupoEstudos.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("GrupoEstudos.Domain.Entities.Endereco", b =>
                {
                    b.HasOne("GrupoEstudos.Domain.Entities.Pessoa", "Pessoa")
                        .WithMany("Enderecos")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("GrupoEstudos.Domain.Entities.Municipio", b =>
                {
                    b.HasOne("GrupoEstudos.Domain.Entities.Estado", "Estado")
                        .WithMany("Municipios")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estado");
                });

            modelBuilder.Entity("GrupoEstudos.Domain.Entities.Estado", b =>
                {
                    b.Navigation("Municipios");
                });

            modelBuilder.Entity("GrupoEstudos.Domain.Entities.Pessoa", b =>
                {
                    b.Navigation("Enderecos");
                });
#pragma warning restore 612, 618
        }
    }
}
