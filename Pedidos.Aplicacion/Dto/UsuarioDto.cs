namespace Pedidos.Aplicacion.Dto
{
    public class UsuarioDto
    {
        public string? UserName { get; set; }
        public string? Contrasena { get; set; }
        public int? Aplicacion { get; set; }
    }

    public class Usuario
    {
        public string? username { get; set; }
        public string? contrasena { get; set; }
        public int? aplicacion { get; set; }
    }

    public class UsuarioResponseDto
    {
        public int Resultado { get; set; }
        public string Mensaje { get; set; }
        public int Status { get; set; }
        public string IdUsuario { get; set; }
        public string? Token { get; set; }
    }
}
