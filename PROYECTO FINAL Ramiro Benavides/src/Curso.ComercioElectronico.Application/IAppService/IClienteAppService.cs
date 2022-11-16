using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public interface IClienteAppService
{

    ICollection<ClienteDto> GetAll();

    Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto cliente);

    Task UpdateAsync (Guid id, ClienteCrearActualizarDto cliente);

    Task<bool> DeleteAsync(Guid clienteId);

    Task<ClienteDto> GetByIdAsync(Guid clienteId);
    Task<decimal> GetDescuentoAsync(Guid clienteId);
    Task<ListaPaginada<ClienteDto>> GetListaNombreCedulaAsync(int limit = 0, int offset = 0, string? nombre = "", string cedula = "");

    Task<ICollection<ClienteDto>> GetClientesDtoAsync();

}
 
