using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class ProductoCrearActualizarDto
{
   

    public string Nombre {get;set;}

    public decimal Precio {get;set;}

    public string? Observaciones {get;set;}

    public DateTime? Caducidad {get;set;}

    public string MarcaId {get;set;}
 


    public string TipoProductoId {get;set;}

     
}  

