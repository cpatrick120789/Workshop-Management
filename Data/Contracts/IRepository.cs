using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Entities;

namespace Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        T Find(params object[] keys);
        bool Delete(T element,bool saveChanges=false);
        T Add(T element, bool saveChanges = false);
        T Modify(T element, bool saveChanges = false,params string []properties);
        IEnumerable<T> Get(Paginacion paginacion=null);
        IEnumerable<T> Get(Func<T, bool> filter = null, Paginacion paginacion = null);
        int SaveChanges();
        void AddOrUpdate(Expression<Func<T, object>> expression, params T[] entites);
    }
}
