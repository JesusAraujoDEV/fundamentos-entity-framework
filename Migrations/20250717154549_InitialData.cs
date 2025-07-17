using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace fundamentos_entity_framework.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre", "Peso" },
                values: new object[,]
                {
                    { new Guid("425c86ed-fcce-4946-87f6-1efd10f5f7d8"), "Tareas que aún no se han comenzado.", "Actividades Pendientes", 20 },
                    { new Guid("57e4605e-0baa-49bd-869a-90df45d1147c"), "Tareas que se han completado.", "Actividades Completadas", 10 },
                    { new Guid("b2810791-f000-4ab6-a6a6-c186d82dbcf0"), "Tareas que están en progreso.", "Actividades en Progreso", 15 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "TareaId", "CategoriaId", "Descripcion", "FechaCreacion", "PrioridadTarea", "Titulo" },
                values: new object[,]
                {
                    { new Guid("09bbbf53-ff34-4594-920c-59b381b2258a"), new Guid("425c86ed-fcce-4946-87f6-1efd10f5f7d8"), "Revisar y aprobar el informe mensual de ventas.", new DateTime(2025, 7, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, "Revisar el informe mensual" },
                    { new Guid("67c032b1-d7e0-4219-9ad7-273a4257b735"), new Guid("57e4605e-0baa-49bd-869a-90df45d1147c"), "Actualizar el contenido del sitio web con las últimas noticias.", new DateTime(2025, 7, 16, 14, 30, 0, 0, DateTimeKind.Unspecified), 2, "Actualizar el sitio web" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("b2810791-f000-4ab6-a6a6-c186d82dbcf0"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("09bbbf53-ff34-4594-920c-59b381b2258a"));

            migrationBuilder.DeleteData(
                table: "Tarea",
                keyColumn: "TareaId",
                keyValue: new Guid("67c032b1-d7e0-4219-9ad7-273a4257b735"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("425c86ed-fcce-4946-87f6-1efd10f5f7d8"));

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "CategoriaId",
                keyValue: new Guid("57e4605e-0baa-49bd-869a-90df45d1147c"));

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
