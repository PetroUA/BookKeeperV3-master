using bookKeeper_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookKeeper_DAL.Abstract.Repository
{
    public interface IRepository<TKey, TEntity> : IDisposable
        where TKey : struct where TEntity : class, IWithId<TKey>
    {
        IEnumerable<TEntity> GetAll(); 
        TEntity GetSingle(TKey id);
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(TKey id);
        void Save();
    }
}