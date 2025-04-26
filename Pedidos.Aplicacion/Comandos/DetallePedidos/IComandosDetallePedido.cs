using Pedidos.Aplicacion.Dto;

namespace Pedidos.Aplicacion.Comandos.DetallePedidos
{
    public interface IComandosDetallePedido
    {
        Task<BaseOut> CrearDetallePedido(DetallePedidoIn detallePedido);
        Task<BaseOut> EliminarDetallePedido(Guid idDetalle);
        Task<BaseOut> ActualizarIdPedido(Guid idUsuario, Guid idPedido);
    }
}
