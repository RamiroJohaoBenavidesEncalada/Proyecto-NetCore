using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class OrdenDto
{
    public Guid Id {get;set; }
    public Guid ClienteId {get;set;}
    public virtual string Cliente {get;set;}
    public virtual ICollection<OrdenItemDto> Items {get;set;}
    public DateTime Fecha {get;set;}
    public decimal Total {get;set;}
    public string? Observaciones { get;set;}
    public OrdenEstado Estado {get;set;}
    public string? MetodoEntregaCompraId{get;set;} 
    public string MetodoEntregaCompra {get;set;}
}
