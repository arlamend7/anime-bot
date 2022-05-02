using Libs.Base.Entities;

namespace Libs.Base.Triggers.Interfaces
{
    public interface ITriggerAfter<T>
        where T : EntityBase
    {
        void OnAfter(T entity);
    }
}
