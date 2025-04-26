using Pedidos.Aplicacion.Dto;

namespace Pedidos.Aplicacion.Consultas.DetallePedidos
{
    public interface IConsultasDetallePedido
    {
        public Task<DetallePedidoOutList> ObtenerDetallePorPedido(Guid idPedido);
        public Task<DetallePedidoOutList> ObtenerDetallePorPedidoUsuario(Guid idUsuario);
    }
}
