namespace Curso.ComercioElectronico.Domain;

public interface IMetodoEntregaCompraRepository:  IRepository<MetodoEntregaCompra,string> {


    Task<bool> ExisteNombre(string nombre);

    Task<bool> ExisteNombre(string nombre, string idExcluir);

}



