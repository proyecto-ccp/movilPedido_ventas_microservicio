using Pedidos.Dominio.Entidades;

namespace Pedidos.Infraestructura.RepositoriosGenericos.DetallePedidos
{
    public interface IRepositorioBaseDetallePedido<T> : IDisposable where T : EntidadBase
    {
        Task<T> Crear(T entity);
        Task<T> BuscarPorLlave(object ValueKey);
        Task<List<T>> BuscarPorAtributo(string ValueAttribute, string Attribute);
        Task<List<T>> BuscarPorAtributo(Guid ValueAttribute, string Attribute);
        Task<List<T>> DarListado();
        Task<T> Eliminar(object ValueKey);
    }
}
