
using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class TipoProductoRepository : EfRepository<TipoProducto,string>, ITipoProductoRepository
{
    public TipoProductoRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }

    public async Task<bool> ExisteNombre(string nombre) {

        var resultado = await this._context.Set<TipoProducto>()
                       .AnyAsync(x => x.Nombre.ToUpper() == nombre.ToUpper());

        return resultado;
    }

    public async Task<bool> ExisteNombre(string nombre, string idExcluir)  {

        var query =  this._context.Set<TipoProducto>()
                       .Where(x => !(x.Id).Equals (idExcluir))
                       .Where(x => x.Nombre.ToUpper() == nombre.ToUpper())
                       ;

        var resultado = await query.AnyAsync();

        return resultado;
    }

    
}