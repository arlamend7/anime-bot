using Libs.Base.Entities;
using Libs.Base.Entities.Interfaces;
using Libs.Base.Models;
using System.Linq;

namespace Libs.Base.Extensions
{
    public static class IQueryableExtensions
    {
        public static PaginatedResponse<T> Paginate<T>(this IQueryable<T> query, PaginateRequest request)
        {
            return Paginar(query, request.Qt, request.Pg);
        }

        public static PaginatedResponse<T> Paginar<T>(this IQueryable<T> query, int qt, int pg)
        {
            return new PaginatedResponse<T>()
            {
                Total = query.Count(),
                Registros = query.Skip(pg * qt).Take(qt)
            };
        }

        public static IQueryable<T> IsNotDeleted<T>(this IQueryable<T> query)
            where T : ILogicDelete
        {
            return query.Where(x => x.Deleted != true);
        }
    }
}
