using AutoMapper;
using Pedidos.Aplicacion.Clientes;
using Pedidos.Aplicacion.Dto;
using Pedidos.Aplicacion.Enum;
using Pedidos.Dominio.Entidades;
using Pedidos.Dominio.Servicios.Pedidos;
using System.Net;

namespace Pedidos.Aplicacion.Comandos.Pedidos
{
    public class ComandosPedido: IComandosPedido
    {
        private readonly CrearPedido _crearPedido;
        private readonly IMapper _mapper;

        public ComandosPedido(CrearPedido crearPedido,IMapper mapper)
        {
            _crearPedido = crearPedido;
            _mapper = mapper;
        }

        public async Task<BaseOut> CrearPedido(PedidoIn pedido)
        {
            BaseOut baseOut = new();

            try
            {
                var pedidoDominio = _mapper.Map<Pedido>(pedido);
                await _crearPedido.Ejecutar(pedidoDominio);
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
    }
}
