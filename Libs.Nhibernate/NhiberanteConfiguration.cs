using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Microsoft.Extensions.DependencyInjection;
using FluentNHibernate.Mapping;
using Libs.Base;
using Libs.Base.Repositories;

namespace Libs.Nhibernate
{
    public static class NhibernateConfiguration
    {
        private static ISessionFactory CreateSession<T>(IPersistenceConfigurer configurer)
        {
            return Fluently.Configure()
                .Database(configurer)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<T>())
                .ExposeConfiguration(cfg =>
                {
                    //new SchemaExport(cfg).Create(true,true);
                    new SchemaUpdate(cfg).Execute(true, true); // CRIA AS TABELA TUDO
                })
                .BuildConfiguration()
                .BuildSessionFactory();
        }
        public static void ConfigureNhibernate<T, TEntity>(this IServiceCollection services, IPersistenceConfigurer persistenceConfiguration)
            where T : ClassMap<TEntity>
        {
            services.AddSingleton(factory => CreateSession<T>(persistenceConfiguration));
            services.AddScoped(factory => factory.GetService<ISessionFactory>().OpenSession());
        }
    }
}
