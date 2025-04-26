using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Dominio.Entidades
{
    [Table("tbl_detalle_pedido")]
    public class DetallePedido : EntidadBase

    {
        [Column("idpedido")]
        public Guid? IdPedido { get; set; }

        [Column("idusuario")]
        public Guid IdUsuario { get; set; }

        [Column("idproducto")]
        public int IdProducto { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("preciounitario", TypeName = "numeric(10,3)")]
        public decimal PrecioUnitario { get; set; }

        [Column("valortotal", TypeName = "numeric(10,3)")]
        public decimal ValorTotal
        {
            get; set;
        }
    }
}