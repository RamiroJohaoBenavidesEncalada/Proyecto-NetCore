using AutoMapper;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class CarritoAppService : ICarritoAppService
{
    private readonly ICarritoRepository carritoRepository;
    private readonly IProductoAppService productoAppService;
    

    public CarritoAppService(
        ICarritoRepository carritoRepository,
        
        IProductoAppService productoAppService 
        
        )
    {
        this.carritoRepository = carritoRepository;
        this.productoAppService = productoAppService;
        
    }

    public async Task<CarritoDto> CreateAsync(CarritoCrearDto carritoDto)
    {

        //2. Mapeos
        var carrito = new Carrito(Guid.NewGuid());
        carrito.ClienteId = carritoDto.ClienteId;
        carrito.Estado = CarritoEstado.Lleno;
        

        var observaciones = string.Empty;

        foreach (var item in carritoDto.Items)
        {

            var productoDto = await productoAppService.GetByIdAsync(item.ProductoId);
            
            //Si no existe el producto

            if (productoDto != null){
                var carritoItem = new CarritoItem(Guid.NewGuid());
                carritoItem.Cantidad = item.Cantidad;
                carritoItem.Precio = productoDto.Precio;
                carritoItem.ProductoId = productoDto.Id;
                carritoItem.Observaciones = item.Observaciones;

                //ordenItem.SubTotal = (Cantidad * Precio) - Descuento ??

                //Aqui agrego mi Item
                carrito.AgregarItem(carritoItem);
            }else{
                observaciones+=$"El producto {item.ProductoId}, no existe";
            }
        }

        //Total de la Orden
        
        carrito.Total =  carrito.Items.Sum(x => x.Cantidad*x.Precio);
        carrito.Observaciones = observaciones;

        //3. Persistencias.
        carrito = await carritoRepository.AddAsync(carrito);
        await carritoRepository.UnitOfWork.SaveChangesAsync();
        
        return await GetByIdAsync(carrito.Id);
    }

    public Task<bool> AnularAsync(Guid odenId)
    {
        throw new NotImplementedException();
    }

    public ListaPaginada<CarritoDto> GetAll(int limit = 10, int offset = 0)
    {
        var consulta = carritoRepository.GetAllIncluding(x => x.Cliente, x => x.Items); //, x => x.Vendedor);
        
        var total = consulta.Count();
        consulta = consulta.Skip(offset).Take(limit);
        var consultaCarritoDto = consulta
                                .Select(
                                    x => new CarritoDto()
                                    {
                                         //VendedorNombre = $"{x.Vendedor.Nombre} {x.Vendedor.Apellido}", 
                                         Id = x.Id,
                                         Cliente = x.Cliente.Nombres,
                                         ClienteId = x.ClienteId,
                                         Estado = x.Estado,
                                         //Fecha = x.Fecha,
                                         //FechaAnulacion = x.FechaAnulacion,
                                         Observaciones = x.Observaciones,
                                         Total = x.Total,
                                         Items = x.Items.Select(item => new CarritoItemDto(){
                                            Cantidad = item.Cantidad,
                                            Id = item.Id,
                                            Observaciones = item.Observaciones,
                                            CarritoId = item.Id,
                                            Precio  = item.Precio,
                                            ProductoId = item.ProductoId,
                                            Producto = item.Producto.Nombre
                                         }).ToList()
                                    }
                                );

    

        var resultado = new ListaPaginada<CarritoDto>();
        resultado.Total = total;
        resultado.Lista = consultaCarritoDto.ToList();
        return resultado;
        //return Task.FromResult(consultaCarritoDto.SingleOrDefault());

    }

    public Task<CarritoDto> GetByIdAsync(Guid id)
    {
        var consulta = carritoRepository.GetAllIncluding(x => x.Cliente, x => x.Items); //, x => x.Vendedor);
        consulta = consulta.Where(x => x.Id == id);

        var consultaCarritoDto = consulta
                                .Select(
                                    x => new CarritoDto()
                                    {
                                         //VendedorNombre = $"{x.Vendedor.Nombre} {x.Vendedor.Apellido}", 
                                         Id = x.Id,
                                         Cliente = x.Cliente.Nombres,
                                         ClienteId = x.ClienteId,
                                         Estado = x.Estado,
                                         //Fecha = x.Fecha,
                                         //FechaAnulacion = x.FechaAnulacion,
                                         Observaciones = x.Observaciones,
                                         Total = x.Total,
                                         Items = x.Items.Select(item => new CarritoItemDto(){
                                            Cantidad = item.Cantidad,
                                            Id = item.Id,
                                            Observaciones = item.Observaciones,
                                            CarritoId = item.Id,
                                            Precio  = item.Precio,
                                            ProductoId = item.ProductoId,
                                            Producto = item.Producto.Nombre
                                         }).ToList()
                                    }
                                ); 

        return Task.FromResult(consultaCarritoDto.SingleOrDefault());
    }

    public async Task UpdateAsync(Guid id, CarritoActualizarDto carritoActualizarDto)
    {
        var carrito = await carritoRepository.GetByIdAsync(id);
        if (carrito == null){
            //var msg=$"El producto con el id: {id}, no existe";
            //logger.LogError(msg);
            throw new ArgumentException($"El producto con el id: {id}, no existe");
            
        }

        //Mapeo Dto => Entidad

        carrito.Estado=carritoActualizarDto.Estado;
        carrito.Observaciones=carritoActualizarDto.Observaciones;
        
        await carritoRepository.UpdateAsync(carrito);
        await carritoRepository.UnitOfWork.SaveChangesAsync();

        return;


    }

    public async Task<bool> DeleteAsync(Guid carritoId)
    {

        //logger.LogInformation("Eliminar Producto por Id");

        //Reglas Validaciones... 
        var marca = await carritoRepository.GetByIdAsync(carritoId);
        if (marca == null){
            //var msg=$"El producto con el id: {carritoId}, no existe";
            //logger.LogError(msg);
            throw new ArgumentException($"El producto con el id: {carritoId}, no existe");
        }

        var producto = await carritoRepository.GetByIdAsync(carritoId);
        carritoRepository.Delete(producto);
        await carritoRepository.UnitOfWork.SaveChangesAsync();
        return true;
    }
}


