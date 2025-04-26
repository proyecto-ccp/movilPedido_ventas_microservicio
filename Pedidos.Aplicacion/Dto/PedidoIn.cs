using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Aplicacion.Dto
{
    public class PedidoIn
    {
        public Guid IdCliente { get; set; }
        public DateTime FechaRealizado { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string EstadoPedido { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public Guid? IdVendedor { get; set; }
        public string Comentarios { get; set; } = string.Empty;
        public int IdMoneda { get; set; }
    }
}
