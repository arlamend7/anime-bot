using NhibernateImplementacao.Products;

namespace NhibernateImplementacao.Categories
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Product> Products { get; set; }

    }
}
