using Pedidos.Dominio.Puertos.Repositorios;

namespace Pedidos.Dominio.Servicios.DetallePedidos
{
    public class EliminarDetallePedido(IDetallePedidoRepositorio detallePedidoRepositorio)
    {
        private readonly IDetallePedidoRepositorio _detallePedidoRepositorio = detallePedidoRepositorio;

        public async Task Ejecutar(Guid idDetalle)
        {
            // Eliminar el detalle de pedido
            await _detallePedidoRepositorio.EliminarDetallePedido(idDetalle);
        }
    }
}
