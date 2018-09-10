using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
   public class OperationResult<T> where T : class
    {
       public T Entity { get; set; }
       public bool Success { get; set; }
       public List<string> Errors { get; set; }
       public OperationResult(T entity)
       {
           Errors = new List<string>();
           Entity = entity;
       }
    }
}
