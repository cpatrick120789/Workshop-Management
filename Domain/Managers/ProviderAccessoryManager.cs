using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Entities;

namespace Domain.Managers
{
    public class ProviderAccessoryManager:GenericManager<ProviderAccessory>
    {
        public ProviderAccessoryManager(GenericRepository<ProviderAccessory> repository, Manager manager)
            : base(repository, manager)
        {
        }

        public ProviderAccessoryManager(DbContext context, Manager manager)
            : base(context, manager)
        {
        }

        public override OperationResult<ProviderAccessory> Add(ProviderAccessory element)
        {
            var proAccessory = base.Add(element);
            
            element.Accessory.Amount += element.Amount;
            Manager.Accessory.Modify(element.Accessory);
            Manager.Accessory.SaveChanges();

            return proAccessory;

        }
    }
}
