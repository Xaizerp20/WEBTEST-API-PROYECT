using WEBTEST_API_PROYECT.Data;
using WEBTEST_API_PROYECT.Models;
using WEBTEST_API_PROYECT.Repository.IRepository;

namespace WEBTEST_API_PROYECT.Repository
{
    public class TestWebRepository : Repository<TestWeb>, ITestWebRepository
    {
        private readonly ApplicationDbContext _db;

        public TestWebRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<TestWeb> Update(TestWeb entity)
        {
            entity.DateCreation = DateTime.Now;
            _db.Update(entity);

            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
