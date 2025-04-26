using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Aplicacion.Dto
{
    public class DetallePedidoIn
    {
        public Guid? IdPedido { get; set; }
        public Guid IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
