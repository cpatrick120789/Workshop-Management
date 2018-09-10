using System;
using System.CodeDom;
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
    public class MechanicManager : GenericManager<Mechanic>
    {
        public MechanicManager(GenericRepository<Mechanic> repository, Manager manager)
            : base(repository, manager)
        {
        }

        public MechanicManager(DbContext context, Manager manager)
            : base(context, manager)
        {
        }

        public override OperationResult<Mechanic> Add(Mechanic element)
        {
            element.Code = Guid.NewGuid().ToString();
            var path = Path.Combine(Manager.ImageFolder, "default.png");
            element.Image = (!string.IsNullOrEmpty(element.Image) && !string.IsNullOrWhiteSpace(element.Image))
                                ? element.Image
                                : path;
            return base.Add(element);
        }

        public override void Seed()
        {
//            var mechanicList = new List<Mechanic>();
//            Random serviceRandon = new Random();
//            for (int i = 0; i < 50; i++)
//            {
//                var mechanic = new Mechanic()
//                {
//                    Name = Security.Criptografia.GenerateString(6),
//                    LastName = Security.Criptografia.GenerateString(5),
//                    Service = new LinkedList<Service>()
//                };
//                var m = serviceRandon.Next(1, 5);
//                for (int j = 0; j < m; j++)
//                {
//                    mechanic.Service.Add(Manager.Service.Find(serviceRandon.Next(1,30)));
//                }
//
//                Manager.Mechanic.Add(mechanic);
//            }
//            Manager.Mechanic.SaveChanges();
//            AddOrUpdate(t => t.Id, mechanicList.ToArray());
        }
    }
}
