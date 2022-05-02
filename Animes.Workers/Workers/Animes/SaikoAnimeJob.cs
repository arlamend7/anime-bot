using Animes.Applications.Animes.DataTransfers;
using Animes.Applications.Animes.DataTransfers.Requests;
using Animes.Applications.Animes.DataTransfers.Responses;
using Animes.Applications.Episodes.DataTransfers.Requests;
using Animes.Applications.Episodes.DataTransfers.Responses;
using Animes.Domain.Animes.Entities;
using Animes.Domain.Episodios.Entities;
using Libs.Base.Injectors.Interfaces;
using Libs.Base.Models;
using Libs.Base.Serivces.Interfaces;
using OpenQA.Selenium;
using Quartz;
using Serilog.Context;

namespace CDL.Integration.Workers.Workers.Animes
{
    public class SaikoAnimeJob : IJob
    {
        private readonly ILogger<SaikoAnimeJob> logger;
        private readonly IWebDriver _webDriver;
        private readonly IApplicationService<Anime> _applicationService;
        private readonly IApplicationService<Episode> _episodesAppService;
        public SaikoAnimeJob(ILogger<SaikoAnimeJob> logger,
                             IWebDriver webDriver,
                             IInjector<IApplicationService<Anime>> animesAppService,
                             IInjector<IApplicationService<Episode>> episodesAppService)
        {
            this.logger = logger;
            _webDriver = webDriver;
            _applicationService = animesAppService.Get();
            _episodesAppService = episodesAppService.Get();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (LogContext.PushProperty("TransactionId", context.FireInstanceId))
            using (LogContext.PushProperty("Job", context.FireInstanceId))
            {
                try
                {
                    Execute();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "CustosServicosJob", "Exception");
                }
            }
            await Task.CompletedTask;
        }

        public void Execute()
        {
            int currentPage = 1;
            bool allRecovered = false;
            List<AnimeInsertRequest> animeList = new List<AnimeInsertRequest>();

            _webDriver.Url = "https://saikoanimes.net/";
            
            while (!allRecovered)
            {
                _webDriver.Url = $"https://saikoanimes.net/?fwp_paged={currentPage}";
                
                IEnumerable<IWebElement> animeElements = _webDriver.FindElements(By.ClassName("view-first"));

                allRecovered = animeElements.Select(element => (anime: GetOrCreateAnime(element), element))
                                            .All(x => CreateEpisode(x.anime, x.element));        
            }
        }

        public AnimeResponse GetOrCreateAnime(IWebElement animeElement)
        {
            AnimeInsertRequest animeRequest = new()
            {
                ImageUrl = animeElement.FindElement(By.TagName("img")).GetAttribute("src"),
                Link = animeElement.FindElement(By.TagName("a")).GetAttribute("href"),
                Name = animeElement.FindElement(By.ClassName("post-name")).Text,
            };

            PaginatedResponse<AnimeResponse> anime = _applicationService.Get<AnimeResponse>(new AnimeQueryRequest() { ExactlyName = animeRequest.Name, Qt = 1 });
            AnimeResponse animeResponse = anime.Registros.FirstOrDefault();
            if (animeResponse is null)
            {
                animeResponse = _applicationService.Insert<AnimeResponse>(animeRequest);
            }
            return animeResponse;
        }
        public bool CreateEpisode(AnimeResponse animeResponse, IWebElement animeElement)
        {
            string epsodioText = animeElement.FindElement(By.ClassName("post-ep")).Text[10..].Trim();
            string[] episodeInfo = epsodioText.Split(' ');
            EpisodioInsertRequest episodioInsertRequest = new EpisodioInsertRequest
            {
                AnimeId = animeResponse.Id,
                Name = epsodioText,
                Number = int.Parse(episodeInfo[0]),
                ExtraText = episodeInfo.Last() != episodeInfo[0] ? episodeInfo.Last() : null,
            };

            PaginatedResponse<EpisodeResponse> episodio = _episodesAppService.Get<EpisodeResponse>(new EpisodeQueryRequest() { AnimeId = animeResponse.Id, Number = episodioInsertRequest.Number, Qt = 1 });
            if(episodio is null)
            {
                _episodesAppService.Insert<EpisodeResponse>(episodioInsertRequest);
                return true;
            }
            return false;
        }
    }
}
