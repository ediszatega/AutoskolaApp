using AutoMapper;
using Autoskola.Repository.Data;
using Autoskola.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Repositories
{
    public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly ApplicationContext dbContext;
        protected readonly DbSet<TEntity> dbSet;
        public BaseRepository(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            dbSet.Add(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            dbSet.Update(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            dbSet.Remove(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<TEntity> GetAll(int page, int pageSize)
        {
            return dbSet.ToPagedList(page, pageSize);
        }

        public TEntity GetById(TKey key)
        {
            return dbSet.Find(key);
        }
    }
}
