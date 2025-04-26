using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Puertos.Repositorios;
using Pedidos.Infraestructura.RepositoriosGenericos.DetallePedidos;

namespace Pedidos.Infraestructura.Repositorios.DetallePedidos
{
    public class DetallePedidoRepositorio: IDetallePedidoRepositorio
    {
        private readonly IRepositorioBaseDetallePedido<DetallePedido> _repositorioBaseDetallePedido;

        public DetallePedidoRepositorio(IRepositorioBaseDetallePedido<DetallePedido> repositorioBaseDetallePedido)
        {
            _repositorioBaseDetallePedido = repositorioBaseDetallePedido;
        }

        public async Task CrearDetallePedido(DetallePedido detallePedido)
        {
            await _repositorioBaseDetallePedido.Crear(detallePedido);
        }

        public async Task EliminarDetallePedido(Guid idDetalle)
        {
            await _repositorioBaseDetallePedido.Eliminar(idDetalle);
        }

        public async Task<List<DetallePedido>> ObtenerDetallePorPedido(Guid idPedido)
        {
            return await _repositorioBaseDetallePedido.BuscarPorAtributo(idPedido, "IdPedido");
        }

        public async Task<List<DetallePedido>> ObtenerDetallePorPedidoUsuario(Guid idUsuario)
        {
            return await _repositorioBaseDetallePedido.BuscarPorAtributo(idUsuario, "IdUsuario");
        }
    }
}
