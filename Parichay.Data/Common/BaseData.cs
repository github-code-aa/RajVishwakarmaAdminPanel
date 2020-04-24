using Parichay.Bussiness;
using Parichay.Bussiness.Model;
using Parichay.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace Parichay.Data
{
    [Serializable]
    public abstract class BaseData<TEntity> : IBaseData<TEntity> where TEntity : BaseEntity
    {
        protected IContext _context;
        protected IDbSet<TEntity> _dbset;
        protected ISPContext _spContext;
        internal ParichayContext contextdb = new ParichayContext();
        public BaseData(IContext context, ISPContext spContext)
        {
            _context = context;
            _dbset = _context.Set<TEntity>();
            _spContext = spContext;
        }
        public virtual int Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entities");
            }
            _dbset.Add(entity);
            return _context.SaveChanges();
        }
        public virtual int Create(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            foreach (var entity in entities)
            {
                _dbset.Add(entity);
            }
            return _context.SaveChanges();
        }
        public virtual int Update(TEntity entity, bool isModified = false)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            if (isModified)
            {
                _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
            return _context.SaveChanges();
        }
        public virtual int Update(IList<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            foreach (var entity in entities)
            {
                _context.Entry(entities).State = System.Data.Entity.EntityState.Modified;
            }
            return _context.SaveChanges();
        }
        public virtual int Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _dbset.Remove(entity);
            return _context.SaveChanges();
        }
        public virtual int Delete(IList<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException("entities");
            foreach (var entity in entities)
            {
                _dbset.Remove(entity);
            }
            return _context.SaveChanges();
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                var result = _dbset.AsEnumerable<TEntity>();
                scope.Complete();
                return result;
            }
        }
        public virtual IList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                var result = _dbset.Where(predicate).ToList();
                scope.Complete();
                return result;
            }
        }
        public virtual IQueryable<TEntity> GetAllQ(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return _dbset.Count();
            return _dbset.Count(predicate);
        }
        public virtual List<TEntity> GetAllbySP(string query, params object[] parameters)
        {
            return _spContext.getAllSP<TEntity>(query, parameters).ToList();
        }
        public virtual TEntity GetById(object id)
        {
            return this._dbset.Find(id);
        }
        public virtual void ReloadT(TEntity entity)
        {
            _context.Entry<TEntity>(entity).Reload();
        }
    }
}
