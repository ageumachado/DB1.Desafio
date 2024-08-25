using DB1.Core.Data;
using DB1.Core.DomainObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DB1.Desafio.Infra.Data
{
    internal abstract class Repository<TEntity> :
        IDisposable, IRepository<TEntity> where TEntity : Entity, IEntity, IAggregateRoot
    {
        private readonly DbSet<TEntity> dbSet;

        protected readonly Db1DataContext Context;

        public IUnitOfWork UnitOfWork => Context;
                
        protected Repository(Db1DataContext context)
        {
            Context = context;
            dbSet = Context.Set<TEntity>();
        }

        /// <inheritdoc/>
        protected IQueryable<TEntity> ObterQuery() =>
            dbSet.AsQueryable();

        /// <inheritdoc/>
        protected IQueryable<TEntitySet> ObterQuery<TEntitySet>() where TEntitySet : class
            => Context.Set<TEntitySet>().AsQueryable();

        /// <inheritdoc/>
        public virtual void Adicionar(TEntity obj)
        {
            dbSet.Add(obj);
        }

        /// <inheritdoc/>
        protected void Adicionar<TEntitySet>(TEntitySet entity) where TEntitySet : class
        {
            Context.Set<TEntitySet>().Add(entity);
        }

        /// <inheritdoc/>
        protected void AdicionarRange<TEntitySet>(params TEntitySet[] entity) where TEntitySet : class
        {
            Context.Set<TEntitySet>().AddRange(entity);
        }

        /// <inheritdoc/>
        public void Adicionar(IEnumerable<TEntity> objs)
        {
            dbSet.AddRange(objs);
        }

        /// <inheritdoc/>
        public async void Remover(Guid id)
        {
            var entity = await dbSet.FindAsync(id);
            if(entity is not null)
                dbSet.Remove(entity);
        }

        /// <inheritdoc/>
        public void Remover(TEntity obj)
        {
            dbSet.Remove(obj);
        }

        /// <inheritdoc/>
        protected void Remover<TEntitySet>(TEntitySet entity) where TEntitySet : class
        {
            Context.Set<TEntitySet>().Remove(entity);
        }

        /// <inheritdoc/>
        protected void RemoverRange<TEntitySet>(params TEntitySet[] entity) where TEntitySet : class
        {
            Context.Set<TEntitySet>().RemoveRange(entity);
        }

        /// <inheritdoc/>
        public void Remover(IEnumerable<TEntity> objs)
        {
            dbSet.RemoveRange(objs);
        }

        /// <inheritdoc/>
        public virtual void Editar(TEntity obj)
        {
            dbSet.Update(obj);
        }

        protected void Editar<TEntitySet>(TEntitySet entity) where TEntitySet : class
        {
            Context.Set<TEntitySet>().Update(entity);
        }

        /// <inheritdoc/>
        public virtual async Task<bool> ExisteAsync(Expression<Func<TEntity, bool>> where)
        {
            return await dbSet.AnyAsync(where);
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> ObterListaAsync()
        {
            return await dbSet.AsNoTrackingWithIdentityResolution().ToListAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> ObterListaAsync(Expression<Func<TEntity, bool>> where)
        {
            return await ObterQuery()
                .Where(where)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<TDestination?> ObterUnicoQueryableAsync<TDestination>(Func<IQueryable<TEntity>, IQueryable<TDestination>> predicate)
        {
            var query = ObterQuery()
                .AsNoTrackingWithIdentityResolution();
            return await predicate.Invoke(query).FirstOrDefaultAsync() ?? default;
        }

        /// <inheritdoc/>
        public async Task<TResult?> ObterUnicoComFiltroEhSelecaoAsync<TResult>
            (Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TResult>> select) where TResult : class
        {
            return await ObterQuery()
                .Where(expression)
                .Select(select)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TDestination>> ObterListaQueryableAsync<TDestination>(Func<IQueryable<TEntity>, IQueryable<TDestination>> predicate)
        {
            var query = ObterQuery().AsNoTrackingWithIdentityResolution();
            return await predicate.Invoke(query).ToListAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> ObterListaEhOrdenarAsync<TKey>(Expression<Func<TEntity, TKey>> order)
        {
            return await ObterQuery()
                .AsNoTrackingWithIdentityResolution()
                .OrderBy(order)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> ObterListaAsync<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order)
        {
            return await ObterQuery()
                .AsNoTrackingWithIdentityResolution()
                .Where(where)
                .OrderBy(order)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async ValueTask<TEntity?> ObterPorIdAsync(params object[]? keyValues)
        {
            return await dbSet.FindAsync(keyValues);
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity?> ObterUnicoAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await ObterQuery()
                .FirstOrDefaultAsync(expression);
        }

        #region IDisposable Support
        private bool disposedValue;

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Context?.Dispose();
                disposedValue = true;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
