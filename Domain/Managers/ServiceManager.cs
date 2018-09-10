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
    public class ServiceManager:GenericManager<Service>
    {
        public ServiceManager(GenericRepository<Service> repository, Manager manager)
            : base(repository, manager)
        {
        }

        public ServiceManager(DbContext context, Manager manager)
            : base(context, manager)
        {
        }

        public void RemoveMechanic(Service service,Mechanic mechanic)
        {
            service.Mechanic.Remove(mechanic);
            mechanic.Service.Remove(service);
            Manager.Service.SaveChanges();
        }

        public override void Seed()
        {
//            Service s  = new Service();
//            var list = new List<Service>();
//            
//            Random randon = new Random();
//            for (int i = 0; i < 30; i++){
//                var servicio = new Service() {Name = "Servicio-" + i, Price = randon.NextDouble()};
//                list.Add(servicio);
//            }
//
//            AddOrUpdate(t=> t.Id, list.ToArray());
        }
    }
}
