namespace Pedidos.Aplicacion.Dto
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string urlFoto1 { get; set; }
        public string urlFoto2 { get; set; }
    }

    public class Producto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class ProductoResponseDto
    {
        public ProductoDto Producto { get; set; }
        public int Resultado { get; set; }
        public string Mensaje { get; set; }
        public int Status { get; set; }
    }
}
