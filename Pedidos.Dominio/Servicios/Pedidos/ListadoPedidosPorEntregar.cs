using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Puertos.Repositorios;

namespace Pedidos.Dominio.Servicios.Pedidos
{
    public class ListadoPedidosPorEntregar(IPedidoRepositorio pedidoRepositorio)
    {
        private readonly IPedidoRepositorio _pedidoRepositorio = pedidoRepositorio;
        public async Task<List<Pedido>> ObtenerPedidosPorEntregar(string strEstado)
        {
            var pedidos = await _pedidoRepositorio.ObtenerPedidosPorEntrega(strEstado);
            return pedidos;
        }
    }
}
