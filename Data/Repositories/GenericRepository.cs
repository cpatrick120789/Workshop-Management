using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using Data.Contracts;
using Entities;

namespace Data.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public DbContext Context { get; set; }
        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        public T Find(params object[] keys)
        {

            return Context.Set<T>().Find(keys);
        }

        public bool Delete(T element, bool saveChanges = false)
        {
            if (element == null)
                return false;
            Context.Entry(element).State = EntityState.Deleted;
            if (saveChanges)
                SaveChanges();
            return true;
        }

        public T Add(T element, bool saveChanges = false)
        {
            Context.Entry(element).State = EntityState.Added;
            if (saveChanges)
                SaveChanges();
            return element;
        }

        public T Modify(T element, bool saveChanges = false, params string[] properties)
        {
            Context.Entry(element).State = EntityState.Modified;
            var type = typeof(T);
            var all = type.GetProperties();
            var prop = properties.Where(t => all.Any(h => h.Name == t));
            foreach (var property in prop)
            {
                Context.Entry(element).Property(property).IsModified = false;
            }
            if (saveChanges)
                SaveChanges();
            return element;
        }

        public IEnumerable<T> Get(Paginacion paginacion = null)
        {
            return paginacion == null
                       ? Context.Set<T>()
                       : Context.Set<T>().Skip((paginacion.Page - 1) * paginacion.ItemsPerPage).Take(
                           paginacion.ItemsPerPage);
        }
        public IEnumerable<T> Get(Func<T, bool> filter, Paginacion paginacion = null)
        {
            IEnumerable<T> list = Context.Set<T>();
            if (filter != null)
                 list = Context.Set<T>().Where(filter);
            if (paginacion != null) return list.Skip((paginacion.Page - 1) * paginacion.ItemsPerPage).Take(
                       paginacion.ItemsPerPage);
            return list;
        }

        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public void AddOrUpdate(Expression<Func<T, object>> expression, params T[] entites)
        {
            Context.Set<T>().AddOrUpdate(expression, entites);
        }
    }
}
