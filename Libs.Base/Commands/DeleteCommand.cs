using Libs.Base.Commands.Base;
using Libs.Base.Entities;
using Libs.Base.Entities.Interfaces;
using Libs.Base.Injectors.Interfaces;
using Libs.Base.Repositories;
using Libs.Base.Rules;
using Libs.Base.Triggers;

namespace Libs.Base.Commands
{
    public class DeleteCommand<T> : Command<T>
        where T : EntityBase
    {
        public DeleteCommand(IManipulationRepository manipulationRepository, IInjector<Rule<T>> rules, IInjector<DeleteTrigger<T>> triggers)
            : base(manipulationRepository, rules.GetAll(), triggers.GetAll())
        {
        }
        
        public DeleteCommand<T> SetEntity(T entity)
        {
            Entity = entity;
            return this;
        }
        
        protected override void Execute(T entity)
        {
            if (entity is ILogicDelete deleteEntity)
            {
                LogicDelete(deleteEntity);
                return;
            }
            _manipulationRepository.Delete(entity);
        }

        private void LogicDelete(ILogicDelete logicDelete)
        {
            logicDelete.Deleted = true;
            _manipulationRepository.Save((T)logicDelete);
        }
    }
}
