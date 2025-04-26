using Pedidos.Dominio.Entidades;

namespace Pedidos.Infraestructura.RepositoriosGenericos.Pedidos
{
    public interface IRepositorioBasePedido<T> : IDisposable where T : EntidadBase
    {
        Task<T> Crear(T entity);
        Task<T> BuscarPorLlave(object ValueKey);
        Task<List<T>> BuscarPorAtributo(string ValueAttribute, string Attribute);
        Task<List<T>> BuscarPorAtributo(Guid ValueAttribute, string Attribute);
        Task<List<T>> DarListado();
    }
}
