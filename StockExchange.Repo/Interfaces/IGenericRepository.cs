using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StockExchange.Repo
{
  public interface IGenericRepository<T> where T : class
  {
    IEnumerable<T> All();
    IEnumerable<T> AllInclude
          (params Expression<Func<T, object>>[] includeProperties);
    IEnumerable<T> FindByInclude
      (Expression<Func<T, bool>> predicate,
          params Expression<Func<T, object>>[] includeProperties);
    IQueryable<T> GetAllIncluding
          (params Expression<Func<T, object>>[] includeProperties);
    IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
    T FindByKey(int id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(int id);
  }
}