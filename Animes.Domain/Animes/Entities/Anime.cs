using Libs.Base.Entities;

namespace Animes.Domain.Animes.Entities
{
    public class Anime : EntityBase
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Link { get; set; }
        public Anime(string name, string imageUrl, string link)
        {
            Name = name;
            ImageUrl = imageUrl;
            Link = link;
        }
    }
}
