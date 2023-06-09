using Libs.Base.Commands.Base;
using Libs.Base.Entities;
using Libs.Base.Extensions;
using Libs.Base.Injectors.Interfaces;
using Libs.Base.Repositories;
using Libs.Base.Rules;
using Libs.Base.Triggers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Libs.Base.Commands
{
    public class InsertCommand<T> : Command<T>
         where T : EntityBase
    {
        public InsertCommand(IManipulationRepository manipulationRepository, IInjector<Rule<T>> rules, IInjector<InsertTrigger<T>> triggers)
            : base(manipulationRepository, rules.GetAll(), triggers.GetAll())
        {

        }

        public InsertCommand<T> Construct(params object[] parameters)
        {
            T entity = (T)Activator.CreateInstance(typeof(T), args: parameters);
            Entity = entity;
            
            Type.GetProperties()
                .Where(x => x.GetValue(entity) != null || x.GetCustomAttribute<RequiredAttribute>() != null)
                .AsParallel()
                .ForAll(x => x.Validate(x.GetValue(entity)));
                                
            return this;
        }
        
        public InsertCommand<T> Clone(T entity)
        {
            Entity = entity;
            return this;
        }
        
        protected override void Execute(T entity)
        {
            object key = _manipulationRepository.Save(Entity);
            Entity.SetId(key);
        }
    }
}
