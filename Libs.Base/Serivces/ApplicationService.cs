using AutoMapper;
using Libs.Base.Commands;
using Libs.Base.Entities;
using Libs.Base.Entities.Interfaces;
using Libs.Base.Models;
using Libs.Base.Models.Requests;
using Libs.Base.Models.Responses;
using Libs.Base.Repositories;
using Libs.Base.Serivces.Interfaces;
using System;
using System.Linq;

namespace Libs.Base.Serivces
{
    public abstract class ApplicationService<T> : IApplicationService<T> where T : EntityBase
    {
        protected readonly IQueryService _queryService;
        protected readonly IManipulationService _manipulationService;
        protected readonly ITransactionRepository _transactionRepository;
        protected readonly IMapper _mapper;

        public ApplicationService(IQueryService queryService, IManipulationService manipulationService, ITransactionRepository transactionRepository, IMapper mapper)
        {
            _queryService = queryService;
            _manipulationService = manipulationService;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public virtual TResponse Get<TResponse>(long? id)
            where TResponse : Response<T>
        {
            T entity = _queryService.GetNotDeleted<T>(id.Value);
            return _mapper.Map<TResponse>(entity);
        }

        protected abstract PaginatedResponse<T> Get(PaginateRequest<T> request);
        protected abstract UpdateCommand<T> Update(UpdateCommand<T> command, UpdateRequest<T> request);
        protected abstract T Insert(InsertRequest<T> request);
        public PaginatedResponse<TResponse> Get<TResponse>(PaginateRequest<T> request)
            where TResponse : Response<T>
        {
            return _mapper.Map<PaginatedResponse<TResponse>>(Get(request));
        }

        public virtual TResponse Insert<TResponse>(InsertRequest<T> request)
            where TResponse : Response<T>
        {
            return Execute<TResponse>(() => _manipulationService.Insert<T>().Clone(Insert(request)).Execute());
        }

        public virtual TResponse Update<TResponse>(long? id, UpdateRequest<T> request)
            where TResponse : Response<T>
        {
            T entity = _queryService.Get<T>(id.Value);
            return Execute<TResponse>(() => Update(_manipulationService.Update<T>(entity), request).Execute());
        }
        protected virtual void Delete(T entity)
        {
            _manipulationService.Delete(entity).Execute();
        }

        public virtual void Delete(long id)
        {
            T entity = _queryService.Get<T>(id);
            if (entity == null)
                throw new Exception(typeof(T).Name + " not found");

            Execute(() => Delete(entity));
        }

        protected void Execute(Action action)
        {
            _transactionRepository.BeginTransaction();
            try
            {
                action();
                _transactionRepository.Commit();
            }
            catch
            {
                _transactionRepository.Rollback();
                throw;
            }
        }
        protected TResponse Execute<TResponse>(Func<T> fucn)
        {
            T entity = default;
            Execute(() => entity = fucn());
            return _mapper.Map<TResponse>(entity);
        }
    }
}
