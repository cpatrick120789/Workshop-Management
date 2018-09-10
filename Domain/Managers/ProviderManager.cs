using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Entities;

namespace Domain.Managers
{
    public class ProviderManager:GenericManager<Provider>
    {
        public ProviderManager(GenericRepository<Provider> repository, Manager manager)
            : base(repository, manager)
        {
        }

        public ProviderManager(DbContext context, Manager manager) : base(context, manager)
        {
        }
        public override OperationResult<Provider> Add(Provider element)
        {
            var path = Path.Combine(Manager.ImageFolder, "default_provider.png");
            element.Image = (!string.IsNullOrEmpty(element.Image) && !string.IsNullOrWhiteSpace(element.Image))
                                ? element.Image
                                : path;
            return base.Add(element);
        }

        public override void Seed()
        {
//            for (int i = 0; i < 30; i++)
//            {
//                var p = new Provider()
//                {
//                    Name = "Proveedor-"+i
//                };
//                Manager.Provider.Add(p);
//            }
//            Manager.Provider.SaveChanges();
        }
    }
}
