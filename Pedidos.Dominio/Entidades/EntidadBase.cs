using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pedidos.Dominio.Entidades
{
    public class EntidadBase
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        //[Column("fecharegistro", TypeName = "timestamp(6)")]
        //public DateTime FechaCreacion { get; set; }
    }
}
