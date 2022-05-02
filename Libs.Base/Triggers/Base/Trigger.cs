using Libs.Base.Triggers.Enums;

namespace Libs.Base.Triggers
{
    public abstract class Trigger<T>
    {
        public abstract TriggerOnResult OnResult { get; }
        protected abstract void OnTrigger(T entity);
        public void Execute(T entity)
        {
            try
            {
                OnTrigger(entity);
            }
            catch (System.Exception)
            {
                if (OnResult == TriggerOnResult.Stop)
                {
                    throw;
                }
            }
        }
    }
}    
