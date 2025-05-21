using fundamentos_entity_framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>("Server=localhost,1433;Database=TareasDB;User Id=sa;Password=wuejejejAURA777;MultipleActiveResultSets=true;TrustServerCertificate=True;");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContexto) => {
    dbContexto.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContexto.Database.IsInMemory());
});

app.Run();
