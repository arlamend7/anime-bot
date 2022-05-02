using Libs.Base.Commands;
using Libs.Base.Entities;

namespace Libs.Base.Serivces.Interfaces
{
    public interface IManipulationService
    {
        DeleteCommand<T> Delete<T>(T entity) where T : EntityBase;
        UpdateCommand<T> Update<T>(T entity) where T : EntityBase;
        InsertCommand<T> Insert<T>() where T : EntityBase;
    }
}
