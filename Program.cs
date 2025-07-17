using fundamentos_entity_framework;
using fundamentos_entity_framework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

var builder = WebApplication.CreateBuilder(args);

var dbPassword = Environment.GetEnvironmentVariable("SA_PASSWORD");
var directConnectionString = $"Server=localhost,1433;Database=TareasDB;User Id=sa;Password={dbPassword};MultipleActiveResultSets=true;TrustServerCertificate=True;";

builder.Services.AddSqlServer<TareasContext>(directConnectionString);

Console.WriteLine("Cadena de conexión usada: " + directConnectionString);

var app = builder.Build();

app.MapGet("/", () =>
{
    Console.WriteLine("Cadena de conexión: " + directConnectionString);
    return Results.Ok("API de Tareas funcionando!"); // Buena práctica: devolver un resultado OK
});

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContexto) => {
    dbContexto.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContexto.Database.IsInMemory());
});

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContexto) =>
{
    var tareas = await dbContexto.Tareas.Include(p => p.Categoria).ToListAsync();
    return Results.Ok(tareas);
});

app.MapGet("/api/tareas/{id}", async ([FromServices] TareasContext dbContexto, [FromRoute] Guid id) =>
{
    var tarea = await dbContexto.Tareas.Include(p => p.Categoria).FirstOrDefaultAsync(t => t.TareaId == id);
    if (tarea == null)
    {
        return Results.NotFound($"No se encontró ninguna tarea con el ID: {id}");
    }
    return Results.Ok(tarea);
});

app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContexto, [FromBody] Tarea tarea) =>
{
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;
    await dbContexto.Tareas.AddAsync(tarea);
    await dbContexto.SaveChangesAsync();
    return Results.Created($"/api/tareas/{tarea.TareaId}", tarea);
});

// ==========================================================
// REEMPLAZO DE PUT POR PATCH (Actualización Parcial)
// ==========================================================

app.MapPatch("/api/tareas/{id}", async ([FromServices] TareasContext dbContexto, [FromRoute] Guid id, [FromBody] TareaPatchDto tareaPatchDto) =>
{
    // 1. Buscar la tarea existente en la base de datos
    // Usa FindAsync para buscar por clave primaria directamente si solo necesitas la entidad.
    // Si necesitas incluir relaciones para devolverla completa, usa FirstOrDefaultAsync(t => t.TareaId == id) con .Include().
    var tareaExistente = await dbContexto.Tareas.FirstOrDefaultAsync(t => t.TareaId == id);

    // 2. Si la tarea no existe, devolver 404 Not Found
    if (tareaExistente == null)
    {
        return Results.NotFound($"No se encontró ninguna tarea con el ID: {id}");
    }

    // 3. Aplicar las modificaciones parciales usando el DTO
    // Solo actualizamos las propiedades si el cliente envió un valor (es decir, no es null en el DTO).

    if (tareaPatchDto.Titulo != null)
    {
        tareaExistente.Titulo = tareaPatchDto.Titulo;
    }

    if (tareaPatchDto.Descripcion != null)
    {
        tareaExistente.Descripcion = tareaPatchDto.Descripcion;
    }

    // Para PrioridadTarea, usamos .HasValue para ver si el enum nullable fue proporcionado.
    if (tareaPatchDto.PrioridadTarea.HasValue)
    {
        tareaExistente.PrioridadTarea = tareaPatchDto.PrioridadTarea.Value;
    }
    
    // Si quieres permitir cambiar la Categoría de la tarea vía PATCH:
    if (tareaPatchDto.CategoriaId.HasValue)
    {
        tareaExistente.CategoriaId = tareaPatchDto.CategoriaId.Value;
    }

    // NOTA: TareaId y FechaCreacion no se actualizan en un PATCH típico.

    // 4. Guardar los cambios
    // EF Core rastrea automáticamente los cambios en 'tareaExistente' si la obtuviste del DbContext.
    // No necesitas llamar a dbContexto.Tareas.Update(tareaExistente); explícitamente en este escenario.
    await dbContexto.SaveChangesAsync();

    // 5. Devolver un 200 OK con la tarea actualizada (o 204 No Content si no quieres devolver el cuerpo)
    return Results.Ok($"Se actualizó la tarea de nombre: {tareaExistente.Titulo}");
});

app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContexto, [FromRoute] Guid id) =>
{
    var tarea = await dbContexto.Tareas.FindAsync(id);
    if (tarea == null)
    {
        return Results.NotFound($"No se encontró ninguna tarea con el ID: {id}");
    }

    dbContexto.Tareas.Remove(tarea);
    await dbContexto.SaveChangesAsync();
    return Results.Ok($"Se eliminó la tarea de nombre: {tarea.Titulo}");
});

app.Run();