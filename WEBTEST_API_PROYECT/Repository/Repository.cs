using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using WEBTEST_API_PROYECT.Data;
using WEBTEST_API_PROYECT.Repository.IRepository;

namespace WEBTEST_API_PROYECT.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;


        public Repository(ApplicationDbContext db) 
        {
            _db = db;   

            this.dbSet = _db.Set<T>();
        }


        public async Task Create(T entity)
        {
            await dbSet.AddAsync(entity);
            await Save();
        }


        public async Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if(!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }


        public async Task Delete(T entity)
        {
             dbSet.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
           await _db.SaveChangesAsync();
        }
    }
}
