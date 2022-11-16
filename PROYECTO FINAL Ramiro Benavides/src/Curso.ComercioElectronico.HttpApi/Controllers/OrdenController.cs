

using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize]
public class OrdenController : ControllerBase
{

    private readonly IOrdenAppService ordenAppService;

    public OrdenController(IOrdenAppService ordenAppService)
    {
        this.ordenAppService = ordenAppService;
    }

    [HttpGet]
    public ListaPaginada<OrdenDto> GetAll(int limit=10,int offset=0)
    {

        return ordenAppService.GetAll(limit,offset);

    }

    [HttpGet("{id}")]
    public async Task<OrdenDto>  GetByIdAsync(Guid id)
    {
        return await ordenAppService.GetByIdAsync(id);
    }


    
    /*
    [HttpPost]
    public async Task<OrdenDto> CreateAsync(OrdenCrearDto marca)
    {

        return await ordenAppService.CreateAsync(marca);

    }
    */
    
    [HttpPut]
    public async Task UpdateAsync(Guid id, OrdenActualizarDto marca)
    {

        await ordenAppService.UpdateAsync(id, marca);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(Guid ordenId)
    {

        return await ordenAppService.DeleteAsync(ordenId);

    }

    [HttpPost("{carritoId}")]
    public async Task<OrdenDto> CreateByIdAsync(Guid carritoId, OrdenCrearDto ordenCrearDto)
    {
        return await ordenAppService.CreateByIdAsync(carritoId,ordenCrearDto);
    }

    [HttpGet("ListaProductoPorMetodoEntregaOCliente")]
    public async Task<ListaPaginada<OrdenDto>> GetListAsync([FromQuery]OrdenListInput input)
    {
        return await ordenAppService.GetListAsync(input);
    }

}
