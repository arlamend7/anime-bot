using Animes.Applications.Animes.DataTransfers;
using Animes.Domain.Animes.Entities;
using Animes.Domain.Animes.Mappigns;
using FluentNHibernate.Cfg.Db;
using LIb.Discord;
using Libs.Base;
using Libs.Nhibernate;
using Microsoft.Extensions.DependencyInjection;

namespace Animes.IOC
{
    public static class NativeBootstrapInjector
    {
        public static void AddInternalDependencies(this IServiceCollection services)
        {
            services.ConfigureNhibernate<AnimesMap, Anime>(SQLiteConfiguration.Standard
<<<<<<< HEAD
                                                           .UsingFile(@"D:\Freelances\Bot\anime-bot\database.sqlite")
=======
                                                           .UsingFile(@"C:\ProjetosEstudos\NhibernateImplementacao\bancosauros.sqlite")
>>>>>>> fe2ad086a689d7ac2ab2b5f540bc422ed2cff76d
                                                           .ShowSql()
                                                           .FormatSql());
            services.InjectBot();
            services.ConfigureLib()
                    .AddAutoMapper<AnimeInsertRequest>()
                    .AddAssembly<AnimeInsertRequest>()
                    .AddAssembly<Anime>()
                    .Database(x =>
                    {
                        x.AddManipulation<NhiberanteRespositorio>();
                        x.AddQuery<NhiberanteRespositorio>();
                        x.AddTransaction<NhibernateTransaction>();
                    })
                    .Build();
        }
    }
}
