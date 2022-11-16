using AutoMapper;
using Curso.ComercioElectronico.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class TipoProductoAppService : ITipoProductoAppService
{
    private readonly ITipoProductoRepository repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IValidator<TipoProductoCrearActualizarDto> validator;
    private readonly ILogger<TipoProductoAppService> logger;

    public TipoProductoAppService(ITipoProductoRepository repository, IUnitOfWork unitOfWork, IMapper mapper,
            IValidator<TipoProductoCrearActualizarDto> validator,
            ILogger<TipoProductoAppService> logger)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.validator = validator;
        this.logger = logger;
    }

    public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tipoProductoDto)
    {

        logger.LogInformation("Crear Tipo de Producto");
        
        await validator.ValidateAndThrowAsync(tipoProductoDto);
        
        //Reglas Validaciones... 
        var existeNombreTipoProducto = await repository.ExisteNombre(tipoProductoDto.Nombre);
        if (existeNombreTipoProducto){
            var msg=$"Ya existe un tipo Producto con el nombre {tipoProductoDto.Nombre}";
            logger.LogError(msg);
            throw new ArgumentException(msg);
        }

        //Se crea un Id aleatorio de tipo string
        var guid = Guid.NewGuid();
        var Id = guid.ToString().Replace("-", String.Empty).Substring(0, 8);

        //Automatico
        //Mapeo Dto => Entidad
        var tipoProducto = mapper.Map<TipoProducto>(tipoProductoDto);
        tipoProducto.Id = Id; 

        //Persistencia objeto
        tipoProducto = await repository.AddAsync(tipoProducto);
        await unitOfWork.SaveChangesAsync();

        //Automatico
        //Mapeo Entidad => Dto
        var tipoProductoCreado = mapper.Map<TipoProductoDto>(tipoProducto);

        return tipoProductoCreado;
    }

    public async Task UpdateAsync(string id, TipoProductoCrearActualizarDto tipoProductoDto)
    {
        logger.LogInformation("Actualizar Tipo Producto por id");

        await validator.ValidateAndThrowAsync(tipoProductoDto);

        var tipoProducto = await repository.GetByIdAsync(id);
        if (tipoProducto == null){
            var msg=$"El tipo Producto con el id: {id}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }
        
        var existeNombreTipoProducto = await repository.ExisteNombre(tipoProductoDto.Nombre,id);
        if (existeNombreTipoProducto){
            var msg=$"Ya existe una tipo Producto con el nombre {tipoProductoDto.Nombre}";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }

        //Mapeo Dto => Entidad
        //tipoProducto.Nombre = tipoProductoDto.Nombre;
        tipoProducto = mapper.Map<TipoProductoCrearActualizarDto, TipoProducto>(tipoProductoDto, tipoProducto);
        //tipoProducto = mapper.Map<TipoProducto>(tipoProductoDto);

        //Persistencia objeto
        await repository.UpdateAsync(tipoProducto);
        await repository.UnitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(string tipoProductoId)
    {
        logger.LogInformation("Eliminar Tipo Producto por id");

        //Reglas Validaciones... 
        var tipoProducto = await repository.GetByIdAsync(tipoProductoId);
        if (tipoProducto == null){
            var msg=$"El TipoProducto con el id: {tipoProductoId}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }

        repository.Delete(tipoProducto);
        await repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<TipoProductoDto> GetAll()
    {
        logger.LogInformation("Obtener todos los Tipos de Producto");

        var tipoProductoList = repository.GetAll();

        var tipoProductoListDto =  from m in tipoProductoList
                            select new TipoProductoDto(){
                                Id = m.Id,
                                Nombre = m.Nombre
                            };

        return tipoProductoListDto.ToList();
    }

    public async Task<TipoProductoDto> GetByIdAsync(string tipoProductoId)
    {
        logger.LogInformation("Obtener Tipo de Producto por Id");
        
        //Reglas Validaciones... 
        var tipoProducto = await repository.GetByIdAsync(tipoProductoId);
        if (tipoProducto == null){
            var msg=$"El tipo de producto con el id: {tipoProductoId}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }

        //Automatico
        //Mapeo Entidad => Dto
        var tipoProductoCreado = mapper.Map<TipoProductoDto>(tipoProducto);

        return tipoProductoCreado;

    }

    
}

