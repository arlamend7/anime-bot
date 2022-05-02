using Libs.Base.Commands.Base;
using Libs.Base.Entities;
using Libs.Base.Extensions;
using Libs.Base.Injectors.Interfaces;
using Libs.Base.Repositories;
using Libs.Base.Rules;
using Libs.Base.Triggers;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Libs.Base.Commands
{
    public class UpdateCommand<T> : Command<T>
         where T : EntityBase
    {
        public UpdateCommand(IManipulationRepository manipulationRepository, IInjector<Rule<T>> rules, IInjector<UpdateTrigger<T>> triggers) 
            : base(manipulationRepository, rules.GetAll(), triggers.GetAll())
        {
            
        }
        
        public UpdateCommand<T> SetEntity(T entity)
        {
            Entity = entity;
            return this;
        }

        public Command<T> Set<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value)
        {
            string expressionPropertyName = ((MemberExpression)expression.Body).Member.Name;
            PropertyInfo property = Type.GetProperty(expressionPropertyName);
            object valueInEntity = property.GetValue(Entity);

            if (valueInEntity == null || !valueInEntity.Equals(value))
            {
                Entity.SetValue(expression, value);
            }

            return this;
        }

        public UpdateCommand<T> SetIfHasValue<TProperty>(Expression<Func<T, TProperty>> expression, TProperty value)
        {
            if (value != null && !value.Equals(default(TProperty)))
            {
                Set(expression, value);
            }

            return this;
        }

        protected override void Execute(T entity)
        {
            _manipulationRepository.Edit(entity);
        }
    }
}
