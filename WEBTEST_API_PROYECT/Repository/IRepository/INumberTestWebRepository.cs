using WEBTEST_API_PROYECT.Models;

namespace WEBTEST_API_PROYECT.Repository.IRepository
{
    public interface INumberTestWebRepository : IRepository<NumberTestWeb>
    {

        Task<NumberTestWeb> Update(NumberTestWeb entity);
    }
}
