

using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize]
public class MetodoEntregaCompraController : ControllerBase
{

    private readonly IMetodoEntregaCompraAppService metodoEntregaCompraAppService;

    public MetodoEntregaCompraController(IMetodoEntregaCompraAppService metodoEntregaCompraAppService)
    {
        this.metodoEntregaCompraAppService = metodoEntregaCompraAppService;
    }

    [HttpGet]
    public ICollection<MetodoEntregaCompraDto> GetAll()
    {

        return metodoEntregaCompraAppService.GetAll();
    }

    [HttpPost]
    public async Task<MetodoEntregaCompraDto> CreateAsync(MetodoEntregaCompraCrearActualizarDto entregaDto)
    {

        return await metodoEntregaCompraAppService.CreateAsync(entregaDto);

    }

    [HttpPut]
    public async Task UpdateAsync(string id, MetodoEntregaCompraCrearActualizarDto entregaDto)
    {

        await metodoEntregaCompraAppService.UpdateAsync(id, entregaDto);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(string entregaId)
    {

        return await metodoEntregaCompraAppService.DeleteAsync(entregaId);

    }
    [HttpGet("{entregaId}")]
    public async Task<MetodoEntregaCompraDto> GetByIdAsync(string entregaId)
    {
        return await metodoEntregaCompraAppService.GetByIdAsync(entregaId);
    }


}
