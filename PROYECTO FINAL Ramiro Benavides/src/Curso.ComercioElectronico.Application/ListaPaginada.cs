namespace Curso.ComercioElectronico.Application;

public class ListaPaginada<T> where T : class
{

    public ICollection<T>? Lista { get; set; }

    public long Total { get; set; }
}
 
