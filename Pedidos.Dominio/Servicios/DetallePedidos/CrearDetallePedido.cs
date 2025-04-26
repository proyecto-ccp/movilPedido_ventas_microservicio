using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Puertos.Repositorios;

namespace Pedidos.Dominio.Servicios.DetallePedidos
{
    public class CrearDetallePedido(IDetallePedidoRepositorio detallePedidoRepositorio)
    {
        private readonly IDetallePedidoRepositorio _detallePedidoRepositorio = detallePedidoRepositorio;

        public async Task<bool> Ejecutar(DetallePedido detallePedido)
        {
            if (ValidarDetalle(detallePedido))
            {
                detallePedido.Id = Guid.NewGuid();
                detallePedido.ValorTotal = detallePedido.Cantidad * detallePedido.PrecioUnitario;
                await _detallePedidoRepositorio.CrearDetallePedido(detallePedido);
            }
            else
            {
                throw new ArgumentException("El detalle del pedido no es válido.");
            }

            return true;
        }

        private bool ValidarDetalle(DetallePedido detallePedido)
        {
            if (detallePedido == null)
            {
                return false;
            }
            if (detallePedido.IdUsuario == Guid.Empty)
            {
                return false;
            }

            if (detallePedido.IdProducto == 0)
            {
                return false;
            }

            if (detallePedido.Cantidad <= 0)
            {
                return false;
            }

            if (detallePedido.PrecioUnitario <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
