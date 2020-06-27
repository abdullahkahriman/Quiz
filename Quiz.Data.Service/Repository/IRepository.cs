using Quiz.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Quiz.Data.Service
{
    public interface IRepository<T> where T : Superior
    {
        List<T> Get();
        List<A> Get<A>() where A : Superior;
        List<T> GetWhere(Expression<Func<T, bool>> predicate);
        List<A> GetWhere<A>(Expression<Func<A, bool>> predicate) where A : Superior;
        T GetSingle(Func<T, bool> predicate);
        A GetSingle<A>(Func<A, bool> predicate) where A : Superior;
        T GetById(long id);
        A GetById<A>(long id) where A : Superior;
        bool Add(T entity);
        bool Add<A>(A entity) where A : Superior;
        bool Remove(T entity);
        bool Remove<A>(A entity) where A : Superior;
        bool Remove(long id);
        bool Remove<A>(long id) where A : Superior;
        bool Update(T entity, long id);
        bool Update<A>(A entity, long id) where A : Superior;
        int Save();
        void BeginTran();
        void Commit();
        void Rollback();
    }
}
