namespace Curso.ComercioElectronico.Application;

public class CarritoItemDto {

    public Guid Id {get;set; }
    public Guid ProductoId {get; set;}
    public virtual string Producto { get; set; }
    public Guid CarritoId {get; set;}
    public long Cantidad {get;set;}
    public decimal Precio {get;set;}
    public string? Observaciones { get;set;}
}