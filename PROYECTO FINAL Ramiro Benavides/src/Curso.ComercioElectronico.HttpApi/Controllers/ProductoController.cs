
using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductoController : ControllerBase
{

    private readonly IProductoAppService productoAppService;

    public ProductoController(IProductoAppService productoAppService)
    {
        this.productoAppService = productoAppService;
    }

    [HttpGet("{productoId}")]
    public Task<ProductoDto> GetByIdAsync(Guid productoId)
    {
        return productoAppService.GetByIdAsync(productoId);
    }

    [HttpGet]
    public ListaPaginada<ProductoDto> GetAll(int limit=10,int offset=0)
    {

        return productoAppService.GetAll(limit,offset);

    }

    [HttpGet("ListaProductoPorNombreMarcaTipoProducto")]
    public Task<ListaPaginada<ProductoDto>> GetListAsync([FromQuery] ProductoListInput input)
    {
        return productoAppService.GetListAsync(input);
    }

    [HttpPost]
    public async Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto producto)
    {

        return await productoAppService.CreateAsync(producto);

    }

    [HttpPut]
    public async Task UpdateAsync(Guid productoId, ProductoCrearActualizarDto producto)
    {

        await productoAppService.UpdateAsync(productoId, producto);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(Guid productoId)
    {

        return await productoAppService.DeleteAsync(productoId);

    }
    
    [HttpGet("ListaProductoPorListaId")]
    public async Task<ListaPaginada<ProductoDto>> ListaProductoIdAsync([FromQuery] IList<Guid> listaIds)
    {
        return await productoAppService.ListaProductoIdAsync(listaIds);
    }
    
    [HttpGet("ListaProductoPorNombrePrecio")]
    public async Task<ListaPaginada<ProductoDto>> GetListaNombrePrecioAsync(int limit = 10, int offset = 0, string? nombre = "", decimal precio = 0)
    {
        return await productoAppService.GetListaNombrePrecioAsync(limit, offset,nombre,precio);
    }
    /*
    [HttpGet("Stock")]
    public async Task<int> GetStockAsync(Guid productoId)
    {
        return await productoAppService.GetStockAsync(productoId);
    }
    [HttpPut("Stock")]
    public async Task<bool> UpdateStockAsync(Guid productoId, int nuevoStock)
    {
        return await productoAppService.UpdateStockAsync(productoId, nuevoStock);
    }
    */

}