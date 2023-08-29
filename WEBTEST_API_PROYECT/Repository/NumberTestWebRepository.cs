using WEBTEST_API_PROYECT.Data;
using WEBTEST_API_PROYECT.Models;
using WEBTEST_API_PROYECT.Repository.IRepository;

namespace WEBTEST_API_PROYECT.Repository
{
    public class NumberTestWebRepository : Repository<NumberTestWeb>, INumberTestWebRepository
    {
        private readonly ApplicationDbContext _db;

        public NumberTestWebRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<NumberTestWeb> Update(NumberTestWeb entity)
        {
            entity.UpdatingTime = DateTime.Now;
            _db.Update(entity);

            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
