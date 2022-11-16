using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain;

public class Orden
{
    public Orden(Guid id)
    {
        this.Id = id;
    }
    public Guid Id {get;set; }
    public Guid ClienteId {get;set;}
    public virtual Cliente? Cliente {get;set;}
    public virtual ICollection<OrdenItem> Items {get;set;} = new List<OrdenItem>();
    public DateTime Fecha {get;set;}
    public decimal Total {get;set;}
    public string? Observaciones { get;set;}
    public OrdenEstado Estado {get;set;}
    public string? MetodoEntregaCompraId {get;set;}
    public virtual MetodoEntregaCompra MetodoEntregaCompra{get;set;}  
    public void AgregarItem(OrdenItem item){
       
        item.Orden = this;
        Items.Add(item); 
    }
      
}

public enum OrdenEstado{
    Anulada=0,
    Registrada =1,
    Procesada =2,
    Entregada =3
}