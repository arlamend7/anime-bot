using Libs.Base.Entities;

namespace Animes.Domain.Animes.Entities
{
    public class Anime : EntityBase
    {
        public virtual string Link { get; set; }
        public virtual string Name { get; set; }
        public virtual string ImageUrl { get; set; }

        public Anime()
        {
            
        }
        public Anime(string name, string imageUrl, string link)
        {
            Name = name;
            ImageUrl = imageUrl;
            Link = link;
        }
    }
}
