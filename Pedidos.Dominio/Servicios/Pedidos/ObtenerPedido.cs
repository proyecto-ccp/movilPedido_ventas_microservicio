
using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Puertos.Repositorios;

namespace Pedidos.Dominio.Servicios.Pedidos
{
    public class ObtenerPedido(IPedidoRepositorio pedidoRepositorio)
    {
        private readonly IPedidoRepositorio _pedidoRepositorio = pedidoRepositorio;

        public async Task<Pedido> ObtenerPedidoPorId(Guid id)
        {
            var pedido = await _pedidoRepositorio.ObtenerPedido(id);            
            return pedido;
        }
    }
}
