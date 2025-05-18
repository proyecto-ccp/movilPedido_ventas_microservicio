using Microsoft.AspNetCore.Mvc;
using Pedidos.Aplicacion.Comandos.DetallePedidos;
using Pedidos.Aplicacion.Consultas.DetallePedidos;
using Pedidos.Aplicacion.Dto;
using ServicioPedido.Helpers;

namespace ServicioPedido.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
    public class DetallePedidoController : ControllerBase
    {
        private readonly IComandosDetallePedido _comandosDetallePedido;
        private readonly IConsultasDetallePedido _consultasDetallePedido;
        public DetallePedidoController(IComandosDetallePedido comandosDetallePedido, IConsultasDetallePedido consultasDetallePedido)
        {
            _comandosDetallePedido = comandosDetallePedido;
            _consultasDetallePedido = consultasDetallePedido;
        }
        [HttpPost]
        [Route("AgregarDetalle")]
        [ProducesResponseType(typeof(DetallePedidoOut), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> AgregarDetalle([FromBody] DetallePedidoIn detallePedidoIn)
        {
            try
            {
                var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                var resultado = await _comandosDetallePedido.CrearDetallePedido(detallePedidoIn, userId);

                if (resultado.Resultado != Pedidos.Aplicacion.Enum.Resultado.Error)
                    return Ok(resultado);
                else
                    return Problem(resultado.Mensaje, statusCode: (int)resultado.Status, title: resultado.Resultado.ToString(), type: resultado.Resultado.ToString(), instance: HttpContext.Request.Path);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("EliminarDetalle/{idDetalle}")]
        [ProducesResponseType(typeof(DetallePedidoOut), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> EliminarDetallePedido(Guid idDetalle)
        {
            try
            {
                var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                var resultado = await _comandosDetallePedido.EliminarDetallePedido(idDetalle, userId);
                if (resultado.Resultado != Pedidos.Aplicacion.Enum.Resultado.Error)
                    return Ok(resultado);
                else
                    return Problem(resultado.Mensaje, statusCode: (int)resultado.Status, title: resultado.Resultado.ToString(), type: resultado.Resultado.ToString(), instance: HttpContext.Request.Path);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("ActualizarDetalles/{idUsuario}/{idPedido}")]
        [ProducesResponseType(typeof(DetallePedidoOut), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ActualizarDetallesPedido(Guid idUsuario,Guid idPedido)
        {
            try
            {
                var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                var resultado = await _comandosDetallePedido.ActualizarIdPedido(idUsuario,idPedido, HttpContext.Request.Headers.Authorization, userId);
                if (resultado.Resultado == Pedidos.Aplicacion.Enum.Resultado.Exitoso)
                    return Ok(resultado);
                else
                    return Problem(resultado.Mensaje, statusCode: (int)resultado.Status, title: resultado.Resultado.ToString(), type: resultado.Resultado.ToString(), instance: HttpContext.Request.Path);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ObtenerDetalles/{idPedido}")]
        [ProducesResponseType(typeof(DetallePedidoOutList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ObtenerCliente(Guid idPedido)
        {
            try
            {
                var resultado = await _consultasDetallePedido.ObtenerDetallePorPedido(idPedido, HttpContext.Request.Headers.Authorization);
                if (resultado.Resultado == Pedidos.Aplicacion.Enum.Resultado.Exitoso)
                    return Ok(resultado);
                else
                    return Problem(resultado.Mensaje, statusCode: (int)resultado.Status, title: resultado.Resultado.ToString(), type: resultado.Resultado.ToString(), instance: HttpContext.Request.Path);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ObtenerDetallesUsuario/{idUsuario}")]
        [ProducesResponseType(typeof(DetallePedidoOutList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ObtenerDetallesUsuario(Guid idUsuario)
        {
            try
            {
                var resultado = await _consultasDetallePedido.ObtenerDetallePorPedidoUsuario(idUsuario, HttpContext.Request.Headers.Authorization);
                if (resultado.Resultado == Pedidos.Aplicacion.Enum.Resultado.Exitoso)
                    return Ok(resultado);
                else
                    return Problem(resultado.Mensaje, statusCode: (int)resultado.Status, title: resultado.Resultado.ToString(), type: resultado.Resultado.ToString(), instance: HttpContext.Request.Path);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
