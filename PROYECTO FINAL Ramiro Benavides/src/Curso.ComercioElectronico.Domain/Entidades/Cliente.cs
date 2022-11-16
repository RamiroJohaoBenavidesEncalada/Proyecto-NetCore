using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Curso.ComercioElectronico.Domain;


public class Cliente
{

    public Guid Id{get;set;}

    public string Nombres{get;set;}
    
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


