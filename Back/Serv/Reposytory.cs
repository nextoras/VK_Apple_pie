using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Vk_server
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        readonly DbContext _context;
        readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Create(TEntity item)
        {
            _dbSet.Add(item);
        }

        public Task CreateAsync(TEntity item)
        {
            return _dbSet.AddAsync(item);
        }

        public TEntity CreateR(TEntity item)
        {
            var res = _dbSet.Add(item);
            return res.Entity;
        }

        public void CreateRange(IList<TEntity> items)
        {
            _dbSet.AddRange(items);
        }

        public Task CreateRangeAsync(IList<TEntity> items)
        {
            return _dbSet.AddRangeAsync(items);
        }

        public async Task<TEntity> CreateRAsync(TEntity item)
        {
            var res = await _dbSet.AddAsync(item);
            return res.Entity;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = _dbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            TEntity res = dbQuery
                .AsNoTracking()
                .FirstOrDefault(predicate);

            return res;
        }


        public IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = _dbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            List<TEntity> list = dbQuery
                .AsNoTracking()
                .ToList();

            return list;
        }

        public async Task<IList<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = _dbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            List<TEntity> list = await dbQuery
                .AsNoTracking()
                .ToListAsync();

            return list;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = _dbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            TEntity res = await dbQuery
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate);

            return res;
        }

        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }
        public Task<TEntity> GetByIdAsync(object id)
        {
            return _dbSet.FindAsync(id);
        }

        public IList<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = _dbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            List<TEntity> list = dbQuery
                .AsNoTracking()
                .Where(predicate)
                .ToList();

            return list;
        }

        public async Task<IList<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> dbQuery = _dbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            List<TEntity> list = await dbQuery
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();


            return list;
        }

        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
        }
        public void RemoveById(object id)
        {
            var c = _dbSet.Find(id);
            if (c == null)
            {
                return;
            }
            _dbSet.Remove(c);
        }

        public void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            // TODO: test remove range
            IEnumerable<TEntity> objects = _dbSet.Where<TEntity>(predicate).AsEnumerable();
            //foreach (TEntity obj in objects)
            //    _dbSet.Remove(obj);
            _dbSet.RemoveRange(objects);
        }

        public void Update(TEntity item)
        {
            _dbSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Update(object id, TEntity newEntity)
        {
            var currentEntity = _dbSet.Find(id);
            // _context.Set<TEntity>().Update(item);
            // _dbSet.Attach(item);
            _context.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet;
        }

        public DbContext Context()
        {
            return _context;
        }

        public TEntity GetLastId()
        {
            return _dbSet.LastOrDefault();
        }
    }
}
