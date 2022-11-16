

using AutoMapper;
using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Domain;

public class ConfiguracionesMapeoProfile : Profile
{
    //Para TipoProducto
    //TipoProductoCrearActualizarDto => TipoProducto

    //TipoProducto => TipoProductoDto
    public ConfiguracionesMapeoProfile()
    {
        CreateMap<TipoProductoCrearActualizarDto, TipoProducto>();
        CreateMap<TipoProducto, TipoProductoDto>();

        CreateMap<MarcaCrearActualizarDto, Marca>();
        CreateMap<Marca, MarcaDto>();
        
        
        CreateMap<Producto, ProductoDto>()
            .ForMember(dest => dest.Marca, opt =>opt.MapFrom(src =>src.Marca.Nombre))
            .ForMember(dest => dest.TipoProducto, opt =>opt.MapFrom(src =>src.TipoProducto.Nombre));
        
        CreateMap<ProductoCrearActualizarDto, Producto>();


        CreateMap<ClienteCrearActualizarDto, Cliente>();
        CreateMap<Cliente, ClienteDto>();
        
        CreateMap<MetodoEntregaCompraCrearActualizarDto, MetodoEntregaCompra>();
        CreateMap<MetodoEntregaCompra, MetodoEntregaCompraDto>();


        // Inicio Fin


        //Agregar otros mapeos que se requieran
    }
}