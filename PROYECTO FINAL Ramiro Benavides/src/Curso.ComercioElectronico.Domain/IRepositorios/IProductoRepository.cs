namespace Curso.ComercioElectronico.Domain;

public interface IProductoRepository :  IRepository<Producto,Guid> {

    //Se requiere a nivel repositorio de producto, obtener una lista de productos, por medio de lista de Ids.
    Task<ICollection<Producto>> ListaProductoIdAsync(IList<Guid> listaIds);//,bool asNoTracking);
    
    Task<bool> ExisteNombre(string nombre);

    Task<bool> ExisteNombre(string nombre, Guid idExcluir);

}

