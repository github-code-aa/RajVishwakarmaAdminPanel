using Parichay.Bussiness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace Parichay.Data.Common
{
    public interface IBaseData<T>  where T : BaseEntity
    {
        int Create(T entity);
        int Delete(T entity);
        int Create(IList<T> entities);
        int Delete(IList<T> entities);
        IEnumerable<T> GetAll();
        IList<T> GetAll(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllQ(Expression<Func<T, bool>> predicate);
        int Count(Expression<Func<T, bool>> predicate);
        T GetById(object id);
        int Update(T entity, bool IsModified = false);
        int Update(IList<T> entities);
        List<T> GetAllbySP(string Query, params object[] parameters);
        void ReloadT(T entity);
    }
}
