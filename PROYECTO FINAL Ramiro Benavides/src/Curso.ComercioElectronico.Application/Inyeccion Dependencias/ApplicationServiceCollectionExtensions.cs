

using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Curso.ComercioElectronico.Application;

public static class AplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IMarcaAppService, MarcaAppService>();
        services.AddTransient<ITipoProductoAppService, TipoProductoAppService>();
        services.AddTransient<IProductoAppService, ProductoAppService>();
        services.AddTransient<IClienteAppService, ClienteAppService>();
        services.AddTransient<IOrdenAppService, OrdenAppService>();
        services.AddTransient<IMetodoEntregaCompraAppService, MetodoEntregaCompraAppService>();
        services.AddTransient<ICarritoAppService, CarritoAppService>();
        //Configurar la inyeccion de todos los Profile que existen en un Assembly
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //Configurar las validaciones
        services.AddScoped<IValidator<MarcaCrearActualizarDto>, MarcaCrearActualizarDtoValidator>();
        services.AddScoped<IValidator<TipoProductoCrearActualizarDto>, TipoProductoCrearActualizarDtoValidator>();
        services.AddScoped<IValidator<ProductoCrearActualizarDto>, ProductoCrearActualizarDtoValidator>();
        services.AddScoped<IValidator<ClienteCrearActualizarDto>, ClienteCrearActualizarDtoValidator>();
        services.AddScoped<IValidator<MetodoEntregaCompraCrearActualizarDto>, MetodoEntregaCompraCrearActualizarDtoValidator>();
        //Configurar todas las validaciones
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}