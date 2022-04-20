using FluentNHibernate.Mapping;

namespace NhibernateImplementacao.Products.Mappings
{
    public class ProductsMap : ClassMap<Product>
    {
        public ProductsMap() 
        {
            Table("Products");
            Id(x => x.Id).Column("ProductId");
            Map(x => x.Name).Column("Name");
            Map(x => x.Price).Column("Price");
            Map(x => x.Description).Column("Description");            
            References(x => x.Category).Column("CategoryId");
        }
    }
}
