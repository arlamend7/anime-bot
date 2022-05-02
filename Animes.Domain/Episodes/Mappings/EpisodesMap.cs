using Animes.Domain.Episodios.Entities;
using FluentNHibernate.Mapping;

namespace Animes.Domain.Episodes.Mappings
{
    public class EpisodesMap : ClassMap<Episode>
    {
        public EpisodesMap()
        {
            Schema("animes");
            Table("episodes");
            Id(x => x.Id).Column("EpisodeId");
            Map(x => x.Name);
            Map(x => x.ExtraText);
            Map(x => x.Number);
            References(x => x.Anime).Column("AnimeId");
        }
    }
}
