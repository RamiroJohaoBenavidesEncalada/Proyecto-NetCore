using AutoMapper;
using Curso.ComercioElectronico.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class MetodoEntregaCompraAppService : IMetodoEntregaCompraAppService
{
    private readonly IMetodoEntregaCompraRepository repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<MetodoEntregaCompraAppService> logger;
    private readonly IValidator<MetodoEntregaCompraCrearActualizarDto> validator;

    private readonly IMapper mapper;
    public MetodoEntregaCompraAppService(IMetodoEntregaCompraRepository repository, IUnitOfWork unitOfWork,ILogger<MetodoEntregaCompraAppService> logger,
            IValidator<MetodoEntregaCompraCrearActualizarDto> validator, IMapper mapper)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
        this.validator = validator;
        this.mapper = mapper;
    }

    public async Task<decimal> GetPrecioEntregaAsync(string entregaId)
    {
        logger.LogInformation("Obtener Valor Costo Entrega por Id");

        //Reglas Validaciones... 
        var entrega = await repository.GetByIdAsync(entregaId);
        if (entrega == null){
            var msg=$"La entrega con el id: {entregaId}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
        }
        
        var costoEntrega = entrega.CostoEntrega;

        return costoEntrega;       
    }
    public async Task<MetodoEntregaCompraDto> CreateAsync(MetodoEntregaCompraCrearActualizarDto entregaDto)
    {

        logger.LogInformation("Crear Metodo de Entrega");

        //Opcion 2 Automatica Utilizar esta para el proyecto
        await validator.ValidateAndThrowAsync(entregaDto);

        //Reglas Validaciones... 
        var existeNombreEntrega = await repository.ExisteNombre(entregaDto.MetodoEntrega);
        if (existeNombreEntrega){
            
            var msg=$"Ya existe un metodo de entrega con el nombre {entregaDto.MetodoEntrega}";
            logger.LogError(msg);
            throw new ArgumentException(msg);
        }

        //Se crea un Id aleatorio de tipo string
        var guid = Guid.NewGuid();
        var Id = guid.ToString().Replace("-", String.Empty).Substring(0, 8);


        //Automatico
        //Mapeo Dto => Entidad
        var entrega = mapper.Map<MetodoEntregaCompra>(entregaDto);
        entrega.Id = Id; 
        //Persistencia objeto
        entrega = await repository.AddAsync(entrega);
        await unitOfWork.SaveChangesAsync();

        //Automatico
        //Mapeo Entidad => Dto
        var entregaCreada = mapper.Map<MetodoEntregaCompraDto>(entrega);

        return entregaCreada;
    }

    public async Task UpdateAsync(string id, MetodoEntregaCompraCrearActualizarDto entregaDto)
    {
        logger.LogInformation("Modificar Metodo de Entrega por id");

        await validator.ValidateAndThrowAsync(entregaDto);

        var entrega = await repository.GetByIdAsync(id);
        
        if (entrega == null){
            var msg=$"El Metodo de Entrega con el id: {id}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }
        
        var existeNombreEntrega = await repository.ExisteNombre(entregaDto.MetodoEntrega,id);
        if (existeNombreEntrega){
            var msg=$"Ya existe un Metodo de Entrega con el nombre {entregaDto.MetodoEntrega}";
            logger.LogError(msg);
            throw new ArgumentException(msg);
        }

        //Mapeo Dto => Entidad
        //marca.Nombre = marcaDto.Nombre;
        entrega = mapper.Map<MetodoEntregaCompraCrearActualizarDto, MetodoEntregaCompra>(entregaDto, entrega);

        //Persistencia objeto
        await repository.UpdateAsync(entrega);
        await repository.UnitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(string entregaId)
    {
        logger.LogInformation("Eliminar Metodo de Entrega por Id");
        
        //Reglas Validaciones... 
        var entrega = await repository.GetByIdAsync(entregaId);
        if (entrega == null){
            var msg=$"El Metodo de Entrega con el id: {entregaId}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }

        repository.Delete(entrega);
        await repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<MetodoEntregaCompraDto> GetAll()
    {
        logger.LogInformation("Obtener todas los Metodos de Entrega");

        var entregaList = repository.GetAll();

        var entregaListDto = mapper.Map<IList<MetodoEntregaCompraDto>>(entregaList);
        
        /*
        var marcaListDto =  from m in marcaList
                            select new MarcaDto(){
                                Id = m.Id,
                                Nombre = m.Nombre
                            };
        */
        return entregaListDto.ToList();
        
    }

    public async Task<MetodoEntregaCompraDto> GetByIdAsync(string entregaId)
    {
        logger.LogInformation("Obtener el metodo de entrega por Id");
        
        //Reglas Validaciones... 
        var entrega = await repository.GetByIdAsync(entregaId);
        if (entrega == null){
            var msg=$"El metodo de entrega con el id: {entregaId}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }

        //Automatico
        //Mapeo Entidad => Dto
        var entregaCreada = mapper.Map<MetodoEntregaCompraDto>(entrega);

        return entregaCreada;

    }
}

