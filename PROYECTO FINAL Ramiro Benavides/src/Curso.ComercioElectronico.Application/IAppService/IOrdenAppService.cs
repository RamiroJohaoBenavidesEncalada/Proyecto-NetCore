namespace Curso.ComercioElectronico.Application;



public interface IOrdenAppService
{
    Task<OrdenDto> GetByIdAsync(Guid id);

    ListaPaginada<OrdenDto> GetAll(int limit=10,int offset=0);
    
    //Necesito sacar las ordenes de este cliente, este cliente ha comprado en los ultimos 3 meses o el 
    //total de compras 
    //ListaPaginada<OrdenDto> GetByClientIdAll(int clientId, int limit=10,int offset=0);


    //Task<OrdenDto> CreateAsync(OrdenCrearDto orden);

    Task UpdateAsync (Guid id, OrdenActualizarDto orden);

    Task<bool> DeleteAsync(Guid ordenId);


    Task<OrdenDto> CreateByIdAsync(Guid carritoId, OrdenCrearDto ordenCrearDto);

    Task<ListaPaginada<OrdenDto>> GetListAsync(OrdenListInput input);
}

public class OrdenListInput
{
    public int Limit{get;set;}=10;
    public int Offset{get;set;}=0;
    public Guid? ClienteId{get;set;}
    public string? MetodoEntregaCompraId {get;set;}
}
