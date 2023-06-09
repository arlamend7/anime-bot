using Libs.Base.Entities;
using Libs.Base.Repositories;
using NHibernate;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Libs.Nhibernate
{
    public class NhiberanteRespositorio : IQueryRepository, IManipulationRepository
    {
        private readonly ISession _session;
        public NhiberanteRespositorio(ISession session)
        {
            _session = session;
        }
        public IQueryable<T> Query<T>() where T : EntityBase
        {
            return _session.Query<T>();
        }
        public T Get<T>(long id) where T : EntityBase
        {
            return _session.Get<T>(id);
        }
        public virtual object Save<T>(T entity) where T : EntityBase
        {
            return _session.Save(entity);
        }
        public virtual void Edit<T>(T entity) where T : EntityBase
        {
            _session.Update(entity);
        }
        public void Refresh<T>(T entity) where T : EntityBase
        {
            _session.Refresh(entity);
        }
        public virtual void Delete<T>(T entity) where T : EntityBase
        {
            _session.Delete(entity);
        }
        public object GetAll<T>() where T : EntityBase
        {
            return Query<T>();
        }
    }
}
