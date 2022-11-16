
using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class ProductoRepository : EfRepository<Producto,Guid>, IProductoRepository
{
    public ProductoRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }

    
    public async Task<ICollection<Producto>>  ListaProductoIdAsync(IList<Guid> listaIds)//, bool asNoTracking = true)
    {
        var consulta = GetAllIncluding(x => x.Marca, x => x.TipoProducto);

        consulta = consulta.Where(x=>listaIds.Contains(x.Id));
        return await consulta.ToListAsync();
    }
    
    //Otra forma de hacerle lo de arriba es:
    /*
    public async Task<ICollection<Producto>> ListaProductoIdAsync(IList<int> indices)
    {
        ICollection<Producto> lista = new List<Producto>();
        
        foreach (var item in indices)
        {
            var productoEntidad = await this.GetByIdAsync(item);
            if (productoEntidad != null)
            {
                lista.Add(productoEntidad);
            }
        }
         return lista;

    }
    */


    public async Task<bool> ExisteNombre(string nombre) {

        var resultado = await this._context.Set<Producto>()
                       .AnyAsync(x => x.Nombre.ToUpper() == nombre.ToUpper());

        return resultado;
    }

    public async Task<bool> ExisteNombre(string nombre, Guid idExcluir)  {

        var query =  this._context.Set<Producto>()
                       .Where(x => x.Id != idExcluir)
                       .Where(x => x.Nombre.ToUpper() == nombre.ToUpper())
                       ;

        var resultado = await query.AnyAsync();

        return resultado;
    }
    

    
}
