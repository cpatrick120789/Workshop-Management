using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Validation
{
    public class RoleValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var text = value.ToString();
            var list = Enum.GetNames(typeof (Rol));

            return !list.Contains(text) ? new ValidationResult("Rol Invalido") : ValidationResult.Success;
        }
    }
}
