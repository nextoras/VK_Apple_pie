using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;


namespace Vk_server
{
    public interface IRepository<TEntity> where TEntity : class
    {        
        void Create(TEntity item);
        Task CreateAsync(TEntity item);
        TEntity CreateR(TEntity item);
        Task<TEntity> CreateRAsync(TEntity item);
        void CreateRange(IList<TEntity> items);
        Task CreateRangeAsync(IList<TEntity> items);
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);

        TEntity GetLastId();

        TEntity Get(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        IList<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        Task<IList<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] navigationProperties);

        IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<IList<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] navigationProperties);
        void RemoveById(object id);
        void Remove(TEntity item);
        void Remove(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity item);
        void Update(object id, TEntity newEntity);
        IQueryable<TEntity> Query();
        DbContext Context();
    }
}