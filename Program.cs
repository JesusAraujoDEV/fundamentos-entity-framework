using fundamentos_entity_framework;
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

Console.WriteLine("Cadena de conexi�n usada: " + directConnectionString);

var app = builder.Build();

app.MapGet("/", () =>
{
    Console.WriteLine("Cadena de conexi�n: " + directConnectionString);
});

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContexto) => {
    dbContexto.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContexto.Database.IsInMemory());
});

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContexto) => {
    var tareas = await dbContexto.Tareas.Include(p=>p.Categoria).Where(p=>p.PrioridadTarea == fundamentos_entity_framework.Models.Prioridad.Alta).ToListAsync();
    return Results.Ok(tareas);
}); 

app.Run();
