using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Puertos.Repositorios;

namespace Pedidos.Dominio.Servicios.DetallePedidos
{
    public class ObtenerDetallePedidoUsuario(IDetallePedidoRepositorio detallePedidoRepositorio)
    {
        private readonly IDetallePedidoRepositorio _detallePedidoRepositorio = detallePedidoRepositorio;

        public async Task<List<DetallePedido>> ObtenerDetallePorPedidoUsuario(Guid idUsuario)
        {
            var detallePedido = await _detallePedidoRepositorio.ObtenerDetallePorPedidoUsuario(idUsuario);
            
            if (detallePedido == null)
            {
                throw new Exception("No se encontró el detalle del pedido.");
            }

            if (detallePedido.Count == 0)
            {
                throw new Exception("No se encontraron detalles para el pedido.");
            }

            return detallePedido;
        }
    }
}
