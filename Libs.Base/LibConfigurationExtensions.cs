using Libs.Base.Commands;
using Libs.Base.Configurations;
using Libs.Base.Injectors;
using Libs.Base.Injectors.Interfaces;
using Libs.Base.Serivces;
using Libs.Base.Serivces.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Libs.Base
{
    public static class LibConfigurationExtensions
    {
        public static LibConfigure ConfigureLib(this IServiceCollection services)
        {
            services.AddScoped<IManipulationService, ManipulationService>();
            services.AddScoped<IQueryService, QueryService>();
            services.AddScoped(typeof(UpdateCommand<>));
            services.AddScoped(typeof(InsertCommand<>));
            services.AddScoped(typeof(DeleteCommand<>));
            services.AddScoped(typeof(IInjector<>), typeof(Injector<>));
            
            return new LibConfigure(services);
        }
    }
}
