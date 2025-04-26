using AutoMapper;
using Pedidos.Aplicacion.Dto;
using Pedidos.Aplicacion.Enum;
using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Servicios.DetallePedidos;
using System.Net;

namespace Pedidos.Aplicacion.Comandos.DetallePedidos
{
    public class ComandosDetallePedido: IComandosDetallePedido
    {
        private readonly CrearDetallePedido _crearDetallePedido;
        private readonly EliminarDetallePedido _eliminarDetallePedido;
        private readonly ActualizarIdPedido _actualizarIdPedido;
        private readonly IMapper _mapper;

        public ComandosDetallePedido(CrearDetallePedido crearDetallePedido, EliminarDetallePedido eliminarDetallePedido, ActualizarIdPedido actualizarIdPedido ,IMapper mapper)
        {
            _crearDetallePedido = crearDetallePedido;
            _eliminarDetallePedido = eliminarDetallePedido;
            _actualizarIdPedido = actualizarIdPedido;
            _mapper = mapper;
        }

        public async Task<BaseOut> CrearDetallePedido(DetallePedidoIn detallePedido)
        {
            BaseOut baseOut = new();
            try
            {
                var detallepedidoDominio = _mapper.Map<DetallePedido>(detallePedido);
                await _crearDetallePedido.Ejecutar(detallepedidoDominio);
                baseOut.Mensaje = "Detalle agregado exitosamente";
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
        public async Task<BaseOut> EliminarDetallePedido(Guid idDetalle)
        {
            BaseOut baseOut = new();
            try
            {
                await _eliminarDetallePedido.Ejecutar(idDetalle);
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

        public async Task<BaseOut> ActualizarIdPedido(Guid idUsuario, Guid idPedido)
        {
            BaseOut baseOut = new();
            try
            {
                await _actualizarIdPedido.Ejecutar(idUsuario, idPedido);
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
