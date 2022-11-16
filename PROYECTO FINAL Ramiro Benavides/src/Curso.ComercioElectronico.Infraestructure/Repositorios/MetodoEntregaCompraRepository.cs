
using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class MetodoEntregaCompraRepository : EfRepository<MetodoEntregaCompra,string>, IMetodoEntregaCompraRepository
{
    public MetodoEntregaCompraRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }

    public async Task<bool> ExisteNombre(string nombre) {

        var resultado = await this._context.Set<Marca>()
                       .AnyAsync(x => x.Nombre.ToUpper() == nombre.ToUpper());

        return resultado;
    }

    public async Task<bool> ExisteNombre(string nombre, string idExcluir)  {

        var query =  this._context.Set<Marca>()
                       .Where(x => !(x.Id).Equals(idExcluir))
                       .Where(x => x.Nombre.ToUpper() == nombre.ToUpper())
                       ;

        var resultado = await query.AnyAsync();

        return resultado;
    }

    

    
}