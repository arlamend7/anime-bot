using Libs.Base.Extensions;
using Libs.Base.Repositories;
using Libs.Base.Rules;
using Libs.Base.Serivces.Interfaces;
using Libs.Base.Triggers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Libs.Base.Configurations
{
    public class LibConfigure : DatabaseConfiguration
    {
        private readonly IServiceCollection _services;
        private readonly IEnumerable<Type> SearchTypes = new List<Type>()
        {
            typeof(IApplicationService<>),
            typeof(Rule<>),
            typeof(Trigger<>)
        };

        public LibConfigure(IServiceCollection services)
        {
            _services = services;
        }

        /// <summary>
        ///     Get all Applications, Rule and Trigger from assembly
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public LibConfigure AddAssembly<T>()
        {
            var types =
            typeof(T).Assembly.ExportedTypes
                       .Where(type => !type.IsAbstract && !type.IsInterface && SearchTypes.Any(x => type.ImplementsOrDerives(x)))
                       .ToList();
            types.ForEach(type => _services.AddScoped(type));
            return this;
        }
        public LibConfigure AddAutoMapper<T>()
        {
            _services.AddAutoMapper(typeof(T).Assembly);
            return this;
        }

        public LibConfigure Database(Action<DatabaseConfiguration> configure)
        {
            configure(this);
            _services.AddScoped(typeof(IManipulationRepository),Manipulation);
            _services.AddScoped(typeof(IQueryRepository), Query);
            _services.AddScoped(typeof(ITransactionRepository), Transaction);
            return this;
        }

        public void Build()
        {
            _services.AddScoped(x => _services);
        }
    }
}
