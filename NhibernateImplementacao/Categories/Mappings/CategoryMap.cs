using FluentNHibernate.Mapping;

namespace NhibernateImplementacao.Categories
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);

            HasMany(x => x.Products)
                .KeyColumn("CategoryId");

        }
    }
}
