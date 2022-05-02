namespace Libs.Base.Triggers.Interfaces
{
    public interface ITriggerBefore<T>
    {
        void OnBefore(T entity);
    }
}
