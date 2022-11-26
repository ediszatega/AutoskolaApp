using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Interfaces
{
    public interface IRepository<TEntity, TKey>
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);
        TEntity GetById(TKey key);
        IEnumerable<TEntity> GetAll(int page, int pageSize);
    }
}
