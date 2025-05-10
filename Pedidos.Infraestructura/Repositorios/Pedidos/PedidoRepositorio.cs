using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Puertos.Repositorios;
using Pedidos.Infraestructura.RepositoriosGenericos.Pedidos;

namespace Pedidos.Infraestructura.Repositorios.Pedidos
{
    public class PedidoRepositorio: IPedidoRepositorio
    {
        private readonly IRepositorioBasePedido<Pedido> _repositorioBase;

        public PedidoRepositorio(IRepositorioBasePedido<Pedido> repositorioBase)
        {
            _repositorioBase = repositorioBase;
        }

        public async Task CrearPedido(Pedido pedido)
        {
            await _repositorioBase.Crear(pedido);
        }

        public async Task ActualizarPedido(Pedido pedido)
        {
            await _repositorioBase.Actualizar(pedido);
        }

        public async Task<Pedido> ObtenerPedido(Guid id)
        {
            return await _repositorioBase.BuscarPorLlave(id);
        }

        public async Task<List<Pedido>> ObtenerPedidosPorCliente(Guid idCliente, string estado)
        {
            return await _repositorioBase.BuscarPorAtributo(idCliente, "IdCliente");
        }

        public async Task<List<Pedido>> ObtenerPedidosPorVendedor(Guid idVendedor, string estado)
        {
            return await _repositorioBase.BuscarPorAtributo(idVendedor, "IdVendedor");
        }

        public async Task<List<Pedido>> ObtenerPedidosPorEntrega(string strEstado)
        {
            return await _repositorioBase.BuscarPendientePorEntregar(strEstado);
        }
    }
    
}
