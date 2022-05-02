using Libs.Base.Entities;

namespace Libs.Base.Triggers
{
    public abstract class DeleteTrigger<T> : Trigger<T>
         where T : EntityBase
    {
    }
}
