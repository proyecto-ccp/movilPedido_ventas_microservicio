namespace Pedidos.Aplicacion.Dto
{
    public class AuditoriaDto
    {
        public Guid IdUsuario { get; set; }
        public string Accion { get; set; }
        public string TablaAfectada { get; set; }
        public string Idregistro { get; set; }
        public string Registro { get; set; }
    }

    public class Auditoria
    {
        public Guid IdUsuario { get; set; }
        public string Accion { get; set; }
        public string TablaAfectada { get; set; }
        public string Idregistro { get; set; }
        public string Registro { get; set; }
    }

    public class AuditoriaResponseDto
    {
        public int Resultado { get; set; }
        public string Mensaje { get; set; }
        public int Status { get; set; }
        public Guid Id { get; set; }
    }
}
