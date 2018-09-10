
using System.Data.Entity;
using System.Linq.Expressions;
using Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Entities;


namespace Domain.Managers
{
    public abstract class GenericManager<T> where T : class
    {
        protected GenericRepository<T> Repository { get; set; }
        protected Manager Manager { get; set; }

        protected GenericManager(GenericRepository<T> repository, Manager manager)
        {
            Repository = repository;
            Manager = manager;
        }

        protected GenericManager(DbContext context, Manager manager)
        {
            Repository = new GenericRepository<T>(context);
            Manager = manager;
        }
        public virtual IEnumerable<T> Get(Func<T, bool> filter = null, Paginacion paginacion = null)
        {
            return Repository.Get(filter, paginacion);
        }
        public virtual T Find(params object[] keys)
        {
            return Repository.Find(keys);
        }
        public virtual OperationResult<T> Add(T element)
        {
            var errors = Validate(element);
            if (errors.Count == 0)
            {
                try
                {
                    var entity = Repository.Add(element);
                    return new OperationResult<T>(entity) { Success = true };
                }
                catch (Exception)
                {
                    return new OperationResult<T>(null) { Errors = new List<string>() { "Imposible efectuar la operación." } };
                }
            }
            return new OperationResult<T>(null) { Errors = errors.Select(t => t.Value).ToList() };
        }
        
        public virtual OperationResult<T> Modify(T element,params string [] properties)
        {
            var errors = Validate(element);
            if (errors.Count == 0)
            {
                try
                {
                    var entity = Repository.Modify(element,false,properties);
                    return new OperationResult<T>(entity) { Success = true };
                }
                catch (Exception)
                {
                    return new OperationResult<T>(null) { Errors = new List<string>() { "Imposible efectuar la operación." } };
                }
            }
            return new OperationResult<T>(null) { Errors = errors.Select(t => t.Value).ToList() };
        }
        
        public virtual OperationResult<T> Delete(T element)
        {
            try
            {
                Repository.Delete(element);
                return new OperationResult<T>(element) { Success = true };
            }
            catch (Exception)
            {
                return new OperationResult<T>(null) { Errors = new List<string>() { "Imposible efectuar la operación." } };
            }

        }
        public virtual OperationResult<T> Delete(params object[] keys)
        {
            try
            {
                var item = Find(keys);
                return item != null ? Delete(item) 
                    : new OperationResult<T>(null) { Errors = new List<string>() { "No se pudo encontrar el elemento." } };
            }
            catch (Exception)
            {
                return new OperationResult<T>(null) { Errors = new List<string>() { "Imposible efectuar la operación." } };
            }

        }
        public virtual int SaveChanges()
        {
            return Repository.SaveChanges();
        }
        public virtual Dictionary<string, string> Validate(T element)
        {
            return new Dictionary<string, string>();
        }

        public virtual void AddOrUpdate(Expression<Func<T,object>> expression,params T[] entites)
        {
            Repository.AddOrUpdate(expression,entites);
            Repository.SaveChanges();
        }
        public virtual void Seed()
        {
            
        }

        

    }
}
