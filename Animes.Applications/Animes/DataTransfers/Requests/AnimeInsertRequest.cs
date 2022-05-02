using Animes.Domain.Animes.Entities;
using Libs.Base.Models.Requests;

namespace Animes.Applications.Animes.DataTransfers
{
    public class AnimeInsertRequest : InsertRequest<Anime>
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
    }
}
