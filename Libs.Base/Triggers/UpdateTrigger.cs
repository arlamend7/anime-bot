using Libs.Base.Entities;

namespace Libs.Base.Triggers
{
    public abstract class UpdateTrigger<T> : Trigger<T>
         where T : EntityBase
    {
    }
}
