using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Puertos.Repositorios;

namespace Pedidos.Dominio.Servicios.Pedidos
{
    public class CrearPedido(IPedidoRepositorio pedidoRepositorio)
    {
        private readonly IPedidoRepositorio _pedidoRepositorio = pedidoRepositorio;
        
        public async Task<bool> Ejecutar(Pedido pedido) 
        {
            if(ValidarPedido(pedido))
            {
                pedido.Id = Guid.NewGuid();
                pedido.EstadoPedido = "CREADO";
                await _pedidoRepositorio.CrearPedido(pedido);
            }

            return true;
        }

        private bool ValidarPedido(Pedido pedido)
        {
            if (pedido == null || 
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
