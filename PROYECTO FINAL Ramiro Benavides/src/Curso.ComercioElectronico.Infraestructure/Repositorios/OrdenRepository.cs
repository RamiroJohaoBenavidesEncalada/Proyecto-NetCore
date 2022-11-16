
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Infraestructure;

public class OrdenRepository : EfRepository<Orden,Guid>, IOrdenRepository
{
    public OrdenRepository(ComercioElectronicoDbContext context) : base(context)
    {
    } 
}
