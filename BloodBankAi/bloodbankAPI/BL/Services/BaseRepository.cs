using BL.Const;
using BL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
namespace BL.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            var items= _context.Set<T>().ToList();
            return items;
        }
        public IQueryable<T> values()
        {
            var items = _context.Set<T>();
            return items;
        }
        //public IQueryable<T> values(int id)
        //{
        //    var items = _context.Set<T>().Find(id);
        //    return items;
        //}
        public async Task< IEnumerable<T>> GetAllWithChildren()
        {
            IQueryable<T> query = _context.Set<T>();
            var entityType = typeof(T);
            var properties = entityType.GetProperties()
                .Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string));
            foreach (var property in properties)
            {
                query = query.Include(property.Name);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
             var items = await _context.Set<T>().ToListAsync();
            return items;
        }


        public T GetById(int id)
        {
            var item= _context.Set<T>().Find(id);
            return item;
        }
        public T GetById(string id)
        {
            var item= _context.Set<T>().Find(id);
            return item;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            return item;
        }
        public async Task<T> GetByIdAsync(string id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            return item;
        }
        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);
            var item = query.SingleOrDefault(criteria);
            return item;
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);
            var item= await query.SingleOrDefaultAsync(criteria);
            return item;
        }
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            var item = query.Where(criteria).ToList();
            return item;
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            var items = _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToList();
            return items;
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            var items = query.ToList();
            return items;
        }
        public async Task<IEnumerable<T>> FindAllAsync(string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            var items = await query.Where(criteria).ToListAsync();
            return items;
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
        {
            var items = await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
            return items;
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            var items = await query.ToListAsync();
            return items;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AttachRange(entities);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Count(criteria);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }
    }
}
