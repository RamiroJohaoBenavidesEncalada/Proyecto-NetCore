using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class CarritoDto
{
    public Guid Id {get;set; }
    public Guid ClienteId {get;set;}
    public virtual string Cliente {get;set;}
    public virtual ICollection<CarritoItemDto> Items {get;set;}
    public decimal Total {get;set;}
    public string? Observaciones { get;set;}
    public CarritoEstado Estado {get;set;}
 
}
