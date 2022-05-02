using Animes.Domain.Animes.Entities;
using Libs.Base.Entities;

namespace Animes.Domain.Episodios.Entities
{
    public class Episode : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual int Number { get; set; }
        public virtual string ExtraText { get; set; }
        public virtual Anime Anime { get; protected set; }
        public virtual Enum Duplagem { get; set; }
        public Episode(string name, int number, string extraText)
        {
            Name = name;
            Number = number;
            ExtraText = extraText;
        }
    }
}
