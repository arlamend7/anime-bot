using NHibernate;
using NhibernateImplementacao.Products;

namespace NhibernateImplementacao.Categories.Services
{
    public class ProductService
    {
        private readonly ISession session;

        public ProductService(ISession session )
        {
            this.session = session;
        }
        
        public Product Insert(Product product)
        {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(product);
                    transaction.Commit();
                }        
            return product;
        } 
    }
}
