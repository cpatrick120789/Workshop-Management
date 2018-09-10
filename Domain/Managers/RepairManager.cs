using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Entities;

namespace Domain.Managers
{
    public class RepairManager:GenericManager<Repair>
    {
        public RepairManager(GenericRepository<Repair> repository, Manager manager)
            : base(repository, manager)
        {
        }

        public RepairManager(DbContext context, Manager manager)
            : base(context, manager)
        {
        }
        public override OperationResult<Repair> Add(Repair element)
        {
            element.Code = Guid.NewGuid().ToString();
            return base.Add(element);
        }

        public override void Seed()
        {
            var listRepair = new List<Repair>();
            Random r = new Random();
            for (var i = 0; i < 100; i++)
            {
                var listM = new List<Mechanic>();
                for (int j = 0; j < r.Next(1,4); j++)
                {
                    var mechanic = Manager.Mechanic.Find(r.Next(1,50));
                    listM.Add(mechanic);
                }

                var repair = new Repair()
                {
                    Date = DateTime.Now,
                    Description = Security.Criptografia.GenerateString(50),
                    Client = Manager.Client.Find(r.Next(10,200)),
                   // Service = Manager.Service.Find(r.Next(r.Next(1, 30))),
                    //Mechanic = listM
                };
            }
        }
    }
}
