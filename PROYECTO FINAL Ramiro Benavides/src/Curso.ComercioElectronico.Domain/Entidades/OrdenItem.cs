using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain;

public class OrdenItem {
    public OrdenItem(Guid id)
    {
        Id = id;
    }

    public Guid Id {get;set; }
    public Guid ProductoId {get; set;}
    public virtual Producto Producto { get; set; }
    public Guid OrdenId {get; set;}
    public virtual Orden Orden { get; set; }
    public long Cantidad {get;set;}
    public decimal Precio {get;set;}
    public string? Observaciones { get;set;}
}

