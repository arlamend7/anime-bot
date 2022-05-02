using Libs.Base.Entities;
using Libs.Base.Models;
using Libs.Base.Models.Requests;
using Libs.Base.Models.Responses;

namespace Libs.Base.Serivces.Interfaces
{
    public interface IApplicationService<T>
        where T : EntityBase
    {
        void Delete(long id);
        TResponse Get<TResponse>(long? id) where TResponse : Response<T>;
        PaginatedResponse<TResponse> Get<TResponse>(PaginateRequest<T> request) where TResponse : Response<T>;
        TResponse Insert<TResponse>(InsertRequest<T> request) where TResponse : Response<T>;
        TResponse Update<TResponse>(long? id, UpdateRequest<T> request) where TResponse : Response<T>;
    }
}