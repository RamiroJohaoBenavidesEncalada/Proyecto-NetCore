using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Curso.ComercioElectronico.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;



public class ProductoAppService : IProductoAppService
{
    private readonly IProductoRepository productoRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<ProductoAppService> logger;
    private readonly IValidator<ProductoCrearActualizarDto> validator;
    private readonly IMapper mapper;

    public ProductoAppService(IProductoRepository productoRepository, IUnitOfWork unitOfWork,ILogger<ProductoAppService> logger,
                    IValidator<ProductoCrearActualizarDto> validator, IMapper mapper)
    {
        this.productoRepository = productoRepository;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
        this.validator = validator;
        this.mapper = mapper;
    }

    
    public async Task<ListaPaginada<ProductoDto>> GetListAsync(ProductoListInput input)
    {

        logger.LogInformation("Obtener Lista Productos por TipoProducto||Marca||Nombre");


        var consulta = productoRepository.GetAllIncluding(x => x.Marca, x => x.TipoProducto);


        if (!string.IsNullOrEmpty(input.TipoProductoId))
        {
            consulta = consulta.Where(x => x.TipoProductoId == input.TipoProductoId);
        }

        if (!string.IsNullOrEmpty(input.MarcaId))
        {
            consulta = consulta.Where(x => x.MarcaId == input.MarcaId);
        }

        if (!string.IsNullOrEmpty(input.BuscarEnNombre))
        {
            //Cuando se quiera hacer tambien con codigo
            //consulta = consulta.Where(x=>x.Nombre.Contains(input.ValorBuscar)) ||
            //      x.Codigo.StarsWith(input.ValorBuscar);
            consulta = consulta.Where(x => x.Nombre.Contains(input.BuscarEnNombre));
        }

        //1. Ejecutar linq Total registros
        var total = consulta.Count();
        consulta = consulta.Skip(input.Offset).Take(input.Limit);
        
        var listaProductosDto = mapper.Map<IList<ProductoDto>>(consulta);        

        var resultado = new ListaPaginada<ProductoDto>();
        resultado.Total = total;
        resultado.Lista = listaProductosDto;
        return resultado;
    }


    public async Task<ProductoDto> GetByIdAsync(Guid productoId)
    {

        logger.LogInformation("Obtener Lista Productos por Id");

        //Reglas Validaciones... 
        var marca = await productoRepository.GetByIdAsync(productoId);
        if (marca == null){
            var msg=$"El producto con el id: {productoId}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
        }
        
        //Lista
        var consulta = productoRepository.GetAllIncluding(x => x.Marca, x => x.TipoProducto);
        consulta = consulta.Where(x => x.Id == productoId);
        
        
        var consultaListaProductosDto = mapper.Map<IList<ProductoDto>>(consulta);

        return consultaListaProductosDto.SingleOrDefault();
    }

    public async Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto productoDto)
    {
        logger.LogInformation("Crear Producto");

        await validator.ValidateAndThrowAsync(productoDto);

        //Reglas Validaciones... 
        var existeNombreProducto = await productoRepository.ExisteNombre(productoDto.Nombre);
        if (existeNombreProducto){
            var msg=$"Ya existe un producto con el nombre {productoDto.Nombre}";
            logger.LogError(msg);
            throw new ArgumentException(msg);

        }
        
        
        var producto = mapper.Map<Producto>(productoDto);

        producto.Id=Guid.NewGuid();
        
        //Persistencia objeto
        producto = await productoRepository.AddAsync(producto);
        await unitOfWork.SaveChangesAsync();

        
        var productoCreado = mapper.Map<ProductoDto>(producto);

        return productoCreado;
    }

    public async Task UpdateAsync(Guid productoId, ProductoCrearActualizarDto productoDto)
    {
        
        logger.LogInformation("Actualizar Producto por Id");

        await validator.ValidateAndThrowAsync(productoDto);

        var producto = await productoRepository.GetByIdAsync(productoId);
        if (producto == null){
            var msg=$"El producto con el id: {productoId}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
            
        }
        
        var existeNombreProducto = await productoRepository.ExisteNombre(productoDto.Nombre,productoId);
        if (existeNombreProducto){
            var msg=$"Ya existe un producto con el nombre {productoDto.Nombre}";
            logger.LogError(msg);
            throw new ArgumentException(msg);
        }
        
        
        producto = mapper.Map<ProductoCrearActualizarDto, Producto>(productoDto, producto);
        //Persistencia objeto
        await productoRepository.UpdateAsync(producto);
        await productoRepository.UnitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(Guid productoId)
    {

        logger.LogInformation("Eliminar Producto por Id");

        //Reglas Validaciones... 
        var marca = await productoRepository.GetByIdAsync(productoId);
        if (marca == null){
            var msg=$"El producto con el id: {productoId}, no existe";
            logger.LogError(msg);
            throw new ArgumentException(msg);
        }

        var producto = await productoRepository.GetByIdAsync(productoId);
        productoRepository.Delete(producto);
        await productoRepository.UnitOfWork.SaveChangesAsync();
        return true;
    }

    public ListaPaginada<ProductoDto> GetAll(int limit = 10, int offset = 0)
    {

        logger.LogInformation("Obtener Lista Produtos Paginada");

        //Lista
        var consulta = productoRepository.GetAllIncluding(x => x.Marca, x => x.TipoProducto);

        //1.Ejecutar linq Total registros
        var total = consulta.Count();


        var ListaProductosDto = mapper.Map<IList<ProductoDto>>(consulta); 
        var resultado = new ListaPaginada<ProductoDto>();
        resultado.Total = total;
        resultado.Lista = ListaProductosDto;
        return resultado;

    }
    
    public async Task<ListaPaginada<ProductoDto>> ListaProductoIdAsync(IList<Guid> listaIds)//, bool asNoTracking = true)
    {

        logger.LogInformation("Obtener Lista Productos por Lista de Ids");

        
        var listaProductos = await productoRepository.ListaProductoIdAsync(listaIds);

        
        
        var total = listaProductos.Count();

        var listaProductosDto = mapper.Map<IList<ProductoDto>>(listaProductos); 
        var resultado = new ListaPaginada<ProductoDto>();
        resultado.Total = total;
        resultado.Lista = listaProductosDto;
        return resultado;

        
    }


    public async Task<ListaPaginada<ProductoDto>> GetListaNombrePrecioAsync(int limit = 0, int offset = 0, string? nombre = "", decimal precio = 0)
    {
        
        logger.LogInformation("Obtener Lista Productos paginada por Nombre||Precio menor al puesto");

        var consulta = productoRepository.GetAllIncluding(x => x.Marca, x => x.TipoProducto);


        if (!string.IsNullOrEmpty(nombre))
        {
            consulta = consulta.Where(x => x.Nombre.Contains(nombre));
        }

        if (precio != 0)
        {
            consulta = consulta.Where(x => x.Precio < precio);
        }

        //1. Ejecutar linq Total registros
        var total = consulta.Count();
        consulta = consulta.Skip(offset).Take(limit);

        var listaProductosDto = mapper.Map<IList<ProductoDto>>(consulta); 
        var resultado = new ListaPaginada<ProductoDto>();
        resultado.Total = total;
        resultado.Lista = listaProductosDto;
        return resultado;
    }

}