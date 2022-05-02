using Libs.Base.Entities;
using Libs.Base.Repositories;
using Libs.Base.Rules;
using Libs.Base.Triggers;
using Libs.Base.Triggers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Libs.Base.Commands.Base
{
    public abstract class Command<T>
        where T : EntityBase
    {
        public Type Type => typeof(T);
        public T Entity { get; protected set; }   

        protected readonly IManipulationRepository _manipulationRepository;
        public readonly IEnumerable<Rule<T>> _rules;
        public readonly IEnumerable<Trigger<T>> _triggers;

        public Command(IManipulationRepository manipulationRepository, IEnumerable<Rule<T>> rules, IEnumerable<Trigger<T>> triggers)
        {
            _manipulationRepository = manipulationRepository;
            _rules = rules;
            _triggers = triggers;
        }
        
        protected abstract void Execute(T entity);
        public T Execute()
        {
            Rule<T> rule = _rules.AsParallel().FirstOrDefault(x => !x.IsValid(Entity));
            if (rule != null)
            {
                throw new Exception($"{ rule.GetType().Name } : {rule.ErroMenssage}");
            }
            
            _triggers.Where(x => x is ITriggerBefore<T>)
                     .AsParallel()
                     .ForAll(x => (x as ITriggerBefore<T>).OnBefore(Entity));
            
            Execute(Entity);
            
            _triggers.AsParallel()
                          .ForAll(x => x.Execute(Entity));

            _triggers.Where(x => x is ITriggerAfter<T>)
                     .AsParallel()
                     .ForAll(x => (x as ITriggerAfter<T>).OnAfter(Entity));

            return Entity;
        }
    }
}
