using Animes.Applications.Animes.DataTransfers.Responses;
using Animes.Domain.Animes.Entities;
using AutoMapper;
using Libs.Base.Models;

namespace Animes.Applications.Animes.Profiles
{
    public class AnimesProfile : Profile
    {
        public AnimesProfile()
        {
            CreateMap(typeof(PaginatedResponse<>), typeof(PaginatedResponse<>));
            CreateMap<Anime, AnimeResponse>();
        }
    }
}
