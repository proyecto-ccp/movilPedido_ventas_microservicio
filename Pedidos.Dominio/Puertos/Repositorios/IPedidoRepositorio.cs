using Pedidos.Dominio.Entidades;

namespace Pedidos.Dominio.Puertos.Repositorios
{
    public interface IPedidoRepositorio
    {
        Task CrearPedido(Pedido pedido);
        Task ActualizarPedido(Pedido pedido);
        Task<Pedido> ObtenerPedido(Guid id);
        Task<List<Pedido>> ObtenerPedidosPorCliente(Guid idCliente, string estado);
        Task<List<Pedido>> ObtenerPedidosPorEntrega(string strEstado);
        Task<List<Pedido>> ObtenerPedidosPorVendedor(Guid idVendedor, string estado);
    }
}
