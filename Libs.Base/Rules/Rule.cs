namespace Libs.Base.Rules
{
    public abstract class Rule<T>
    {
        public abstract string ErroMenssage { get; }
        public abstract bool IsValid(T entity);
    }
}
