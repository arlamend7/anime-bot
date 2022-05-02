using Libs.Base.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Libs.Base.Repositories
{
    public interface IQueryRepository
    {
        IQueryable<T> Query<T>() where T : EntityBase;
        T Get<T>(Expression<Func<T, bool>> expression) where T : EntityBase;
        T Get<T>(long id) where T : EntityBase;
        void Refresh<T>(T entity) where T : EntityBase;
    }
}
