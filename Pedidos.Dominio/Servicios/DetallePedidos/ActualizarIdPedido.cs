using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Puertos.Repositorios;

namespace Pedidos.Dominio.Servicios.DetallePedidos
{
    public class ActualizarIdPedido(IDetallePedidoRepositorio detallePedidoRepositorio)
    {
        private readonly IDetallePedidoRepositorio _detallePedidoRepositorio = detallePedidoRepositorio;

        public async Task<bool> Ejecutar(Guid idUsuario, Guid idPedido)
        {
            if (idUsuario != Guid.Empty && idPedido != Guid.Empty)
            {
                await _detallePedidoRepositorio.ActualizarIdPedido(idUsuario, idPedido);
            }
            else
            {
                throw new ArgumentException("No es posible actualizar los detalles con base al pedido indicado.");
            }

            return true;
        }
    }
}
