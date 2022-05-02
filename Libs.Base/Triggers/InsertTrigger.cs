using Libs.Base.Entities;

namespace Libs.Base.Triggers
{
    public abstract class InsertTrigger<T> : Trigger<T>
         where T : EntityBase
    {
    }
}
