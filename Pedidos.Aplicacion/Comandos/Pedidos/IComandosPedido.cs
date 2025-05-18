using Pedidos.Aplicacion.Dto;

namespace Pedidos.Aplicacion.Comandos.Pedidos
{
    public interface IComandosPedido
    {
        Task<BaseOut> CrearPedido(PedidoIn pedido,Guid userId);
        Task<BaseOut> ActualizarPedido(PedidoActualizarIn pedido, Guid id, Guid userId);
    }
}
