namespace Curso.ComercioElectronico.Application;

public interface ICarritoAppService
{
    Task<CarritoDto> GetByIdAsync(Guid id);

    ListaPaginada<CarritoDto> GetAll(int limit = 10, int offset = 0);
    
    Task<CarritoDto> CreateAsync(CarritoCrearDto carrito);

    Task UpdateAsync(Guid id, CarritoActualizarDto carritoActualizarDto);

    Task<bool> DeleteAsync(Guid carritoId);
}

