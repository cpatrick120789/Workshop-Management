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
    public class RepairAccessoryManager:GenericManager<RepairAccessory>
    {
        public RepairAccessoryManager(GenericRepository<RepairAccessory> repository, Manager manager)
            : base(repository, manager)
        {
        }

        public RepairAccessoryManager(DbContext context, Manager manager)
            : base(context, manager)
        {
        }
    }
}
