using Libs.Base.Entities;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace Libs.Base.Serivces.Interfaces
{
    public interface IQueryService
    {
        T Get<T>(Expression<Func<T, bool>> expression) where T : EntityBase;
        T Get<T>(long id) where T : EntityBase;
        IQueryable<T> Query<T>() where T : EntityBase;
        T Validate<T>(long? id) where T : EntityBase;
        T Refresh<T>(T entity) where T : EntityBase;
    }
}
