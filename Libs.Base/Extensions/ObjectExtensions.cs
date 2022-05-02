using Libs.Base.Entities;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Libs.Base.Extensions
{
    public static class ObjectExtensions
    {
        public static void SetValue<T, TProperty>(this T classe, Expression<Func<T, TProperty>> expression, TProperty value)
           where T : EntityBase
        {
            Type type = typeof(T);
            string expressionPropertyName = ((MemberExpression)expression.Body).Member.Name;
            PropertyInfo property = type.GetProperty(expressionPropertyName);

            property.Validate(value);

            property.SetValue(classe, value);
        }
    }
}
