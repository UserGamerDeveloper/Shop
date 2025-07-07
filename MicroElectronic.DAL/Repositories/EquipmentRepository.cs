using MicroElectronic.DAL.Interfaces;
using MicroElectronic.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.DAL.Repositories
{
    public class EquipmentRepository :IBaseRepository<Equipment>
    {
        private readonly ApplicationDbContext _db;
        public EquipmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Equipment entity)
        {
            await _db.Equipments.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Equipment entity)
        {
            _db.Equipments.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Equipment> GetAll()
        {
            return _db.Equipments;
        }

        public async Task<Equipment> Update(Equipment entity)
        {
            _db.Equipments.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
