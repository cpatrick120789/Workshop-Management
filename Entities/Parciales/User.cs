using System;
using System.ComponentModel.DataAnnotations;
using Entities.Validation;

namespace Entities
{
    
    public partial class User
    {
        [RoleValidation]
        public Rol RolEnum
        {
            get { return (Rol)Enum.Parse(typeof(Rol), Rol.ToString()); }
            set
            {
               Rol = (long)value;
            }
        }
    }
}
