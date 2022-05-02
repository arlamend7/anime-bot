using Animes.Applications.Animes.DataTransfers.Requests;
using Animes.Applications.Episodes.DataTransfers.Requests;
using Animes.Domain.Episodios.Entities;
using AutoMapper;
using Libs.Base.Commands;
using Libs.Base.Models;
using Libs.Base.Models.Requests;
using Libs.Base.Repositories;
using Libs.Base.Serivces;
using Libs.Base.Serivces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Applications.Episodes.Services
{
    public class EpisodesAppService : ApplicationService<Episode>
    {
        public EpisodesAppService(IQueryService queryService, IManipulationService manipulationService, ITransactionRepository transactionRepository, IMapper mapper) : base(queryService, manipulationService, transactionRepository, mapper)
        {
        }

        protected override IQueryable<Episode> Filters(IQueryable<Episode> query, PaginateRequest<Episode> request)
        {
            EpisodeQueryRequest QueryRequest = request as EpisodeQueryRequest;

            if (QueryRequest.AnimeId.HasValue)
            {
                query = query.Where(x => x.Anime.Id == QueryRequest.AnimeId.Value);
            }
            if (QueryRequest.Number.HasValue)
            {
                query = query.Where(x => x.Number == QueryRequest.Number.Value);
            }
            return query;
        }

        protected override Episode Insert(InsertCommand<Episode> command, InsertRequest<Episode> request)
        {
            throw new NotImplementedException();
        }

        protected override Episode Update(UpdateCommand<Episode> command, UpdateRequest<Episode> request)
        {
            throw new NotImplementedException();
        }
    }
}
