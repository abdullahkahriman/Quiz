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
        public DbSet<Type> Table()
        {
            return Table<Type>();
        }

        [NonAction]
        public DbSet<A> Table<A>() where A : class
        {
            return _context.Set<A>();
        }

        [NonAction]
        public bool Add(Type model)
        {
            return Add<Type>(model);
        }

        [NonAction]
        public bool Add<A>(A model) where A : Superior
        {
            model.IsDeleted = false;
            Table<A>().Add(model);
            Save();
            return true;
        }

        [NonAction]
        public List<Type> Get()
        {
            return Get<Type>();
        }

        [NonAction]
        public List<A> Get<A>() where A : Superior
        {
            return Table<A>().ToList();
        }

        [NonAction]
        public Type GetById(long id)
        {
            return GetById<Type>(id);
        }

        [NonAction]
        public A GetById<A>(long id) where A : Superior
        {
            return GetSingle<A>(c => !c.IsDeleted && c.ID.Equals(id));
        }

        [NonAction]
        public bool Remove(Type model)
        {
            return Remove<Type>(model);
        }

        [NonAction]
        public bool Remove<A>(A model) where A : Superior
        {
            model.IsDeleted = true;
            Save();
            return true;
        }

        [NonAction]
        public bool Remove(long id)
        {
            return Remove<Type>(id);
        }

        [NonAction]
        public bool Remove<A>(long id) where A : Superior
        {
            A data = GetSingle<A>(c => !c.IsDeleted && c.ID.Equals(id));
            return Remove<A>(data);
        }

        [NonAction]
        public int Save()
        {
            return _context.SaveChanges();
        }

        [NonAction]
        public bool Update(Type model, long id)
        {
            return Update<Type>(model, id);
        }

        [NonAction]
        public bool Update<A>(A model, long id) where A : Superior
        {
            A data = GetById<A>(id);

            var props = typeof(A).GetProperties();
            var baseProps = typeof(Superior).GetProperties();
            foreach (var prop in props)
                if (!baseProps.Any(c => c.Name.Equals(prop.Name)))
                    prop.SetValue(data, prop.GetValue(model) ?? prop.GetValue(data));

            model.UpdatedAt = DateTime.Now;
            Save();
            return true;
        }

        [NonAction]
        public List<Type> GetWhere(Expression<Func<Type, bool>> metot)
        {
            return GetWhere<Type>(metot);
        }

        [NonAction]
        public List<A> GetWhere<A>(Expression<Func<A, bool>> metot) where A : Superior
        {
            return Table<A>().Where(metot).ToList();
        }

        [NonAction]
        public Type GetSingle(Func<Type, bool> metot)
        {
            return GetSingle<Type>(metot);
        }

        [NonAction]
        public A GetSingle<A>(Func<A, bool> metot) where A : Superior
        {
            return Table<A>().FirstOrDefault(metot);
        }

        [NonAction]
        public void BeginTran()
        {
            _context.Database.BeginTransaction();
        }

        [NonAction]
        public void Commit()
        {
            _context.Database.CommitTransaction();
        }

        [NonAction]
        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
