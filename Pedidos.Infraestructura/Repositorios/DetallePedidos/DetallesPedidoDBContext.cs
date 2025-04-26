using Microsoft.EntityFrameworkCore;
using Pedidos.Dominio.Entidades;

namespace Pedidos.Infraestructura.Repositorios.DetallePedidos
{
    public class DetallesPedidoDBContext: DbContext
    {
        public DetallesPedidoDBContext(DbContextOptions<DetallesPedidoDBContext> options) : base(options)
        {
        }
        public DbSet<DetallePedido> DetallePedido { get; set; }
    }
    
}
