using customerOrders.Domain.Entities;
using customerOrders.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace customerOrders.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private readonly CustomerOrdersDbContext _context;

        public GenericRepository(CustomerOrdersDbContext context)
        {
            _context = context;
        }
        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Add(TEntity Entity)
        {
            _context.Set<TEntity>().Add(Entity);
        }

        public void Update(TEntity Entity)
        {
            _context.Set<TEntity>().Update(Entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
            }
        }
    }
}
