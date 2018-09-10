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
    public class ClientManager : GenericManager<Client>
    {
        public ClientManager(GenericRepository<Client> repository, Manager manager)
            : base(repository, manager)
        {
        }

        public ClientManager(DbContext context, Manager manager)
            : base(context, manager)
        {
        }

        public override OperationResult<Client> Add(Client element)
        {
            element.Code = Guid.NewGuid().ToString();
            return base.Add(element);
        }

        public override void Seed()
        {
            //Random r = new Random();
            //for (var i = 0; i < 200; i++)
            //{
            //    var client = new Client()
            //    {
            //        Name = Security.Criptografia.GenerateString(r.Next(5, 8)),
            //        LastName = Security.Criptografia.GenerateString(r.Next(5, 8)),
            //        Identification = "client-" + i + "-" + Security.Criptografia.GenerateString(3)

            //    };
            //    Manager.Client.Add(client);
            //}
            //Manager.Client.SaveChanges();
        }
    }
}
