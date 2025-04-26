using AutoMapper;
using Pedidos.Aplicacion.Dto;
using Pedidos.Aplicacion.Enum;
using Pedidos.Dominio.Puertos.Repositorios;
using Pedidos.Dominio.Servicios.Pedidos;
using System.Net;

namespace Pedidos.Aplicacion.Consultas.Pedidos
{
    public class ConsultasPedidos: IConsultasPedidos
    {
        private readonly ObtenerPedido _obtenerPedido;
        private readonly ListadoPedidosPorCliente _listadoPedidosPorCliente;
        private readonly ListadoPedidosPorVendedor _listadoPedidosPorVendedor;
        private readonly IMapper _mapper;
        public ConsultasPedidos(IPedidoRepositorio pedidoRepositorio, IMapper mapper)
        {
            _obtenerPedido = new ObtenerPedido(pedidoRepositorio);
            _listadoPedidosPorCliente = new ListadoPedidosPorCliente(pedidoRepositorio);
            _listadoPedidosPorVendedor = new ListadoPedidosPorVendedor(pedidoRepositorio);
            _mapper = mapper;
        }

        public async Task<PedidoOut> ObtenerPedidoPorId(Guid id)
        {
            PedidoOut PedidoOut = new();
            try
            {
                var Pedido = await _obtenerPedido.ObtenerPedidoPorId(id);

                if (Pedido == null || Pedido.Id == Guid.Empty)
                {
                    PedidoOut.Resultado = Resultado.SinRegistros;
                    PedidoOut.Mensaje = "Pedido NO encontrado";
                    PedidoOut.Status = HttpStatusCode.NoContent;
                }
                else
                {
                    PedidoOut.Resultado = Resultado.Exitoso;
                    PedidoOut.Mensaje = "Pedido encontrado";
                    PedidoOut.Status = HttpStatusCode.OK;
                    PedidoOut.Pedido = _mapper.Map<PedidoDto>(Pedido);
                }
            }
            catch (Exception ex)
            {
                PedidoOut.Resultado = Resultado.Error;
                PedidoOut.Mensaje = ex.Message;
                PedidoOut.Status = HttpStatusCode.InternalServerError;
            }

            return PedidoOut;
        }

        public async Task<PedidoOutList> ObtenerPedidosPorClienteId(Guid idCliente, string estado)
        {
            PedidoOutList output = new()
            {
                Pedidos = []
            };

            try
            {
                var Pedidos = await _listadoPedidosPorCliente.ObtenerPedidosPorCliente(idCliente, estado);

                if (Pedidos == null || Pedidos.Count == 0)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "No se encontraron pedidos para el cliente";
                    output.Status = HttpStatusCode.NoContent;
                }
                else
                { 
                    output.Resultado = Resultado.Exitoso;
                    output.Mensaje = "Pedidos encontrados en la zona";
                    output.Status = HttpStatusCode.OK;
                    output.Pedidos = _mapper.Map<List<PedidoDto>>(Pedidos);
                }
            }
            catch (Exception ex)
            {
                output.Resultado = Resultado.Error;
                output.Mensaje = ex.Message;
                output.Status = HttpStatusCode.InternalServerError;
            }

            return output;
        }

        public async Task<PedidoOutList> ObtenerPedidosPorVendedorId(Guid idVendedor, string estado)
        {
            PedidoOutList output = new()
            {
                Pedidos = []
            };

            try
            {
                var Pedidos = await _listadoPedidosPorVendedor.ObtenerPedidosPorVendedor(idVendedor, estado);

                if (Pedidos == null || Pedidos.Count == 0)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "No se encontraron pedidos para el vendedor";
                    output.Status = HttpStatusCode.NoContent;
                }
                else
                {
                    output.Resultado = Resultado.Exitoso;
                    output.Mensaje = "Pedidos encontrados en la zona";
                    output.Status = HttpStatusCode.OK;
                    output.Pedidos = _mapper.Map<List<PedidoDto>>(Pedidos);
                }
            }
            catch (Exception ex)
            {
                output.Resultado = Resultado.Error;
                output.Mensaje = ex.Message;
                output.Status = HttpStatusCode.InternalServerError;
            }

            return output;
        }
    }
}
