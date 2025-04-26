using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Dominio.Entidades
{
    [Table("tbl_pedido")]
    public class Pedido: EntidadBase
    {
        [Column("idcliente")]
        public Guid IdCliente { get; set; }
        
        [Column("fecharealizado", TypeName = "timestamp(6)")]
        public DateTime FechaRealizado { get; set; }

        [Column("fechaentrega", TypeName = "timestamp(6)")]
        public DateTime FechaEntrega { get; set; }

        [Column("estadopedido")]
        public string EstadoPedido { get; set; } = string.Empty;

        [Column("valortotal", TypeName = "numeric(10,3)")]
        public decimal ValorTotal { get; set; }

        [Column("idvendedor")]
        public Guid? IdVendedor { get; set; }

        [Column("comentarios")]
        public string Comentarios { get; set; } = string.Empty;

        [Column("idmoneda")]
        public int IdMoneda { get; set; }
    }
}