// Models/TareaPatchDto.cs
using System;
using fundamentos_entity_framework.Models; // Asegúrate de que este using sea correcto para Prioridad

namespace fundamentos_entity_framework.Models;

public class TareaPatchDto
{
    // Las propiedades se hacen 'nullable' (?) para saber si el cliente las envió.
    // Si el cliente no envía una propiedad, su valor aquí será null.
    public Guid? CategoriaId { get; set; }
    public string? Titulo { get; set; }
    public string? Descripcion { get; set; }
    public Prioridad? PrioridadTarea { get; set; }
    // No incluyas TareaId ni FechaCreacion, ya que no deberían ser modificables vía PATCH.
}