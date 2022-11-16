using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;
public class OrdenItemDto {

    
    public Guid Id {get;set; }
    public Guid ProductoId {get; set;}
    public virtual string Producto { get; set; }
    public Guid OrdenId {get; set;}
    public long Cantidad {get;set;}
    public decimal Precio {get;set;}
    public string? Observaciones { get;set;}
}