using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Curso.ComercioElectronico.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class ClienteAppService : IClienteAppService
{
    private readonly IClienteRepository clienteRepository;
    private readonly ILogger<MarcaAppService> logger;
    private readonly IUnitOfWork unitOfWork;
    private readonly IValidator<ClienteCrearActualizarDto> validator;
    private readonly IMapper mapper;

    public ClienteAppService(IClienteRepository clienteRepository, ILogger<MarcaAppService> logger,IUnitOfWork unitOfWork,
                IValidator<ClienteCrearActualizarDto> validator, IMapper mapper)
    {
        this.clienteRepository = clienteRepository;
        this.logger = logger;
        this.unitOfWork = unitOfWork;
        this.validator = validator;
        this.mapper = mapper;
    }

    public async Task<ListaPaginada<ClienteDto>> GetListaNombreCedulaAsync(int limit = 0, int offset = 0, string? nombre = "", string? cedula = "")
    {
        logger.LogInformation("Obtener Lista Clientes paginada por Nombre||Cedula");

        var consulta = clienteRepository.GetAll();
        if (!string.IsNullOrEmpty(nombre))
        {
            consulta = consulta.Where(x => x.Nombres.Contains(nombre));
        }

        if (!string.IsNullOrEmpty(cedula))
        {
            consulta = consulta.Where(x => x.Cedula.Contains(cedula));
        }

        //1. Ejecutar linq Total registros
        var total = consulta.Count();
        consulta = consulta.Skip(offset).Take(limit);

        var listaClientesDto = mapper.Map<IList<ClienteDto>>(consulta); 
        var resultado = new ListaPaginada<ClienteDto>();
        resultado.Total = total;
        resultado.Lista = listaClientesDto;
        return resultado;
    }


    public async Task<decimal> GetDescuentoAsync(Guid clienteId)
    {
        logger.LogInformation("Obtener Descuento por Id");

        //Reglas Validaciones... 
        var cliente = await clienteRepository.GetByIdAsync(clienteId);
        if (cliente == null){
            var msg=$"El clientecon el id: {clienteId}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
        }
        
        var porcentajeDescuento = cliente.PorcentajeDescuento;

        return porcentajeDescuento;       
    }


    public async Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto clienteDto)
    {
        logger.LogInformation("Crear Cliente");

        await validator.ValidateAndThrowAsync(clienteDto);

        //Reglas Validaciones... 
        var existeNombreCliente = await clienteRepository.ExisteNombre(clienteDto.Nombres);
        if (existeNombreCliente){
            var msg=$"Ya existe un cliente con el nombre {clienteDto.Nombres}";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }
        
        //Automatico
        //Mapeo Dto => Entidad
        var cliente = mapper.Map<Cliente>(clienteDto);
        
        //Persistencia objeto
        cliente = await clienteRepository.AddAsync(cliente);
        await unitOfWork.SaveChangesAsync();


        //Automatico
        //Mapeo Entidad => Dto
        var clienteCreada = mapper.Map<ClienteDto>(cliente);

        return clienteCreada;
    }

    public async Task UpdateAsync(Guid id, ClienteCrearActualizarDto clienteDto)
    {
        logger.LogInformation("Actualizar Cliente por id");

        await validator.ValidateAndThrowAsync(clienteDto);

        var cliente = await clienteRepository.GetByIdAsync(id);
        if (cliente == null){
            var msg=$"El cliente con el id: {id}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }
        
        var existeNombreCliente = await clienteRepository.ExisteNombre(clienteDto.Nombres,id);
        if (existeNombreCliente){
            var msg=$"Ya existe un cliente con el nombre {clienteDto.Nombres}";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }

        //Mapeo Dto => Entidad
        //marca.Nombre = marcaDto.Nombre;
        cliente = mapper.Map<ClienteCrearActualizarDto, Cliente>(clienteDto, cliente);
        //Persistencia objeto
        await clienteRepository.UpdateAsync(cliente);
        await clienteRepository.UnitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(Guid clienteId)
    {
        logger.LogInformation("Eliminar Cliente");

        //Reglas Validaciones... 
        var cliente = await clienteRepository.GetByIdAsync(clienteId);
        if (cliente == null){
            var msg=$"El cliente con el id: {clienteId}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }

        clienteRepository.Delete(cliente);
        await clienteRepository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<ClienteDto> GetAll()
    {

        logger.LogInformation("Obtener todos los Clientes");

        var clienteList = clienteRepository.GetAll();

        var clienteListDto = mapper.Map<IList<ClienteDto>>(clienteList);


        return clienteListDto.ToList();
    }

    public async Task<ClienteDto> GetByIdAsync(Guid clienteId)
    {
        logger.LogInformation("Obtener Cliente por Id");
        
        //Reglas Validaciones... 
        var cliente = await clienteRepository.GetByIdAsync(clienteId);
        if (cliente == null){
            var msg=$"El cliente con el id: {clienteId}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }

        //Automatico
        //Mapeo Entidad => Dto
        var clienteCreado = mapper.Map<ClienteDto>(cliente);

        return clienteCreado;

    }

    public async Task<ICollection<ClienteDto>> GetClientesDtoAsync()
        {
            
            var consulta = await clienteRepository.GetAsync();
            var Lista = new List<ClienteDto>();
            foreach (var item in consulta)
            {
               Lista.Add(mapper.Map<ClienteDto>(item));
            }
            return Lista;
        }

    

}

