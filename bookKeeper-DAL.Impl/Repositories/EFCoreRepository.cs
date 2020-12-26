using System;
using System.Collections.Generic;
using bookKeeper_DAL.Abstract.Repository;
using bookKeeper_DAL.Impl.Contexts;
using bookKeeper_Entity;

namespace bookKeeper_DAL.Impl.Repositories
{
    public abstract class EFCoreRepository<TKey, TEntity> : IRepository<TKey, TEntity> 
        where TKey : struct where TEntity : class, IWithId<TKey>
    {
        protected readonly DataBaseContext Db;

        protected EFCoreRepository(DataBaseContext context)
        {
            Db = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>();
        }

        public TEntity GetSingle(TKey id)
        {
            return Db.Find<TEntity>(id);
        }

        public void Create(TEntity item)
        {
            Db.Add(item);
            Db.SaveChanges();
        }

        public void Update(TEntity item)
        {
            Db.Update(item);
            Db.SaveChanges();
        }

        public void Delete(TKey id)
        {
            var result = Db.Find<TEntity>(id);
            Db.Remove(result);
        }

        public void Save()
        {
            Db.SaveChanges();
        }


        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Db.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}