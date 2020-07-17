using Quiz.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Quiz.Data.Service
{
    public interface IRepository<T> where T : Superior
    {
        List<T> _Get();
        List<A> _Get<A>() where A : Superior;
        List<T> _GetWhere(Expression<Func<T, bool>> predicate);
        List<A> _GetWhere<A>(Expression<Func<A, bool>> predicate) where A : Superior;
        T _GetSingle(Func<T, bool> predicate);
        A _GetSingle<A>(Func<A, bool> predicate) where A : Superior;
        T _GetById(long id);
        A _GetById<A>(long id) where A : Superior;
        bool _Add(T entity);
        bool _Add<A>(A entity) where A : Superior;
        bool Remove(T entity);
        bool _Remove<A>(A entity) where A : Superior;
        bool _Remove(long id);
        bool _Remove<A>(long id) where A : Superior;
        bool _Update(T entity);
        bool _Update<A>(A entity) where A : Superior;
        int _Save();
        void _BeginTran();
        void _Commit();
        void _Rollback();
    }
}
