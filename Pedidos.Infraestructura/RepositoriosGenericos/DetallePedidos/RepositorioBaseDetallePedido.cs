using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pedidos.Dominio.Entidades;
using Pedidos.Infraestructura.Repositorios.DetallePedidos;

namespace Pedidos.Infraestructura.RepositoriosGenericos.DetallePedidos
{
    public class RepositorioBaseDetallePedido<T> : IRepositorioBaseDetallePedido<T> where T : EntidadBase
    {
        private readonly IServiceProvider _serviceProvider;
        public RepositorioBaseDetallePedido(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private DetallesPedidoDBContext GetContext()
        {
            return _serviceProvider.GetService<DetallesPedidoDBContext>();
        }

        protected DbSet<T> GetEntitySet()
        {
            return GetContext().Set<T>();
        }

        public async Task<T> Crear(T entity)
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();
            var res = await entitySet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task ActualizarIdPedido(Guid idUsuario, Guid idPedido)
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();

            var registros = await entitySet
            .Where(v => EF.Property<Guid>(v, "IdUsuario") == idUsuario 
                        &&
                        EF.Property<Guid>(v, "IdPedido").ToString().Length == 0)
            .ToListAsync();

            foreach (var registro in registros)
            {
                var property = registro.GetType().GetProperty("IdPedido");
                if (property != null && property.CanWrite)
                {
                    property.SetValue(registro, idPedido);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<T> BuscarPorLlave(object ValueKey)
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();
            var res = await entitySet.FindAsync(ValueKey);
            await _context.DisposeAsync();
            return res;

        }

        public async Task<T> Eliminar(object ValueKey)
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();
            var res = await entitySet.FindAsync(ValueKey);
            entitySet.Remove(res);
            await _context.SaveChangesAsync();

            return res;
        }
        public async Task<List<T>> BuscarPorAtributo(string ValueAttribute, string Attribute)
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();
            var res = await entitySet.Where(v => EF.Property<string>(v, Attribute) == ValueAttribute).ToListAsync();
            await _context.DisposeAsync();
            return res;
        }

        public async Task<List<T>> BuscarPorAtributo(Guid ValueAttribute, string Attribute, int usuario=0)
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();

            if (usuario != 0)
            {
                var res = await entitySet.Where(v => EF.Property<Guid>(v, Attribute) == ValueAttribute
                                                    &&
                                                    EF.Property<Guid>(v, "IdPedido").ToString().Length == 0).ToListAsync();
                await _context.DisposeAsync();
                return res;
            }
            else
            {
                var res = await entitySet.Where(v => EF.Property<Guid>(v, Attribute) == ValueAttribute).ToListAsync();
                await _context.DisposeAsync();
                return res;
            }
            
        }

        public async Task<List<T>> DarListado()
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();
            var res = await entitySet.ToListAsync();
            await _context.DisposeAsync();
            return res;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                try
                {
                    var ctx = GetContext();
                    ctx.Dispose();
                }
                catch (Exception ex)
                { }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
