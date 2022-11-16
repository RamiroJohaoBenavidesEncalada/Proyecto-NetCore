using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Curso.ComercioElectronico.Domain;


public class Producto
{
    /*
    public Producto(Guid id)
    {
        this.Id = id;
    }
    */
    public Guid Id {get;set;}

    public string Nombre {get;set;}

    public decimal Precio {get;set;}

    public string? Observaciones {get;set;}

    public DateTime? Caducidad {get;set;}
    
    public string? MarcaId {get;set;}

    public virtual Marca Marca {get; set; }


    public string? TipoProductoId {get;set;}

    public virtual TipoProducto TipoProducto {get;set;}

    
}




