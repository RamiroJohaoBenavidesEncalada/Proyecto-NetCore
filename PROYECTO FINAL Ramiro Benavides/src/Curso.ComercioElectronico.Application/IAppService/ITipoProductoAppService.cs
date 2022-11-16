using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public interface ITipoProductoAppService
{

    ICollection<TipoProductoDto> GetAll();

    Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tipoProducto);

    Task UpdateAsync (string id, TipoProductoCrearActualizarDto tipoProducto);

    Task<bool> DeleteAsync(string tipoProductoId);

    Task<TipoProductoDto> GetByIdAsync(string tipoProductoId);
}
 
 