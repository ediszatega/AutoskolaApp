using Autoskola.Repository.Interfaces;
using Autoskola.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Services
{
    public class BaseService<TEntity, TKey> : IBaseService<TEntity, TKey> where TEntity : class
    {
        protected readonly IRepository<TEntity, TKey> repository;

        public BaseService(IRepository<TEntity, TKey> repository)
        {
            this.repository = repository;
        }

        public TEntity Add(TEntity entity)
        {
            repository.Add(entity);
            return entity;
        }

        public IEnumerable<TEntity> GetAll(int page, int pageSize)
        {
            return repository.GetAll(page, pageSize);
        }

        public TEntity GetById(TKey key)
        {
            var entity = repository.GetById(key);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            repository.Update(entity);
            return entity;
        }

        public TEntity Remove(TKey key)
        {
            var entity = repository.GetById(key);
            repository.Remove(entity);
            return entity;
        }
    }
}
