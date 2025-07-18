﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using fundamentos_entity_framework;

#nullable disable

namespace fundamentos_entity_framework.Migrations
{
    [DbContext(typeof(TareasContext))]
    [Migration("20250717154549_InitialData")]
    partial class InitialData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("fundamentos_entity_framework.Models.Categoria", b =>
                {
                    b.Property<Guid>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Peso")
                        .HasColumnType("int");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categoria", (string)null);

                    b.HasData(
                        new
                        {
                            CategoriaId = new Guid("425c86ed-fcce-4946-87f6-1efd10f5f7d8"),
                            Descripcion = "Tareas que aún no se han comenzado.",
                            Nombre = "Actividades Pendientes",
                            Peso = 20
                        },
                        new
                        {
                            CategoriaId = new Guid("57e4605e-0baa-49bd-869a-90df45d1147c"),
                            Descripcion = "Tareas que se han completado.",
                            Nombre = "Actividades Completadas",
                            Peso = 10
                        },
                        new
                        {
                            CategoriaId = new Guid("b2810791-f000-4ab6-a6a6-c186d82dbcf0"),
                            Descripcion = "Tareas que están en progreso.",
                            Nombre = "Actividades en Progreso",
                            Peso = 15
                        });
                });

            modelBuilder.Entity("fundamentos_entity_framework.Models.Tarea", b =>
                {
                    b.Property<Guid>("TareaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("PrioridadTarea")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("TareaId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Tarea", (string)null);

                    b.HasData(
                        new
                        {
                            TareaId = new Guid("09bbbf53-ff34-4594-920c-59b381b2258a"),
                            CategoriaId = new Guid("425c86ed-fcce-4946-87f6-1efd10f5f7d8"),
                            Descripcion = "Revisar y aprobar el informe mensual de ventas.",
                            FechaCreacion = new DateTime(2025, 7, 17, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            PrioridadTarea = 1,
                            Titulo = "Revisar el informe mensual"
                        },
                        new
                        {
                            TareaId = new Guid("67c032b1-d7e0-4219-9ad7-273a4257b735"),
                            CategoriaId = new Guid("57e4605e-0baa-49bd-869a-90df45d1147c"),
                            Descripcion = "Actualizar el contenido del sitio web con las últimas noticias.",
                            FechaCreacion = new DateTime(2025, 7, 16, 14, 30, 0, 0, DateTimeKind.Unspecified),
                            PrioridadTarea = 2,
                            Titulo = "Actualizar el sitio web"
                        });
                });

            modelBuilder.Entity("fundamentos_entity_framework.Models.Tarea", b =>
                {
                    b.HasOne("fundamentos_entity_framework.Models.Categoria", "Categoria")
                        .WithMany("Tareas")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("fundamentos_entity_framework.Models.Categoria", b =>
                {
                    b.Navigation("Tareas");
                });
#pragma warning restore 612, 618
        }
    }
}
