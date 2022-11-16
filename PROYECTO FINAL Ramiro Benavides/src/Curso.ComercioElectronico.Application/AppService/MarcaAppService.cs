using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Curso.ComercioElectronico.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class MarcaAppService : IMarcaAppService
{
    private readonly IMarcaRepository repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<MarcaAppService> logger;
    private readonly IValidator<MarcaCrearActualizarDto> validator;

    private readonly IMapper mapper;
    public MarcaAppService(IMarcaRepository repository, IUnitOfWork unitOfWork,ILogger<MarcaAppService> logger,
            IValidator<MarcaCrearActualizarDto> validator, IMapper mapper)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
        this.validator = validator;
        this.mapper = mapper;
    }

    public async Task<MarcaDto> CreateAsync(MarcaCrearActualizarDto marcaDto)
    {

        logger.LogInformation("Crear Marca");

        //Opcion 2 Automatica Utilizar esta para el proyecto
        await validator.ValidateAndThrowAsync(marcaDto);

        //Reglas Validaciones... 
        var existeNombreMarca = await repository.ExisteNombre(marcaDto.Nombre);
        if (existeNombreMarca){
            
            var msg=$"Ya existe una marca con el nombre {marcaDto.Nombre}";
            logger.LogError(msg);
            throw new ArgumentException(msg);
        }

        //Se crea un Id aleatorio de tipo string
        var guid = Guid.NewGuid();
        var Id = guid.ToString().Replace("-", String.Empty).Substring(0, 8);

        //Automatico
        //Mapeo Dto => Entidad
        var marca = mapper.Map<Marca>(marcaDto);
        marca.Id = Id; 
        //Persistencia objeto
        marca = await repository.AddAsync(marca);
        await unitOfWork.SaveChangesAsync();

        //Automatico
        //Mapeo Entidad => Dto
        var marcaCreada = mapper.Map<MarcaDto>(marca);

        return marcaCreada;
    }

    public async Task UpdateAsync(string id, MarcaCrearActualizarDto marcaDto)
    {
        logger.LogInformation("Modificar Marca por id");

        await validator.ValidateAndThrowAsync(marcaDto);

        var marca = await repository.GetByIdAsync(id);
        
        if (marca == null){
            var msg=$"La marca con el id: {id}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }
        
        var existeNombreMarca = await repository.ExisteNombre(marcaDto.Nombre,id);
        if (existeNombreMarca){
            var msg=$"Ya existe una marca con el nombre {marcaDto.Nombre}";
            logger.LogError(msg);
            throw new ArgumentException(msg);
        }

        //Mapeo Dto => Entidad
        //marca.Nombre = marcaDto.Nombre;
        marca = mapper.Map<MarcaCrearActualizarDto, Marca>(marcaDto, marca);

        //Persistencia objeto
        await repository.UpdateAsync(marca);
        await repository.UnitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(string marcaId)
    {
        logger.LogInformation("Eliminar Marca por Id");
        
        //Reglas Validaciones... 
        var marca = await repository.GetByIdAsync(marcaId);
        if (marca == null){
            var msg=$"La marca con el id: {marcaId}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }

        repository.Delete(marca);
        await repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<MarcaDto> GetAll()
    {
        logger.LogInformation("Obtener todas las marcas");

        var marcaList = repository.GetAll();

        var marcaListDto = mapper.Map<IList<MarcaDto>>(marcaList);
        
        return marcaListDto.ToList();
        
    }

    public async Task<MarcaDto> GetByIdAsync(string marcaId)
    {
        logger.LogInformation("Obtener Marca por Id");
        
        //Reglas Validaciones... 
        var marca = await repository.GetByIdAsync(marcaId);
        if (marca == null){
            var msg=$"La marca con el id: {marcaId}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }

        //Automatico
        //Mapeo Entidad => Dto
        var marcaCreada = mapper.Map<MarcaDto>(marca);

        return marcaCreada;

    }
}

