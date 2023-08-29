using WEBTEST_API_PROYECT.Models;

namespace WEBTEST_API_PROYECT.Repository.IRepository
{
    public interface ITestWebRepository : IRepository<TestWeb>
    {

        Task<TestWeb> Update(TestWeb entity);
    }
}
