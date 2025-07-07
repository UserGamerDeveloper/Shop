using MicroElectronic.DAL.Interfaces;
using MicroElectronic.Domain.Models;

namespace MicroElectronic.DAL.Repositories
{
    public class CategoryRepository : IBaseRepository<Category>
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Category entity)
        {
            await _db.Categories.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Category entity)
        {
            _db.Categories.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Category> GetAll()
        {
            return _db.Categories;
        }

        public async Task<Category> Update(Category entity)
        {
            _db.Categories.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
