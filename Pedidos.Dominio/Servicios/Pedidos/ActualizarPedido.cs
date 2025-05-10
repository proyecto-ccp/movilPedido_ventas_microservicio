using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Puertos.Repositorios;

namespace Pedidos.Dominio.Servicios.Pedidos
{
    public class ActualizarPedido(IPedidoRepositorio pedidoRepositorio)
    {
        private readonly IPedidoRepositorio _pedidoRepositorio = pedidoRepositorio;

        public async Task<bool> Ejecutar(Pedido pedido)
        {
            if (ValidarPedido(pedido))
            {
                //pedido.FechaRealizado = DateTime.Now;
                await _pedidoRepositorio.ActualizarPedido(pedido);
            }
            return true;
        }

        private bool ValidarPedido(Pedido pedido)
        {
            if (pedido == null ||
                pedido.Id == Guid.Empty ||
                pedido.IdCliente == Guid.Empty ||
                pedido.FechaEntrega == null ||
                pedido.ValorTotal == 0 ||
                pedido.IdVendedor == null)
            {
                throw new InvalidOperationException("Valores incorrectos para los parametros minimos.");
            }
            return true;
        }
    }
}
