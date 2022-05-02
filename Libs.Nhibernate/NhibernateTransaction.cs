using Libs.Base.Repositories;
using NHibernate;

namespace Libs.Nhibernate
{
    public class NhibernateTransaction : ITransactionRepository
    {
        private ISession session;
        private ITransaction transaction;

        public NhibernateTransaction(ISession session)
        {
            this.session = session;
        }

        /// <summary>
        /// Iniciar uma transação no banco de dados
        /// </summary>
        public void BeginTransaction()
        {
            transaction = session.BeginTransaction();
        }

        /// <summary>
        /// Desfazer as alterações realizadas em uma transação
        /// </summary>
        public void Rollback()
        {
            if (transaction != null && transaction.IsActive)
                transaction.Rollback();
        }

        /// <summary>
        /// Aplicar as alterações realizadas em uma transação
        /// </summary>
        public void Commit()
        {
            if (transaction != null && transaction.IsActive)
                transaction.Commit();
        }

        public void Dispose()
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }

            if (session.IsOpen)
            {
                session.Close();
                session = null;
            }
        }

        public void ClearSession()
        {
            if (session != null)
                session.Clear();
        }

        public void FlushSession()
        {
            if (session != null)
                session.Flush();
        }
    }
}
