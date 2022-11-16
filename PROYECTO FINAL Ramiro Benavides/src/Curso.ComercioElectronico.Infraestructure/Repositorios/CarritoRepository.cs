
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Infraestructure;

public class CarritoRepository : EfRepository<Carrito,Guid>, ICarritoRepository
{
    public CarritoRepository(ComercioElectronicoDbContext context) : base(context)
    {
    } 
}