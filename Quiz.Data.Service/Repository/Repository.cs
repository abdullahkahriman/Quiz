using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Core;
using Quiz.Data.Context.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Quiz.Data.Service
{
    public class Repository<Type> : IRepository<Type> where Type : Superior
    {
        protected QuizDBContext _context;
        public Repository(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetService<QuizDBContext>();
        }

        [NonAction]
        public DbSet<Type> _Table()
        {
            return _Table<Type>();
        }

        [NonAction]
        public DbSet<A> _Table<A>() where A : class
        {
            return _context.Set<A>();
        }

        [NonAction]
        public bool _Add(Type model)
        {
            return _Add<Type>(model);
        }

        [NonAction]
        public bool _Add<A>(A model) where A : Superior
        {
            model.IsDeleted = false;
            _Table<A>().Add(model);
            _Save();
            return true;
        }

        [NonAction]
        public List<Type> _Get()
        {
            return _Get<Type>();
        }

        [NonAction]
        public List<A> _Get<A>() where A : Superior
        {
            return _Table<A>().ToList();
        }

        [NonAction]
        public Type _GetById(long id)
        {
            return _GetById<Type>(id);
        }

        [NonAction]
        public A _GetById<A>(long id) where A : Superior
        {
            return _GetSingle<A>(c => !c.IsDeleted && c.ID.Equals(id));
        }

        [NonAction]
        public bool Remove(Type model)
        {
            return _Remove<Type>(model);
        }

        [NonAction]
        public bool _Remove<A>(A model) where A : Superior
        {
            model.IsDeleted = true;
            _Save();
            return true;
        }

        [NonAction]
        public bool _Remove(long id)
        {
            return _Remove<Type>(id);
        }

        [NonAction]
        public bool _Remove<A>(long id) where A : Superior
        {
            A data = _GetSingle<A>(c => !c.IsDeleted && c.ID.Equals(id));
            return _Remove<A>(data);
        }

        [NonAction]
        public int _Save()
        {
            return _context.SaveChanges();
        }

        [NonAction]
        public bool _Update(Type model)
        {
            return _Update<Type>(model);
        }

        [NonAction]
        public bool _Update<A>(A model) where A : Superior
        {
            A data = _GetById<A>(model.ID);

            var props = typeof(A).GetProperties();
            var baseProps = typeof(Superior).GetProperties();
            foreach (var prop in props)
                if (!baseProps.Any(c => c.Name.Equals(prop.Name)))
                    prop.SetValue(data, prop.GetValue(model) ?? prop.GetValue(data));

            model.UpdatedAt = DateTime.Now;
            _Save();
            return true;
        }

        [NonAction]
        public List<Type> _GetWhere(Expression<Func<Type, bool>> metot)
        {
            return _GetWhere<Type>(metot);
        }

        [NonAction]
        public bool _GetAny<A>(Expression<Func<A, bool>> metot) where A : Superior
        {
            return _Table<A>().Any(metot);
        }

        [NonAction]
        public List<A> _GetWhere<A>(Expression<Func<A, bool>> metot) where A : Superior
        {
            return _Table<A>().Where(metot).ToList();
        }

        [NonAction]
        public Type _GetSingle(Func<Type, bool> metot)
        {
            return _GetSingle<Type>(metot);
        }

        [NonAction]
        public A _GetSingle<A>(Func<A, bool> metot) where A : Superior
        {
            return _Table<A>().FirstOrDefault(metot);
        }

        [NonAction]
        public int _Count()
        {
            return _Table().Count();
        }

        [NonAction]
        public int _Count(Expression<Func<Type, bool>> metot)
        {
            return _Table().Count(metot);
        }

        [NonAction]
        public void _BeginTran()
        {
            _context.Database.BeginTransaction();
        }

        [NonAction]
        public void _Commit()
        {
            _context.Database.CommitTransaction();
        }

        [NonAction]
        public void _Rollback()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
