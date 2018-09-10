using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using Entities;
using Security;

namespace Domain.Managers
{
    public class UserManager:GenericManager<User>
    {
        public UserManager(GenericRepository<User> repository, Manager manager) : base(repository, manager)
        {
        }

        public UserManager(DbContext context, Manager manager) : base(context, manager)
        {
        }

        public override OperationResult<User> Add(User element)
        {
            var path = Path.Combine(Manager.ImageFolder, "default.png");
            element.Image = (!string.IsNullOrEmpty(element.Image) && !string.IsNullOrWhiteSpace(element.Image))
                                ? element.Image
                                : path;
            element.Password = element.Password.GetMd5();
            return base.Add(element);
        }
        public User Login(string userName,string password)
        {
            password = password.GetMd5();
            return Get(t => t.Username == userName && t.Password == password).FirstOrDefault();
        }

        public override void Seed()
        {
            var user = new User()
                {
                    Id = 1,
                    Name = "Super",
                    LastName = "Administrador",
                    Password = "superadmin".GetMd5(),
                    Username = "superadmin",
                    RolEnum=Rol.Administrador,
                    Image = Path.Combine(Manager.ImageFolder,"default.png")

                };
            AddOrUpdate(t=>t.Name,user);
        }

        public override Dictionary<string, string> Validate(User element)
        {
            var dic = base.Validate(element);
            if (string.IsNullOrEmpty(element.Name))
                dic.Add("Name", "El Nombre no puede estar vacío");
            return dic;
        }
    }
}
