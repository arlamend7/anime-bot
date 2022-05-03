using Animes.Domain.Animes.Entities;
using Libs.Base.Entities;

namespace Animes.Domain.Episodios.Entities
{
    public class Episode : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual string Number { get; set; }
        public virtual string ExtraText { get; set; }
        public virtual Anime Anime { get; protected set; }
        public virtual Enum Duplagem { get; set; }

        public Episode()
        {
            
        }
        public Episode(Anime anime,string name, string number, string extraText)
        {
            Anime = anime;
            Name = name;
            Number = number;
            ExtraText = extraText;
        }
    }
}
