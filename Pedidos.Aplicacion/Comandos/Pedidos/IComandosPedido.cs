using Pedidos.Aplicacion.Dto;

namespace Pedidos.Aplicacion.Comandos.Pedidos
{
    public interface IComandosPedido
    {
        Task<BaseOut> CrearPedido(PedidoIn pedido);
        Task<BaseOut> ActualizarPedido(PedidoActualizarIn pedido, Guid id);
    }
}
