using GameDAL.Repository.Abstraction;
using GameStoreUI.Models.VIewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameDAL.Repository.Implementation
{
    public class EFRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext context;
        private readonly DbSet<TEntity> set;

        public EFRepository(DbContext _context)
        {
            context = _context;
            set = context.Set<TEntity>();
        }
        public void Create(TEntity entity)
        {
            set.Add(entity);
            //    context.Entry(entity).State = EntityState.Added;
            Save();
        }

        public void Delete(TEntity entity)
        {
            set.Remove(entity);
            Save();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return set.AsEnumerable();
        }

        public void Update(TEntity entity)
        {
            set.AddOrUpdate(entity);
            // context.Entry(entity).State = EntityState.Added;
            Save();
        }
        public void Save()
        {
            context.SaveChanges();
        }

        public TEntity Find(int id)
        {
            return set.Find(id);
        }
      
        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> gameFilters)
        {
            return set.Where(gameFilters).AsEnumerable();
        }

        public TEntity Find(string id)
        {
            return set.Find(id);
        }
    }
}
