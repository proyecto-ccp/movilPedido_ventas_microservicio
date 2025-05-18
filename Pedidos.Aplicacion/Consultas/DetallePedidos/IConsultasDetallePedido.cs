using Pedidos.Aplicacion.Dto;

namespace Pedidos.Aplicacion.Consultas.DetallePedidos
{
    public interface IConsultasDetallePedido
    {
        public Task<DetallePedidoOutList> ObtenerDetallePorPedido(Guid idPedido, string authorization);
        public Task<DetallePedidoOutList> ObtenerDetallePorPedidoUsuario(Guid idUsuario, string authorization);
    }
}
