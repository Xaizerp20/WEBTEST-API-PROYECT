using System.Linq.Expressions;

namespace WEBTEST_API_PROYECT.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task Create(T entity);
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);

        Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true);

        //Task<T> Update(T entity);

        Task Delete(T entity);

        Task Save();





    }
}
