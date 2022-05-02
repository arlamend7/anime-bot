using Libs.Base.Entities;
using Libs.Base.Entities.Interfaces;
using Libs.Base.Repositories;
using Libs.Base.Serivces.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Libs.Base.Serivces
{
    public class QueryService : IQueryService
    {
        private readonly IQueryRepository _repository;

        public QueryService(IQueryRepository repository)
        {
            _repository = repository;
        }

        public virtual T Refresh<T>(T entity)
             where T : EntityBase
        {
            _repository.Refresh(entity);
            return entity;
        }

        public virtual T Validate<T>(long? id)
            where T : EntityBase
        {
            if (!id.HasValue) throw new Exception(typeof(T).Name + " index not found");
            T entity = _repository.Get<T>(id.Value);
            if (entity == null) throw new Exception(typeof(T).Name + "not found");
            if (entity is ILogicDelete logicDelete && logicDelete.IsDeleted)
            {
                throw new Exception(typeof(T).Name + " is deleted");
            }
            return entity;
        }

        public virtual T Get<T>(Expression<Func<T, bool>> expression)
                        where T : EntityBase

        {
            return _repository.Get(expression);
        }

        public virtual T Get<T>(long id)
                        where T : EntityBase

        {
            return _repository.Get<T>(id);
        }

        public virtual T GetNotDeleted<T>(long id)
                        where T : EntityBase

        {
            T entity = Get<T>(id);
            if (entity is ILogicDelete logicDelete && logicDelete.IsDeleted)
            {
                return default;
            }
            return entity;
        }

        public virtual IQueryable<T> Query<T>()
                        where T : EntityBase

        {
            return _repository.Query<T>();
        }
    }
}
