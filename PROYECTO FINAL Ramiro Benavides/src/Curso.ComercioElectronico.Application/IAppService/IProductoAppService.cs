
namespace Curso.ComercioElectronico.Application;



public interface IProductoAppService
{
    Task<ProductoDto> GetByIdAsync(Guid productoId);
    ListaPaginada<ProductoDto> GetAll(int limit=10,int offset=0);

    //obtener una lista paginada de productos, hay tres opciones
    //poner el TipoProductoId busca todos los productos que tengan ese tipo Producto
    //poner el MarcaId busca todos los productos que tengan esa Marca
    //poner valor a buscar una letra, silaba palabra y se buscará en los nombre de los productos
    //Si los 3 espacios MarcaId, ProductId y valor a buscar estan llenos se buscará por valor a buscar
    Task<ListaPaginada<ProductoDto>>GetListAsync(ProductoListInput input);
    Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto producto);

    Task UpdateAsync (Guid productoId, ProductoCrearActualizarDto producto);

    Task<bool> DeleteAsync(Guid productoId);
    //Obtener una lista de productos con una lista de Ids Guid
    Task<ListaPaginada<ProductoDto>>  ListaProductoIdAsync(IList<Guid> listaIds);//, bool asNoTracking = true);
    // obtener una lista de productos paginados, por nombre o que sean menores a un precio especificado
    Task<ListaPaginada<ProductoDto>> GetListaNombrePrecioAsync(int limit = 10, int offset = 0, string nombre = "", decimal precio = 0);

    //Task<int> GetStockAsync(Guid productoId);

    //Task<bool> UpdateStockAsync(Guid productoId, int nuevoStock);


}

public class ProductoListInput
{
    public int Limit{get;set;}=10;
    public int Offset{get;set;}=0;
    public string? TipoProductoId{get;set;}
    public string? MarcaId {get;set;}
    public string? BuscarEnNombre{get;set;}
}
