using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Curso.ComercioElectronico.Infraestructure;


public class ComercioElectronicoDbContext : DbContext, IUnitOfWork
{
    
    //Agregar sus entidades
    public DbSet<Marca> Marcas { get; set; }

    public DbSet<TipoProducto> TipoProductos { get; set; }
    
    public DbSet<Producto> Productos { get; set; }
   
    public DbSet<Cliente> Clientes {get;set;}
    public DbSet<Orden> Ordenes {get;set;}

    public DbSet<Carrito> Carritos {get;set;}

    public DbSet<MetodoEntregaCompra> MetodoEntregaCompras {get;set;}

    public string DbPath { get; set; }

    public ComercioElectronicoDbContext(DbContextOptions<ComercioElectronicoDbContext> options) : base(options)
    {
    } 

    /*
    Las configuraciones de mis entidades con el modelBuilder (lo de las Tablas de Entity).
    Si se quiere ajusta el modelo se lo puede hacer en OnModelCreating.
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>()
                                .Property(e=>e.Precio)
                                .HasConversion<double>();
        //TODO: Conversion. Ejemplos. Estado??
        modelBuilder.Entity<OrdenItem>()
                                .Property(e=>e.Precio)
                                .HasConversion<double>();
    }

}



