
using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize]
public class TipoProductoController : ControllerBase
{

    private readonly ITipoProductoAppService tipoProductoAppService;

    public TipoProductoController(ITipoProductoAppService tipoProductoAppService)
    {
        this.tipoProductoAppService = tipoProductoAppService;
    }

    [HttpGet]
    public ICollection<TipoProductoDto> GetAll()
    {

        return tipoProductoAppService.GetAll();
    }

    [HttpPost]
    public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tipoProducto)
    {

        return await tipoProductoAppService.CreateAsync(tipoProducto);

    }

    [HttpPut]
    public async Task UpdateAsync(string id, TipoProductoCrearActualizarDto tipoProducto)
    {

        await tipoProductoAppService.UpdateAsync(id, tipoProducto);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(string tipoProductoId)
    {

        return await tipoProductoAppService.DeleteAsync(tipoProductoId);

    }
    [HttpGet("{tipoProductoId}")]
    public async Task<TipoProductoDto> GetByIdAsync(string tipoProductoId)
    {
        return await tipoProductoAppService.GetByIdAsync(tipoProductoId);
    }

}

