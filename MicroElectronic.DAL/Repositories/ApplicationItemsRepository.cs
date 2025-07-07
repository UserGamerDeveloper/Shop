using MicroElectronic.DAL.Interfaces;
using MicroElectronic.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.DAL.Repositories
{
    public class ApplicationItemsRepository : IBaseRepository<ApplicationItem>
    {
        private readonly ApplicationDbContext _db;

        public ApplicationItemsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(ApplicationItem entity)
        {
            await _db.ApplicationItems.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(ApplicationItem entity)
        {
            _db.ApplicationItems.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<ApplicationItem> GetAll()
        {
            return _db.ApplicationItems;
        }

        public async Task<ApplicationItem> Update(ApplicationItem entity)
        {
            _db.ApplicationItems.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
