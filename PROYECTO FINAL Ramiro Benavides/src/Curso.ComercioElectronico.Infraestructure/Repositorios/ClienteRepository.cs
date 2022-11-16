
using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class ClienteRepository : EfRepository<Cliente,Guid>, IClienteRepository
{
    public ClienteRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }

    public async Task<bool> ExisteNombre(string nombre) {

        var resultado = await this._context.Set<Cliente>()
                       .AnyAsync(x => x.Nombres.ToUpper() == nombre.ToUpper());

        return resultado;
    }

    public async Task<bool> ExisteNombre(string nombre, Guid idExcluir)  {

        var query =  this._context.Set<Cliente>()
                       .Where(x => x.Id != idExcluir)
                       .Where(x => x.Nombres.ToUpper() == nombre.ToUpper())
                       ;

        var resultado = await query.AnyAsync();

        return resultado;
    }

    
}
