using AutoMapper;
using Pedidos.Aplicacion.Dto;
using Pedidos.Aplicacion.Enum;
using Pedidos.Dominio.Puertos.Repositorios;
using Pedidos.Dominio.Servicios.DetallePedidos;
using System.Net;

namespace Pedidos.Aplicacion.Consultas.DetallePedidos
{
    public class ConsultasDetallePedido: IConsultasDetallePedido
    {
        private readonly ObtenerDetallePedido _obtenerDetallePedido;
        private readonly IMapper _mapper;
        public ConsultasDetallePedido(IDetallePedidoRepositorio detallePedidoRepositorio, IMapper mapper)
        {
            _obtenerDetallePedido = new ObtenerDetallePedido(detallePedidoRepositorio);
            _mapper = mapper;
        }

        public async Task<DetallePedidoOutList> ObtenerDetallePorPedido(Guid idPedido)
        {
            DetallePedidoOutList output = new()
            {
                DetallePedidos = []
            };

            try
            {
                var DetallesPedido = await _obtenerDetallePedido.ObtenerDetallePorPedido(idPedido);

                if (DetallesPedido == null || DetallesPedido.Count == 0)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "No se encontraron DetallePedidos";
                    output.Status = HttpStatusCode.NoContent;
                }
                else
                {
                    output.Resultado = Resultado.Exitoso;
                    output.Mensaje = "DetallePedidos encontrados";
                    output.Status = HttpStatusCode.OK;
                    output.DetallePedidos = _mapper.Map<List<DetallePedidoDto>>(DetallesPedido);
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
