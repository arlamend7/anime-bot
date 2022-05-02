using Libs.Base.Commands;
using Libs.Base.Entities;
using Libs.Base.Entities.Interfaces;
using Libs.Base.Serivces.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Libs.Base.Serivces
{
    public class ManipulationService : IManipulationService
    {
        private readonly IServiceProvider _services;

        public ManipulationService(IServiceProvider services)
        {
            _services = services;
        }

        public InsertCommand<T> Insert<T>()
            where T : EntityBase
        {
            return _services.GetService<InsertCommand<T>>();
        }

        public UpdateCommand<T> Update<T>(T entity)
            where T : EntityBase
        {
            return _services.GetService<UpdateCommand<T>>().SetEntity(entity);
        }
        public DeleteCommand<T> Delete<T>(T entity)
              where T : EntityBase
        {
            return _services.GetService<DeleteCommand<T>>().SetEntity(entity);
        }
    }
}
