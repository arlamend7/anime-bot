using Animes.Applications.Episodes.DataTransfers.Responses;
using Animes.Domain.Episodios.Entities;
using AutoMapper;

namespace Animes.Applications.Episodes.Profiles
{
    public class EpisodesProfile : Profile
    {
        public EpisodesProfile()
        {
            CreateMap<Episode, EpisodeResponse>();
        }
    }
}
