﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pedidos.Dominio.Entidades;
using Pedidos.Infraestructura.Repositorios.Pedidos;

namespace Pedidos.Infraestructura.RepositoriosGenericos.Pedidos
{
    public class RepositorioBasePedido<T> : IRepositorioBasePedido<T> where T : EntidadBase
    {
        private readonly IServiceProvider _serviceProvider;
        public RepositorioBasePedido(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private PedidosDBContext GetContext()
        {
            return _serviceProvider.GetService<PedidosDBContext>();
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

        public async Task<T> Actualizar(T entity)
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();
            var res = entitySet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> BuscarPorLlave(object ValueKey)
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();
            var res = await entitySet.FindAsync(ValueKey);
            await _context.DisposeAsync();
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

        public async Task<List<T>> BuscarPorAtributo(Guid ValueAttribute, string Attribute)
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();
            var res = await entitySet.Where(v => EF.Property<Guid>(v, Attribute) == ValueAttribute).ToListAsync();
            await _context.DisposeAsync();
            return res;
        }
        public async Task<List<T>> DarListado()
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();
            var res = await entitySet.ToListAsync();
            await _context.DisposeAsync();
            return res;
        }

        public async Task<List<T>> BuscarPendientePorEntregar(string strEstado)
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();
            var res = await entitySet.Where(v => EF.Property<string>(v, "EstadoPedido") == strEstado).ToListAsync();
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
