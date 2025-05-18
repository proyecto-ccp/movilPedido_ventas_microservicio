using AutoMapper;
using Pedidos.Aplicacion.Clientes;
using Pedidos.Aplicacion.Dto;
using Pedidos.Aplicacion.Enum;
using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Servicios.DetallePedidos;
using Pedidos.Dominio.Servicios.Pedidos;
using System.Net;
using System.Text.Json;

namespace Pedidos.Aplicacion.Comandos.DetallePedidos
{
    public class ComandosDetallePedido: IComandosDetallePedido
    {
        private readonly CrearDetallePedido _crearDetallePedido;
        private readonly EliminarDetallePedido _eliminarDetallePedido;
        private readonly ActualizarIdPedido _actualizarIdPedido;
        private readonly ObtenerDetallePedido _obtenerDetallePedido;
        private readonly IMapper _mapper;
        private readonly IInventariosApiClient _inventariosApiClient;
        private readonly IAuditoriaApiClient _auditoriaApiClient;
        private readonly ActualizarPedido _actualizarPedido;
        private readonly ObtenerPedido _obtenerPedido;

        public ComandosDetallePedido(CrearDetallePedido crearDetallePedido, EliminarDetallePedido eliminarDetallePedido, ActualizarIdPedido actualizarIdPedido ,IMapper mapper, IInventariosApiClient inventariosApiClient, ObtenerDetallePedido obtenerDetallePedido, ActualizarPedido actualizarPedido, ObtenerPedido obtenerPedido, IAuditoriaApiClient auditoriaApiClient)
        {
            _crearDetallePedido = crearDetallePedido;
            _eliminarDetallePedido = eliminarDetallePedido;
            _actualizarIdPedido = actualizarIdPedido;
            _mapper = mapper;
            _inventariosApiClient = inventariosApiClient;
            _obtenerDetallePedido = obtenerDetallePedido;
            _actualizarPedido = actualizarPedido;
            _obtenerPedido = obtenerPedido;
            _auditoriaApiClient = auditoriaApiClient;
        }

        public async Task<BaseOut> CrearDetallePedido(DetallePedidoIn detallePedido, Guid userId)
        {
            BaseOut baseOut = new();
            try
            {
                var detallepedidoDominio = _mapper.Map<DetallePedido>(detallePedido);
                await _crearDetallePedido.Ejecutar(detallepedidoDominio);

                _ = Task.Run(() => _auditoriaApiClient.RegistrarAuditoria(new AuditoriaDto
                {
                    IdUsuario = userId,
                    Accion = "Crear Detalle Pedido",
                    TablaAfectada = "tbl_detalle_pedido",
                    Idregistro = detallepedidoDominio.Id.ToString(),
                    Registro = JsonSerializer.Serialize(detallepedidoDominio),
                }));

                baseOut.Mensaje = "Detalle agregado exitosamente";
                baseOut.Resultado = Resultado.Exitoso;
                baseOut.Status = HttpStatusCode.Created;
                baseOut.Id = detallepedidoDominio.Id;
            }
            catch (Exception ex)
            {
                baseOut.Resultado = Resultado.Error;
                baseOut.Mensaje = ex.Message;
                baseOut.Status = HttpStatusCode.InternalServerError;
            }

            return baseOut;
        }
        public async Task<BaseOut> EliminarDetallePedido(Guid idDetalle, Guid userId)
        {
            BaseOut baseOut = new();
            try
            {
                await _eliminarDetallePedido.Ejecutar(idDetalle);

                _ = Task.Run(() => _auditoriaApiClient.RegistrarAuditoria(new AuditoriaDto
                {
                    IdUsuario = userId,
                    Accion = "Eliminar DetallePedido",
                    TablaAfectada = "tbl_detalle_pedido",
                    Idregistro = idDetalle.ToString(),
                    Registro = idDetalle.ToString(),
                }));

                baseOut.Mensaje = "Detalle retirado exitosamente";
                baseOut.Resultado = Resultado.Exitoso;
                baseOut.Status = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                baseOut.Resultado = Resultado.Error;
                baseOut.Mensaje = ex.Message;
                baseOut.Status = HttpStatusCode.InternalServerError;
            }

            return baseOut;
        }

        public async Task<BaseOut> ActualizarIdPedido(Guid idUsuario, Guid idPedido, string authorization, Guid userId)
        {
            BaseOut baseOut = new();
            try
            {
                await _actualizarIdPedido.Ejecutar(idUsuario, idPedido);

                var response = await _obtenerDetallePedido.ObtenerDetallePorPedido(idPedido);
                if (response != null && response.Count > 0)
                {
                    foreach (var detalle in response)
                    {
                        var inventarioResponse = await _inventariosApiClient.RetirarInventarioAsync(detalle.IdProducto, detalle.Cantidad, authorization);

                        if (inventarioResponse.Resultado != ((int)Resultado.Exitoso))
                        {
                            baseOut.Resultado = Resultado.Error;
                            baseOut.Mensaje = "Error al actualizar inventario";
                            baseOut.Status = HttpStatusCode.InternalServerError;
                            return baseOut;
                        }
                    }

                    var pedido = await _obtenerPedido.ObtenerPedidoPorId(idPedido);
                    if (pedido != null)
                    {
                        pedido.EstadoPedido = "CONFIRMADO";
                        await _actualizarPedido.Ejecutar(pedido);
                    }
                }

                _ = Task.Run(() => _auditoriaApiClient.RegistrarAuditoria(new AuditoriaDto
                {
                    IdUsuario = userId,
                    Accion = "Actualizar Detalle Pedido",
                    TablaAfectada = "tbl_detalle_pedido",
                    Idregistro = "Varios registros",
                    Registro = "idUsuario: " + idUsuario + " idPedido: " + idPedido,
                }));

                baseOut.Mensaje = "Detalles actualizados exitosamente";
                baseOut.Resultado = Resultado.Exitoso;
                baseOut.Status = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                baseOut.Resultado = Resultado.Error;
                baseOut.Mensaje = ex.Message;
                baseOut.Status = HttpStatusCode.InternalServerError;
            }

            return baseOut;
        }
    }
}
