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
    public class AccessoryManager : GenericManager<Accessory>
    {
        public AccessoryManager(GenericRepository<Accessory> repository, Manager manager)
            : base(repository, manager)
        {
        }

        public AccessoryManager(DbContext context, Manager manager)
            : base(context, manager)
        {
        }

        public override OperationResult<Accessory> Add(Accessory element)
        {
            element.Code = Guid.NewGuid().ToString();
            return base.Add(element);
        }

        public override void Seed()
        {
//            Random r = new Random();
//            for (int i = 0; i < 500; i++)
//            {
//
//                var accesorie = new Accessory()
//                {
//                    Name = "Accesorio-" + i,
//                    Price = r.Next(5000),
//                    Amount = r.Next(1, 100),
//                    Description = Security.Criptografia.GenerateString(20)
//                };
//
//                Manager.Accessory.Add(accesorie);
//                Manager.Accessory.SaveChanges();
//                
//                
//                for (int j = 0; j < r.Next(1, 5); j++)
//                {
//                    var providerAcc = new ProviderAccessory()
//                    {
//                        Amount = r.Next(1, 100),
//                        Date = DateTime.Now,
//                        Accessory = accesorie,
//                        Provider = Manager.Provider.Find(r.Next(1, 30))
//                    };
//                    Manager.ProviderAccessory.Add(providerAcc);
//                }
//               
//                Manager.ProviderAccessory.SaveChanges();
//            }
        }
    }
}
