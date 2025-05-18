namespace Pedidos.Aplicacion.Dto
{
    public class UsuarioDto
    {
        public int Aplicacion { get; set; }
        public string Token { get; set; }
        public string IdUsuario { get; set; }
    }

    public class Usuario
    {
        public string Token { get; set; }
        public string IdUsuario { get; set; }
    }

    public class UsuarioResponseDto
    {
        public int Resultado { get; set; }
        public string Mensaje { get; set; }
        public int Status { get; set; }
        public string IdUsuario { get; set; }
    }
}
