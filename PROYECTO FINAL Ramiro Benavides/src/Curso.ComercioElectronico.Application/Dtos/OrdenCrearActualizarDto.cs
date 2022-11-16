using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class OrdenCrearDto
{
    
    //public Guid ClienteId {get;set;}
    //public virtual ICollection<OrdenItemCrearActualizarDto> Items {get;set;}
    public DateTime Fecha {get;set;}
    public string? Observaciones { get;set;}
    public string MetodoEntregaCompraId {get;set;}
  
} 

public class OrdenActualizarDto
{
    public OrdenEstado Estado {get;set;}
    public string? Observaciones { get;set;}
    //public string MetodoEntregaCompraId {get;set;}
}  



public class OrdenItemCrearActualizarDto {
    public Guid ProductoId {get; set;} 
    public long Cantidad {get;set;}
    public string? Observaciones { get;set;}
}



public class CarritoCrearDto
{
    
    public Guid ClienteId {get;set;}
    public virtual ICollection<OrdenItemCrearActualizarDto> Items {get;set;}
    public string? Observaciones { get;set;}
  
} 

public class CarritoActualizarDto
{
    public CarritoEstado Estado {get;set;}
    public string? Observaciones { get;set;}
}  

public class CarritoItemCrearActualizarDto {
    public Guid ProductoId {get; set;} 
    public long Cantidad {get;set;}
    public string? Observaciones { get;set;}
}