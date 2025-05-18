using Pedidos.Aplicacion.Clientes;
using System.Diagnostics.CodeAnalysis;

namespace ServicioPedido.Middleware
{
    /// <summary>
    /// Middleware para validar el token de autorización
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AutorizadorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUsuarioApiClient _autorizador;

        /// <summary>
        /// Constructor del middleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="autorizador"></param>
        public AutorizadorMiddleware(RequestDelegate next, IUsuarioApiClient autorizador)
        {
            _next = next;
            _autorizador = autorizador;
        }
        /// <summary>
        /// Procesa el request y valida el token
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token)) 
            {
                context.Items["UserId"] = await Validar(token);
            }

            await _next(context);
        }

        private async Task<string> Validar(string token)
        {
            string usuarioToken = null;
            try
            {
                var resultado = await _autorizador.ValidarToken(token,1);

                if (resultado.Status == (int)System.Net.HttpStatusCode.OK) 
                { 
                    usuarioToken = resultado.IdUsuario;   
                }
            }
            catch 
            {
                usuarioToken = null;
            }

            return usuarioToken;
        }
    }
}
