using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Libs.Base.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static void Validate(this PropertyInfo property, object value)
        {
            IEnumerable<ValidationAttribute> attributes = property.GetCustomAttributes<ValidationAttribute>();

            foreach (ValidationAttribute attribute in attributes)
            {
                try
                {
                    attribute.Validate(value, new ValidationContext(value));
                }
                catch (Exception ex)
                {
                    throw new Exception($"{property.Name} - {ex.Message}");
                }
            }
        }
    }
}
