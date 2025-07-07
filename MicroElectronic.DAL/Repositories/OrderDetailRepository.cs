using MicroElectronic.DAL.Interfaces;
using MicroElectronic.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroElectronic.DAL.Repositories
{
    public class OrderDetailRepository :IBaseRepository<OrderDetail>
    {
        private readonly ApplicationDbContext _db;

        public OrderDetailRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(OrderDetail entity)
        {
            await _db.OrderDetails.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(OrderDetail entity)
        {
            _db.OrderDetails.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<OrderDetail> GetAll()
        {
            return _db.OrderDetails;
        }

        public async Task<OrderDetail> Update(OrderDetail entity)
        {
            _db.OrderDetails.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
