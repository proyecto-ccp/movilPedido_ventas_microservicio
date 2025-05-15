using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Puertos.Repositorios;

namespace Pedidos.Dominio.Servicios.DetallePedidos
{
    public class ObtenerDetallePedido(IDetallePedidoRepositorio detallePedidoRepositorio)
    {
        private readonly IDetallePedidoRepositorio _detallePedidoRepositorio = detallePedidoRepositorio;

        public async Task<List<DetallePedido>> ObtenerDetallePorPedido(Guid idPedido)
        {
            var detallePedido = await _detallePedidoRepositorio.ObtenerDetallePorPedido(idPedido);
            
            //if (detallePedido == null)
            //{
            //    throw new Exception("No se encontró el detalle del pedido.");
            //}

            //if (detallePedido.Count == 0)
            //{
            //    throw new Exception("No se encontraron detalles para el pedido.");
            //}

            return detallePedido;
        }
    }
}
