using Microsoft.AspNetCore.Mvc;
using Pedidos.Aplicacion.Comandos.Pedidos;
using Pedidos.Aplicacion.Consultas.Pedidos;
using Pedidos.Aplicacion.Dto;
using ServicioPedido.Helpers;

namespace ServicioPedido.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
    public class PedidoController : ControllerBase
    {
        private readonly IComandosPedido _comandosPedido;
        private readonly IConsultasPedidos _consultasPedidos;

        public PedidoController(IComandosPedido comandosPedido, IConsultasPedidos consultasPedidos)
        {
            _comandosPedido = comandosPedido;
            _consultasPedidos = consultasPedidos;
        }

        [HttpPost]
        [Route("CrearPedido")]
        [ProducesResponseType(typeof(PedidoOut), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> CrearPedido([FromBody] PedidoIn PedidoIn)
        {
            try
            {
                var resultado = await _comandosPedido.CrearPedido(PedidoIn);

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
        [Route("ActualizarPedido/{id}")]
        [ProducesResponseType(typeof(PedidoOut), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ActualizarPedido(Guid id, [FromBody] PedidoActualizarIn PedidoIn)
        {
            try
            {
                var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
                var resultado = await _comandosPedido.ActualizarPedido(PedidoIn,id,userId);
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

        [HttpGet]
        [Route("ObtenerPedido/{id}")]
        [ProducesResponseType(typeof(PedidoOut), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ObtenerPedido(Guid id)
        {
            try
            {
                var resultado = await _consultasPedidos.ObtenerPedidoPorId(id);
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
        [Route("ObtenerPedidosPorVendedor/{idVendedor}/{estado}")]
        [ProducesResponseType(typeof(PedidoOutList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ObtenerPedidosPorVendedor(Guid idVendedor,string estado)
        {
            try
            {
                var resultado = await _consultasPedidos.ObtenerPedidosPorVendedorId(idVendedor, estado);
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
        [Route("ObtenerPedidosPorCliente/{idCliente}/{estado}")]
        [ProducesResponseType(typeof(PedidoOutList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ObtenerPedidosPorCliente(Guid idCliente, string estado)
        {
            try
            {
                var resultado = await _consultasPedidos.ObtenerPedidosPorClienteId(idCliente, estado);
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
        [Route("ObtenerPedidosPorEstado/{estado}")]
        [ProducesResponseType(typeof(PedidoOutList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> ObtenerPedidosPorEstado(string estado)
        {
            try
            {
                var resultado = await _consultasPedidos.ObtenerPedidosPorEstado(estado);
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
