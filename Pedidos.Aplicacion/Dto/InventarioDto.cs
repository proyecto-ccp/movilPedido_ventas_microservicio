
namespace Pedidos.Aplicacion.Dto
{
    public class InventarioDto
    {
        public Guid Id { get; set; }
        public int IdProducto { get; set; }
        public int CantidadStock { get; set; }

    }

    public class Inventario
    {
        public int IdProducto { get; set; }
        public int CantidadStock { get; set; }

    }

    public class InventarioResponseDto
    {
        public InventarioDto Inventario { get; set; }
        public int Resultado { get; set; }
        public string Mensaje { get; set; }
        public int Status { get; set; }
    }
}
