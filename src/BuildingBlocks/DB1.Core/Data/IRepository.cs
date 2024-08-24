using DB1.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DB1.Core.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : IEntity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        void Adicionar(TEntity obj);
        void Adicionar(IEnumerable<TEntity> objs);
        void Editar(TEntity obj);
        void Remover(Guid id);
        void Remover(TEntity obj);
        void Remover(IEnumerable<TEntity> objs);

        /// <summary>
        /// Obter objetos convertido com base no predicado
        /// </summary>
        /// <typeparam name="TDestination">Classe de destino</typeparam>
        /// <param name="predicate">Regra para conversão</param>
        /// <returns></returns>
        Task<TDestination?> ObterUnicoQueryableAsync<TDestination>
            (Func<IQueryable<TEntity>, IQueryable<TDestination>> predicate);

        /// <summary>
        /// Obter lista de objetos convertido com base no predicado
        /// </summary>
        /// <typeparam name="TSource">Classe de destino</typeparam>
        /// <param name="predicate">Regra para conversão</param>
        /// <returns></returns>
        Task<TResult?> ObterUnicoComFiltroEhSelecaoAsync<TResult>
            (Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TResult>> select) where TResult : class;

        /// <summary>
        /// Obter lista de objetos convertido com base no predicado
        /// </summary>
        /// <typeparam name="TDestination">Classe de destino</typeparam>
        /// <param name="predicate">Regra para conversão</param>
        /// <returns></returns>
        Task<IEnumerable<TDestination>> ObterListaQueryableAsync<TDestination>(Func<IQueryable<TEntity>, IQueryable<TDestination>> predicate);

        Task<IEnumerable<TEntity>> ObterListaAsync();
        Task<IEnumerable<TEntity>> ObterListaAsync(Expression<Func<TEntity, bool>> where);
        Task<IEnumerable<TEntity>> ObterListaEhOrdenarAsync<TKey>(Expression<Func<TEntity, TKey>> order);
        Task<IEnumerable<TEntity>> ObterListaAsync<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order);

        ValueTask<TEntity?> ObterPorIdAsync(params object[] keyValues);
        Task<TEntity?> ObterUnicoAsync(Expression<Func<TEntity, bool>> expression);
        //Task<TDestination?> ObterUnicoAsync<TDestination>(Func<IQueryable<TEntity>, IQueryable<TDestination>> predicate);

        Task<bool> ExisteAsync(Expression<Func<TEntity, bool>> where);
    }
}
