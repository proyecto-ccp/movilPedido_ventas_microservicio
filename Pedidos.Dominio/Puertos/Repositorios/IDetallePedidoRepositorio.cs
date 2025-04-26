using Pedidos.Dominio.Entidades;

namespace Pedidos.Dominio.Puertos.Repositorios
{
    public interface IDetallePedidoRepositorio
    {
        Task CrearDetallePedido(DetallePedido detallePedido);
        Task<List<DetallePedido>> ObtenerDetallePorPedido(Guid idPedido);
        Task<List<DetallePedido>> ObtenerDetallePorPedidoUsuario(Guid idUsuario);
        Task EliminarDetallePedido(Guid idDetalle);
    }
}
