using Animes.Domain.Episodios.Entities;
using Libs.Base.Models;

namespace Animes.Applications.Episodes.DataTransfers.Requests
{
    public class EpisodeQueryRequest : PaginateRequest<Episode>
    {
        public long? AnimeId { get; set; }
        public int? Number { get; set; }
    }
}
