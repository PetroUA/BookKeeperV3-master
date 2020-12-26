using bookKeeper_DAL.Abstract.Repository;
using bookKeeper_DAL.Impl.Repositories;
using bookKeeper_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookKeeper.Tests
{
    public abstract class MockRepository<TKey, TEntity> : IRepository<TKey, TEntity>
         where TKey : struct where TEntity : class, IWithId<TKey>
    {
        public Dictionary<TKey, TEntity> dic;

        public MockRepository()
        {
            dic = new Dictionary<TKey, TEntity>();
        }

        public void Create(TEntity item)
        {
            dic.Add(item.Id, item);
        }

        public void Delete(TKey id)
        {
            dic.Remove(id);
        }

        public void Dispose()
        {
            dic.Clear();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dic.Values;
        }

        public TEntity GetSingle(TKey id)
        {
            return dic[id];
        }

        public void Save()
        {
            //do nothing
        }

        public void Update(TEntity item)
        {
            dic[item.Id] = item;
        }
    }
}