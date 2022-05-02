using Libs.Base.Entities;

namespace Libs.Base.Repositories
{
    public interface IManipulationRepository
    {
        void Edit<T>(T entity) where T : EntityBase;
        long Save<T>(T entity) where T : EntityBase;
        void Delete<T>(T entity) where T : EntityBase;
    }
}
