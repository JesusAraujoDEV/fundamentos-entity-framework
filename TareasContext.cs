using fundamentos_entity_framework.Models;
using Microsoft.EntityFrameworkCore;

namespace fundamentos_entity_framework;

public class TareasContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria { CategoriaId = Guid.Parse("425c86ed-fcce-4946-87f6-1efd10f5f7d8"), Nombre = "Actividades Pendientes", Peso = 20, Descripcion = "Tareas que aún no se han comenzado." });
        categoriasInit.Add(new Categoria { CategoriaId = Guid.Parse("57e4605e-0baa-49bd-869a-90df45d1147c"), Nombre = "Actividades Completadas", Peso = 10, Descripcion = "Tareas que se han completado." });
        categoriasInit.Add(new Categoria { CategoriaId = Guid.Parse("b2810791-f000-4ab6-a6a6-c186d82dbcf0"), Nombre = "Actividades en Progreso", Peso = 15, Descripcion = "Tareas que están en progreso." });

        modelBuilder.Entity<Categoria>(categoria =>
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);

            categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);

            categoria.Property(p => p.Descripcion).IsRequired(false);

            categoria.Property(p => p.Peso);

            categoria.HasData(categoriasInit);
        });

        List<Tarea> tareasInit = new List<Tarea>();
        // Ejemplos corregidos (asegúrate de generar tus propios GUIDs válidos y únicos si es necesario)
        tareasInit.Add(new Tarea() {
            TareaId = Guid.Parse("09bbbf53-ff34-4594-920c-59b381b2258a"),
            CategoriaId = Guid.Parse("425c86ed-fcce-4946-87f6-1efd10f5f7d8"),
            PrioridadTarea = Prioridad.Media,
            Titulo = "Revisar el informe mensual",
            Descripcion = "Revisar y aprobar el informe mensual de ventas.",
            // CAMBIO AQUÍ: Usar una fecha y hora estática
            FechaCreacion = new DateTime(2025, 7, 17, 10, 0, 0) // Ejemplo: 17 de Julio de 2025, 10:00:00 AM
        });
        tareasInit.Add(new Tarea() {
            TareaId = Guid.Parse("67c032b1-d7e0-4219-9ad7-273a4257b735"),
            CategoriaId = Guid.Parse("57e4605e-0baa-49bd-869a-90df45d1147c"),
            PrioridadTarea = Prioridad.Alta,
            Titulo = "Actualizar el sitio web",
            Descripcion = "Actualizar el contenido del sitio web con las últimas noticias.",
            // CAMBIO AQUÍ: Usar otra fecha y hora estática
            FechaCreacion = new DateTime(2025, 7, 16, 14, 30, 0) // Ejemplo: 16 de Julio de 2025, 02:30:00 PM
        });

        modelBuilder.Entity<Tarea>(tarea =>
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(p => p.TareaId);

            tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);

            tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);

            tarea.Property(p => p.Descripcion).IsRequired(false);

            tarea.Property(p => p.PrioridadTarea);

            tarea.Property(p => p.FechaCreacion);

            tarea.Ignore(p => p.Resumen);
            
            tarea.HasData(tareasInit);

        });

    }

}