using Animes.Applications.Animes.DataTransfers;
using Animes.Domain.Animes.Entities;
using AutoMapper;
using Libs.Base.Commands;
using Libs.Base.Models;
using Libs.Base.Models.Requests;
using Libs.Base.Repositories;
using Libs.Base.Serivces;
using Libs.Base.Serivces.Interfaces;

namespace Animes.Applications.Animes.Services
{
    public class AnimesAppService : ApplicationService<Anime>
    {
        public AnimesAppService(IQueryService queryService, IManipulationService manipulationService, ITransactionRepository transactionRepository, IMapper mapper) : base(queryService, manipulationService, transactionRepository, mapper)
        {
        }

        protected override PaginatedResponse<Anime> Get(PaginateRequest<Anime> request)
        {
            AnimeQueryRequest QueryRequest = request as AnimeQueryRequest;

            var query = _queryService.GetAll<Anime>() as IQueryable<Anime>;
            
            if (!string.IsNullOrWhiteSpace(QueryRequest.Name))
            {
                query = query.Where(x => x.Name.Contains(QueryRequest.Name));
            }
            if (!string.IsNullOrWhiteSpace(QueryRequest.ExactlyName))
            {
                query = query.Where(x => x.Name == QueryRequest.ExactlyName);
            }

        }

        protected override Anime Insert(InsertRequest<Anime> request)
        {
            AnimeInsertRequest InsertRequest = request as AnimeInsertRequest;
            return new Anime(){

            };
        }

        protected override UpdateCommand<Anime> Update(UpdateCommand<Anime> command, UpdateRequest<Anime> request)
        {
            AnimeEditRequest InsertRequest = request as AnimeEditRequest;
            return command
                    .SetIfHasValue(x => x.ImageUrl, InsertRequest.ImageUrl)
        }
    }
}
