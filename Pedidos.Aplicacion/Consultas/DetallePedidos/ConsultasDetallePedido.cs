using AutoMapper;
using Pedidos.Aplicacion.Clientes;
using Pedidos.Aplicacion.Dto;
using Pedidos.Aplicacion.Enum;
using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Puertos.Repositorios;
using Pedidos.Dominio.Servicios.DetallePedidos;
using System.Net;

namespace Pedidos.Aplicacion.Consultas.DetallePedidos
{
    public class ConsultasDetallePedido : IConsultasDetallePedido
    {
        private readonly ObtenerDetallePedido _obtenerDetallePedido;
        private readonly ObtenerDetallePedidoUsuario _obtenerDetallePedidoUsuario;
        private readonly IMapper _mapper;
        private readonly IProductosApiClient _productosApiClient;
        public ConsultasDetallePedido(IDetallePedidoRepositorio detallePedidoRepositorio, IMapper mapper, IProductosApiClient productosApiClient)
        {
            _obtenerDetallePedido = new ObtenerDetallePedido(detallePedidoRepositorio);
            _obtenerDetallePedidoUsuario = new ObtenerDetallePedidoUsuario(detallePedidoRepositorio);
            _productosApiClient = productosApiClient;
            _mapper = mapper;
        }

        private void DiligenciarProducto(DetallePedido detalle)
        {
            if (detalle.IdProducto != null && detalle.IdProducto > 0)
            {
                var producto = _productosApiClient.ObtenerProductoPorIdAsync(detalle.IdProducto).Result;
                if (producto != null)
                {
                    detalle.NombreProducto = producto.Nombre;
                    detalle.UrlFotoProducto1 = producto.urlFoto1;
                    detalle.UrlFotoProducto2 = producto.urlFoto2;
                }
            }
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
                    output.Status = HttpStatusCode.NotFound;
                }
                else
                {
                    foreach(var detallePedido in DetallesPedido)
                    {
                        DiligenciarProducto(detallePedido);
                    }

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


        public async Task<DetallePedidoOutList> ObtenerDetallePorPedidoUsuario(Guid idUsuario)
        {
            DetallePedidoOutList output = new()
            {
                DetallePedidos = []
            };

            try
            {
                var DetallesPedido = await _obtenerDetallePedidoUsuario.ObtenerDetallePorPedidoUsuario(idUsuario);

                if (DetallesPedido == null || DetallesPedido.Count == 0)
                {
                    output.Resultado = Resultado.SinRegistros;
                    output.Mensaje = "No se encontraron DetallePedidos";
                    output.Status = HttpStatusCode.NoContent;
                }
                else
                {
                    foreach (var detallePedido in DetallesPedido)
                    {
                        DiligenciarProducto(detallePedido);
                    }

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
