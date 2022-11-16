namespace Curso.ComercioElectronico.Domain;

public class CarritoItem {

    public CarritoItem(Guid id)
    {
        this.Id = id;
    }
    public Guid Id {get;set; }
    public Guid ProductoId {get; set;}
    public virtual Producto Producto { get; set; }
    public Guid CarritoId {get; set;}
    public virtual Carrito Carrito { get; set; }
    public long Cantidad {get;set;}
    public decimal Precio {get;set;}
    public string? Observaciones { get;set;}
}