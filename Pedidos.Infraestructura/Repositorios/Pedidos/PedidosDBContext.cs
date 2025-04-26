using Microsoft.EntityFrameworkCore;
using Pedidos.Dominio.Entidades;

namespace Pedidos.Infraestructura.Repositorios.Pedidos
{
    public class PedidosDBContext: DbContext
    {
        public PedidosDBContext(DbContextOptions<PedidosDBContext> options) : base(options)
        {
        }
        public DbSet<Pedido> Pedidos { get; set; }
    }
}
