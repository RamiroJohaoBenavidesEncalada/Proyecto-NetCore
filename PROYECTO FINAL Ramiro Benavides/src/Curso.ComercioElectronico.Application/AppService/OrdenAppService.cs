using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class OrdenAppService : IOrdenAppService
{
    private readonly IOrdenRepository ordenRepository;
    private readonly IProductoAppService productoAppService;
    private readonly ICarritoAppService carritoAppService;
    private readonly IClienteAppService clienteAppService;
    private readonly IMetodoEntregaCompraAppService metodoEntregaCompraAppService;

    public OrdenAppService(
        IOrdenRepository ordenRepository,
        //IProductoRepository productoRepository,
        IProductoAppService productoAppService,
        ICarritoAppService carritoAppService,
        IClienteAppService clienteAppService,
        IMetodoEntregaCompraAppService metodoEntregaCompraAppService)
    {
        this.ordenRepository = ordenRepository;
        this.productoAppService = productoAppService;
        this.carritoAppService = carritoAppService;
        this.clienteAppService = clienteAppService;
        this.metodoEntregaCompraAppService = metodoEntregaCompraAppService;
    }

    public async Task<ListaPaginada<OrdenDto>> GetListAsync(OrdenListInput input)
    {
        var consulta = ordenRepository.GetAllIncluding(x => x.Cliente, x => x.MetodoEntregaCompra);


        if (input.ClienteId != null)
        {
            consulta = consulta.Where(x => x.ClienteId == input.ClienteId);
        }

        if (!string.IsNullOrEmpty(input.MetodoEntregaCompraId))
        {
            consulta = consulta.Where(x => x.MetodoEntregaCompraId == input.MetodoEntregaCompraId);
        }

        var total = consulta.Count();
        consulta = consulta.Skip(input.Offset).Take(input.Limit);

        var consultaListaProductosDto = consulta
                                        .Select(
                                            x => new OrdenDto()
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
                                                Items = x.Items.Select(item => new OrdenItemDto()
                                                {
                                                    Cantidad = item.Cantidad,
                                                    Id = item.Id,
                                                    Observaciones = item.Observaciones,
                                                    OrdenId = item.Id,
                                                    Precio = item.Precio,
                                                    ProductoId = item.ProductoId,
                                                    Producto = item.Producto.Nombre
                                                }).ToList()
                                            }
                                        );
        //var listaProductosDto = mapper.Map<IList<ProductoDto>>(consulta);        

        var resultado = new ListaPaginada<OrdenDto>();
        resultado.Total = total;
        resultado.Lista = consultaListaProductosDto.ToList();
        return resultado;
    }

    public async Task<OrdenDto> CreateByIdAsync(Guid carritoId, OrdenCrearDto ordenCrearDto)
    {
        var carritoDto = await carritoAppService.GetByIdAsync(carritoId);

        var orden = new Orden(Guid.NewGuid());
        orden.ClienteId = carritoDto.ClienteId;
        orden.Estado = OrdenEstado.Registrada;
        orden.MetodoEntregaCompraId = ordenCrearDto.MetodoEntregaCompraId;


        //orden.Fecha = carritoDto.Fecha;
        //Porcentaje Descuento
        var porcentajeDescuento = await clienteAppService.GetDescuentoAsync(carritoDto.ClienteId);

        //Costo Entrega
        var costoEntrega = await metodoEntregaCompraAppService.GetPrecioEntregaAsync(ordenCrearDto.MetodoEntregaCompraId);

        var observaciones = string.Empty;

        foreach (var item in carritoDto.Items)
        {

            var productoDto = await productoAppService.GetByIdAsync(item.ProductoId);

            //Si no existe el producto

            if (productoDto != null)
            {
                var ordenItem = new OrdenItem(Guid.NewGuid());
                ordenItem.Cantidad = item.Cantidad;
                ordenItem.Precio = productoDto.Precio;
                ordenItem.ProductoId = productoDto.Id;
                ordenItem.Observaciones = item.Observaciones;

                //ordenItem.SubTotal = (Cantidad * Precio) - Descuento ??

                //Aqui agrego mi Item
                orden.AgregarItem(ordenItem);
            }
            else
            {
                observaciones += $"El producto {item.ProductoId}, no existe";
            }
        }

        //Total de la Orden

        orden.Total = orden.Items.Sum(x => x.Cantidad * x.Precio) * (1 - (porcentajeDescuento / 100)) + costoEntrega;
        orden.Observaciones = observaciones;

        //3. Persistencias.
        orden = await ordenRepository.AddAsync(orden);
        await ordenRepository.UnitOfWork.SaveChangesAsync();

        return await GetByIdAsync(orden.Id);

    }


    public async Task<bool> DeleteAsync(Guid ordenId)
    {

        //logger.LogInformation("Eliminar Producto por Id");

        //Reglas Validaciones... 
        var marca = await ordenRepository.GetByIdAsync(ordenId);
        if (marca == null)
        {
            //var msg=$"El producto con el id: {carritoId}, no existe";
            //logger.LogError(msg);
            throw new ArgumentException($"La orden con el id: {ordenId}, no existe");
        }

        var orden = await ordenRepository.GetByIdAsync(ordenId);
        ordenRepository.Delete(orden);
        await ordenRepository.UnitOfWork.SaveChangesAsync();
        return true;
    }

    public ListaPaginada<OrdenDto> GetAll(int limit = 10, int offset = 0)
    {
        var consulta = ordenRepository.GetAllIncluding(x => x.Cliente, x => x.Items); //, x => x.Vendedor);

        var total = consulta.Count();
        consulta = consulta.Skip(offset).Take(limit);
        var consultaOrdenDto = consulta
                                .Select(
                                    x => new OrdenDto()
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
                                        Items = x.Items.Select(item => new OrdenItemDto()
                                        {
                                            Cantidad = item.Cantidad,
                                            Id = item.Id,
                                            Observaciones = item.Observaciones,
                                            OrdenId = item.Id,
                                            Precio = item.Precio,
                                            ProductoId = item.ProductoId,
                                            Producto = item.Producto.Nombre
                                        }).ToList()
                                    }
                                );



        var resultado = new ListaPaginada<OrdenDto>();
        resultado.Total = total;
        resultado.Lista = consultaOrdenDto.ToList();
        return resultado;
        //return Task.FromResult(consultaCarritoDto.SingleOrDefault());

    }

    public Task<OrdenDto> GetByIdAsync(Guid id)
    {
        var consulta = ordenRepository.GetAllIncluding(x => x.Cliente, x => x.Items); //, x => x.Vendedor);
        consulta = consulta.Where(x => x.Id == id);

        var consultaOrdenDto = consulta
                                .Select(
                                    x => new OrdenDto()
                                    {
                                        //VendedorNombre = $"{x.Vendedor.Nombre} {x.Vendedor.Apellido}", 
                                        Id = x.Id,
                                        Cliente = x.Cliente.Nombres,
                                        ClienteId = x.ClienteId,
                                        Estado = x.Estado,
                                        Fecha = x.Fecha,
                                        //FechaAnulacion = x.FechaAnulacion,
                                        Observaciones = x.Observaciones,
                                        Total = x.Total,
                                        Items = x.Items.Select(item => new OrdenItemDto()
                                        {
                                            Cantidad = item.Cantidad,
                                            Id = item.Id,
                                            Observaciones = item.Observaciones,
                                            OrdenId = item.OrdenId,
                                            Precio = item.Precio,
                                            ProductoId = item.ProductoId,
                                            Producto = item.Producto.Nombre
                                        }).ToList()
                                    }
                                );
        return Task.FromResult(consultaOrdenDto.SingleOrDefault());
    }

    public async Task UpdateAsync(Guid id, OrdenActualizarDto ordenActualizarDto)
    {
        var orden = await ordenRepository.GetByIdAsync(id);
        if (orden == null)
        {
            //var msg=$"El producto con el id: {id}, no existe";
            //logger.LogError(msg);
            throw new ArgumentException($"La orden con el id: {id}, no existe");

        }

        //Mapeo Dto => Entidad

        orden.Estado = ordenActualizarDto.Estado;
        orden.Observaciones = ordenActualizarDto.Observaciones;

        await ordenRepository.UpdateAsync(orden);
        await ordenRepository.UnitOfWork.SaveChangesAsync();

        return;

    }
}


