using Libs.Base.Entities;

namespace Libs.Base.Repositories
{
    public interface IQueryRepository
    {
        object GetAll<T>() where T : EntityBase;
        T Get<T>(long id) where T : EntityBase;
        void Refresh<T>(T entity) where T : EntityBase;
    }
}
