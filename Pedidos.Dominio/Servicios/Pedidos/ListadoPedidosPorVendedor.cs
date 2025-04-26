using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Puertos.Repositorios;

namespace Pedidos.Dominio.Servicios.Pedidos
{
    public class ListadoPedidosPorVendedor(IPedidoRepositorio pedidoRepositorio)
    {
        private readonly IPedidoRepositorio _pedidoRepositorio = pedidoRepositorio;
        public async Task<List<Pedido>> ObtenerPedidosPorVendedor(Guid idVendedor, string estado)
        {
            var pedidos = await _pedidoRepositorio.ObtenerPedidosPorVendedor(idVendedor, estado);
            return pedidos;
        }
    }
}
