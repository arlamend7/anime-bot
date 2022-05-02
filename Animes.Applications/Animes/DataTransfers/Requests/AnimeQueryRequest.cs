using Animes.Domain.Animes.Entities;
using Libs.Base.Models;

namespace Animes.Applications.Animes.DataTransfers.Requests
{
    public class AnimeQueryRequest : PaginateRequest<Anime>
    {
        public string Name { get; set; }
        public string ExactlyName { get; set; }
    }
}
