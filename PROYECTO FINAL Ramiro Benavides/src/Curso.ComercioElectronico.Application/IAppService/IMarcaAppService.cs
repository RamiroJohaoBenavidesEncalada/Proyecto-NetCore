using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Application;


public interface IMarcaAppService
{

    ICollection<MarcaDto> GetAll();

    Task<MarcaDto> CreateAsync(MarcaCrearActualizarDto marca);

    Task UpdateAsync (string id, MarcaCrearActualizarDto marca);

    Task<bool> DeleteAsync(string marcaId);

    Task<MarcaDto> GetByIdAsync(string marcaId);
}
