using System;
using System.Collections.Generic;
using System.Text;

namespace Libs.Base.Extensions
{
    public static class TypeExtensions
    {
        public static bool ImplementsOrDerives(this Type @this, Type from)
        {
            if (from is null)
            {
                return false;
            }
            else if (!from.IsGenericType)
            {
                return from.IsAssignableFrom(@this);
            }
            else if (!from.IsGenericTypeDefinition)
            {
                return from.IsAssignableFrom(@this);
            }
            else if (from.IsInterface)
            {
                foreach (Type @interface in @this.GetInterfaces())
                {
                    if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == from)
                    {
                        return true;
                    }
                }
            }

            if (@this.IsGenericType && @this.GetGenericTypeDefinition() == from)
            {
                return true;
            }

            if (@this.BaseType == null)
            {
                return false;
            }
            return ImplementsOrDerives(@this.BaseType, from);
        }
    }
}
