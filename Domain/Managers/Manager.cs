using System.Net.Mime;
using System.Runtime.InteropServices;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Managers
{
    public class Manager
    {
         
        public string ImageFolder { get; set; }
        public AccessoryManager Accessory { get; set; }
        public ClientManager Client { get; set; }
        public MechanicManager Mechanic { get; set; }
        public ProviderAccessoryManager ProviderAccessory { get; set; }
        public ProviderManager Provider { get; set; }
        public RepairAccessoryManager RepairAccessory { get; set; }
        public RepairManager Repair { get; set; }
        public ServiceManager Service { get; set; }
        public UserManager User { get; set; }
        public OrderManager Order { get; set; }
        public MechanicOrderManager MechanicOrder { get; set; }

        public static Manager _Manager { get; set; }
        public static Manager SingleManager(string imageFolder = "Resources\\Images")
        {
            return _Manager ?? (_Manager = new Manager(imageFolder));
        }

        public Manager(string imageFolder="Images")
        {
            if (!Check())
                throw new DomainException();
            ImageFolder = imageFolder;
            var context = new workshopEntities();
            Accessory = new AccessoryManager(context, this);
            Client = new ClientManager(context, this);
            Mechanic = new MechanicManager(context, this);
            ProviderAccessory = new ProviderAccessoryManager(context, this);
            Provider = new ProviderManager(context, this);
            RepairAccessory = new RepairAccessoryManager(context, this);
            Repair = new RepairManager(context, this);
            Service = new ServiceManager(context, this);
            User = new UserManager(context, this);
            Order = new OrderManager(context, this);
            MechanicOrder = new MechanicOrderManager(context, this);
        }

        public void Seed()
        {
            var properties = GetType().GetProperties().Where(t =>t.PropertyType!=typeof(Manager) && t.PropertyType.GetMethod("Seed") != null);
            foreach (var propertyInfo in properties)
            {
                propertyInfo.PropertyType.GetMethod("Seed").Invoke(propertyInfo.GetMethod.Invoke(this, null), null);
            }
        }

        public GenericManager<T> GetManager<T>() where T : class
        {
            var property = GetType().GetProperties().FirstOrDefault(t =>
                t.PropertyType.IsSubclassOf(typeof(GenericManager<T>)));
            if (property != null)
                return property.GetMethod.Invoke(this, null) as GenericManager<T>;
            return null;
        }

        public bool Check()
        {
            return Security.License.CheckLisence();
        }

    }
}
