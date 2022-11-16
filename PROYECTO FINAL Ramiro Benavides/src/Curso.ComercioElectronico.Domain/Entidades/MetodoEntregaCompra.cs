using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain;

public class MetodoEntregaCompra
{
    public string Id {get;set;}

    public string MetodoEntrega {get;set;}

    public string? Observaciones{get;set;}
    public decimal CostoEntrega{get;set;}

    public string? DireccionEntrega{get;set;}

}





