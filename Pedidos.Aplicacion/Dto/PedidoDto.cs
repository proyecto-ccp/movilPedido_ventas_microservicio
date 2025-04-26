namespace Pedidos.Aplicacion.Dto
{
    public class PedidoDto
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public DateTime? FechaRealizado { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string EstadoPedido { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public Guid? IdVendedor { get; set; }
        public string Comentarios { get; set; } = string.Empty;
        public int IdMoneda { get; set; }
    }

    public class PedidoOut : BaseOut
    {
        public PedidoDto Pedido { get; set; }
    }

    public class PedidoOutList : BaseOut
    {
        public List<PedidoDto> Pedidos { get; set; }
    }
}
