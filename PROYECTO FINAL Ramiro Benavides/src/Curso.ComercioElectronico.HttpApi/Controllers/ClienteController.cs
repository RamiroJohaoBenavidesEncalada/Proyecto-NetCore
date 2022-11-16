
using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{

    private readonly IClienteAppService clienteAppService;

    public ClienteController(IClienteAppService clienteAppService)
    {
        this.clienteAppService = clienteAppService;
    }

    [HttpGet]
    public ICollection<ClienteDto> GetAll()
    {

        return clienteAppService.GetAll();
    }

    [HttpPost]
    public async Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto cliente)
    {

        return await clienteAppService.CreateAsync(cliente);

    }

    [HttpPut]
    public async Task UpdateAsync(Guid id, ClienteCrearActualizarDto cliente)
    {

        await clienteAppService.UpdateAsync(id, cliente);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(Guid clienteId)
    {

        return await clienteAppService.DeleteAsync(clienteId);

    }

    [HttpGet("{clienteId}")]
    public async Task<ClienteDto> GetByIdAsync(Guid clienteId)
    {
        return await clienteAppService.GetByIdAsync(clienteId);
    }

    [HttpGet("ListaClientesPorNombreOCedula")]
    public async Task<ListaPaginada<ClienteDto>> GetListaNombreCedulaAsync(int limit = 10, int offset = 0, string? nombre = "", string? cedula = "")
    {
        return await clienteAppService.GetListaNombreCedulaAsync(limit , offset, nombre,cedula);
    }

}