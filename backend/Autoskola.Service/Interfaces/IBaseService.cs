using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Service.Interfaces
{
    public interface IBaseService<TEntity, TKey>
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TKey key);
        TEntity GetById(TKey key);
        IEnumerable<TEntity> GetAll(int page, int pageSize);
    }
}
