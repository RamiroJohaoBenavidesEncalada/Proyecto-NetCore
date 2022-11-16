using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public class ClienteCrearActualizarDto
{
 
    public string? Nombres {get;set;}
    public string Cedula{get;set;}
    public string? Direccion{get;set;}
    
    public string? Telefono{get;set;}
    
    [EmailAddress]
    public string? Email{get;set;}
    public string? Ciudad{get;set;}
    public decimal PorcentajeDescuento{get;set;}

    public string Usuario{get;set;}
    public string Contraseña{get;set;}
    
}
