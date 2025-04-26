using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Puertos.Repositorios;

namespace Pedidos.Dominio.Servicios.Pedidos
{
    public class ListadoPedidosPorCliente(IPedidoRepositorio pedidoRepositorio)
    {
        private readonly IPedidoRepositorio _pedidoRepositorio = pedidoRepositorio;

        public async Task<List<Pedido>> ObtenerPedidosPorCliente(Guid idCliente, string estado)
        {
            var pedidos = await _pedidoRepositorio.ObtenerPedidosPorCliente(idCliente, estado);
            return pedidos;
        }
    }
}
