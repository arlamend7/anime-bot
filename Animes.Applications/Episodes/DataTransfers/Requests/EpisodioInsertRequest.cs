using Animes.Domain.Episodios.Entities;
using Libs.Base.Models.Requests;

namespace Animes.Applications.Episodes.DataTransfers.Requests
{
    public class EpisodioInsertRequest : InsertRequest<Episode>
    {
        public long AnimeId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string ExtraText { get; set; }
    }
}
