using Animes.Applications.Animes.DataTransfers;
using Animes.Applications.Animes.DataTransfers.Requests;
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

        protected override IQueryable<Anime> Filters(IQueryable<Anime> query, PaginateRequest<Anime> request)
        {
            AnimeQueryRequest QueryRequest = request as AnimeQueryRequest;
            if (!string.IsNullOrWhiteSpace(QueryRequest.Name))
            {
                query = query.Where(x => x.Name.Contains(QueryRequest.Name));
            }
            if (!string.IsNullOrWhiteSpace(QueryRequest.ExactlyName))
            {
                query = query.Where(x => x.Name == QueryRequest.ExactlyName);
            }
            return query;
        }

        protected override Anime Insert(InsertCommand<Anime> command, InsertRequest<Anime> request)
        {
            AnimeInsertRequest InsertRequest = request as AnimeInsertRequest;
            return command.Construct(InsertRequest.Name, InsertRequest.ImageUrl, InsertRequest.Link)
                        .Execute();

        }

        protected override Anime Update(UpdateCommand<Anime> command, UpdateRequest<Anime> request)
        {
            throw new NotImplementedException();
        }
    }
}
