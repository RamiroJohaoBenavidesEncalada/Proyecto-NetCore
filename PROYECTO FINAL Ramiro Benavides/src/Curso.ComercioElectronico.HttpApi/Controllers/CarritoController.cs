

using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CarritoController : ControllerBase
{

    private readonly ICarritoAppService carritoAppService;

    public CarritoController(ICarritoAppService carritoAppService)
    {
        this.carritoAppService = carritoAppService;
    }

    [HttpGet]
    public ListaPaginada<CarritoDto> GetAll(int limit = 10, int offset = 0)
    {
        return carritoAppService.GetAll(limit,offset);
    }

    [HttpGet("{id}")]
    public Task<CarritoDto> GetByIdAsync(Guid id)
    {
        return carritoAppService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<CarritoDto> CreateAsync(CarritoCrearDto carrito)
    {
        return await carritoAppService.CreateAsync(carrito);
    }

    [HttpPut]
    public async Task UpdateAsync(Guid id, CarritoActualizarDto carritoActualizarDto)
    {
        await carritoAppService.UpdateAsync(id, carritoActualizarDto);
    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(Guid carritoId)
    {
        return await carritoAppService.DeleteAsync(carritoId);
    }

}