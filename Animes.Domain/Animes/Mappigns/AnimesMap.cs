using Animes.Domain.Animes.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Domain.Animes.Mappigns
{
    public class AnimesMap : ClassMap<Anime>
    {
        public AnimesMap()
        {
            Schema("animes");
            Table("anime");
            Id(x => x.Id).Column("Animeid");
            Map(x => x.Name);
            Map(x => x.ImageUrl);
            Map(x => x.Link);

        }
    }
}
