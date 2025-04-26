using Pedidos.Aplicacion.Dto;

namespace Pedidos.Aplicacion.Consultas.DetallePedidos
{
    internal interface IConsultasDetallePedido
    {
        public Task<DetallePedidoOutList> ObtenerDetallePorPedido(Guid idPedido);
    }
}
