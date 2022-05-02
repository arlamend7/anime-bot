using Libs.Base.Extensions;
using Libs.Base.Injectors.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Libs.Base.Injectors
{
    public class Injector<T> : IInjector<T>
    {
        private readonly IServiceProvider _provider;
        private readonly IServiceCollection _services;

        public Injector(IServiceProvider provider, IServiceCollection services)
        {
            _provider = provider;
            _services = services;
        }
        public T Get()
        {
            ServiceDescriptor type = _services.FirstOrDefault(Implements);
            return type == null ? default : (T)_provider.GetService(type.ServiceType);
        }
        public IEnumerable<T> GetAll()
        {
            return _services
                 .Where(Implements)
                 .Select(x => (T)_provider.GetService(x.ServiceType));
        }
        public Func<ServiceDescriptor, bool> Implements => type => !type.ServiceType.IsAbstract && !type.ServiceType.IsInterface && type.ServiceType.ImplementsOrDerives(typeof(T));
    }
}
