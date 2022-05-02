using Libs.Base.Repositories;
using System;

namespace Libs.Base.Configurations
{
    public class DatabaseConfiguration
    {
        protected Type Manipulation { get; set; }
        protected Type Transaction { get; set; }
        protected Type Query { get; set; }

        public void AddManipulation<T>()
            where T : IManipulationRepository
        {
            Manipulation = typeof(T);
        }
        public void AddTransaction<T>()
           where T : ITransactionRepository
        {
            Transaction = typeof(T);
        }
        public void AddQuery<T>()
           where T : IQueryRepository
        {
            Query = typeof(T);
        }
    }
}
