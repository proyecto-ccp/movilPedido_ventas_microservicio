using Pedidos.Aplicacion.Dto;

namespace Pedidos.Aplicacion.Comandos.DetallePedidos
{
    public interface IComandosDetallePedido
    {
        Task<BaseOut> CrearDetallePedido(DetallePedidoIn detallePedido, Guid userId);
        Task<BaseOut> EliminarDetallePedido(Guid idDetalle, Guid userId);
        Task<BaseOut> ActualizarIdPedido(Guid idUsuario, Guid idPedido, string authorization, Guid userId);
    }
}
