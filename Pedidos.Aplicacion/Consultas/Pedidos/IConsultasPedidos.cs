using Pedidos.Aplicacion.Dto;

namespace Pedidos.Aplicacion.Consultas.Pedidos
{
    public interface IConsultasPedidos
    {
        public Task<PedidoOut> ObtenerPedidoPorId(Guid id);
        public Task<PedidoOutList> ObtenerPedidosPorClienteId(Guid clienteId, string estado);
        public Task<PedidoOutList> ObtenerPedidosPorVendedorId(Guid vendedorId, string estado);
    }
}
