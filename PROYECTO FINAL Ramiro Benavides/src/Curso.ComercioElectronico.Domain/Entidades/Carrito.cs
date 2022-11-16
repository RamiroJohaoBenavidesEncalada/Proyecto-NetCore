namespace Curso.ComercioElectronico.Domain;

public class Carrito
{
    public Carrito(Guid id)
    {
        this.Id = id;
    }
    public Guid Id {get;set; }

    public Guid ClienteId {get;set;}
    public virtual Cliente? Cliente {get;set;}
    public virtual ICollection<CarritoItem> Items {get;set;} = new List<CarritoItem>();
    public decimal Total {get;set;}
    public string? Observaciones { get;set;}

    public CarritoEstado Estado {get;set;}
    public void AgregarItem(CarritoItem item){
       
        item.Carrito = this;
        Items.Add(item); 
    }
}

public enum CarritoEstado{
    Vacío=0,
    Lleno =1
    
}
