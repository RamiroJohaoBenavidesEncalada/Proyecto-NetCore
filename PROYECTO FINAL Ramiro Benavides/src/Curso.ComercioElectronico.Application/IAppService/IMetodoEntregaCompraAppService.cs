using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Application;

public interface IMetodoEntregaCompraAppService
{

    ICollection<MetodoEntregaCompraDto> GetAll();

    Task<MetodoEntregaCompraDto> CreateAsync(MetodoEntregaCompraCrearActualizarDto entregaDto);

    Task UpdateAsync (string id, MetodoEntregaCompraCrearActualizarDto entregaDto);

    Task<bool> DeleteAsync(string entregaId);

    Task<MetodoEntregaCompraDto> GetByIdAsync(string entregaId);
    Task<decimal> GetPrecioEntregaAsync(string entregaId);
}
