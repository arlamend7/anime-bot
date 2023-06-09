using Libs.Base.Entities;

namespace Libs.Base.Serivces.Interfaces
{
    public interface IQueryService
    {
        T Get<T>(long id) where T : EntityBase;
        object GetAll<T>() where T : EntityBase;
        T Validate<T>(long? id) where T : EntityBase;
        T GetNotDeleted<T>(long id) where T : EntityBase;
    }
}
