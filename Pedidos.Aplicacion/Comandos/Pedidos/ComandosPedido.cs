using AutoMapper;
using Pedidos.Aplicacion.Clientes;
using Pedidos.Aplicacion.Dto;
using Pedidos.Aplicacion.Enum;
using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Servicios.Pedidos;
using System.Net;
using System.Text.Json;

namespace Pedidos.Aplicacion.Comandos.Pedidos
{
    public class ComandosPedido: IComandosPedido
    {
        private readonly CrearPedido _crearPedido;
        private readonly ActualizarPedido _actualizarPedido;
        private readonly IMapper _mapper;
        private readonly IAuditoriaApiClient _auditoriaApiClient;
        public ComandosPedido(CrearPedido crearPedido, ActualizarPedido actualizarPedido,IMapper mapper, IAuditoriaApiClient auditoriaApiClient)
        {
            _crearPedido = crearPedido;
            _actualizarPedido = actualizarPedido;
            _auditoriaApiClient = auditoriaApiClient;
            _mapper = mapper;
        }

        public async Task<BaseOut> CrearPedido(PedidoIn pedido, Guid userId)
        {
            BaseOut baseOut = new();

            try
            {
                var pedidoDominio = _mapper.Map<Pedido>(pedido);
                await _crearPedido.Ejecutar(pedidoDominio);

                _ = Task.Run(() => _auditoriaApiClient.RegistrarAuditoria(new AuditoriaDto
                {
                    IdUsuario = userId,
                    Accion = "Crear Pedido",
                    TablaAfectada = "tbl_pedido",
                    Idregistro = pedidoDominio.Id.ToString(),
                    Registro = JsonSerializer.Serialize(pedidoDominio),
                }));

                baseOut.Mensaje = "Pedido creado exitosamente";
                baseOut.Id = pedidoDominio.Id;
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

        public async Task<BaseOut> ActualizarPedido(PedidoActualizarIn pedido, Guid id, Guid userId)
        {
            BaseOut baseOut = new();
            try
            {
                var pedidoDominio = _mapper.Map<Pedido>(pedido);
                pedidoDominio.Id = id;
                await _actualizarPedido.Ejecutar(pedidoDominio);

                _ = Task.Run(() => _auditoriaApiClient.RegistrarAuditoria(new AuditoriaDto
                {
                    IdUsuario = userId,
                    Accion = "Actualizar Pedido",
                    TablaAfectada = "tbl_pedido",
                    Idregistro = id.ToString(),
                    Registro = JsonSerializer.Serialize(pedidoDominio),
                }));

                baseOut.Mensaje = "Pedido actualizado exitosamente";
                baseOut.Id = pedidoDominio.Id;
                baseOut.Resultado = Resultado.Exitoso;
                baseOut.Status = HttpStatusCode.OK;
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
