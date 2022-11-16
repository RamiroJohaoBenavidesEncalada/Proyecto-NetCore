using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class ProductoDto
{

    public Guid Id {get;set;}


    public string Nombre {get;set;}

    public decimal Precio {get;set;}

    public string? Observaciones {get;set;}

    public DateTime? Caducidad {get;set;}

    public string MarcaId {get;set;}

    public string  Marca {get; set; }

    public string TipoProductoId {get;set;}

    public string TipoProducto {get;set;}

}

