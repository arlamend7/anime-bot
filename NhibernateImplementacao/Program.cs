using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NhibernateImplementacao.Categories;
using NhibernateImplementacao.Categories.Services;
using NhibernateImplementacao.Products;
using NhibernateImplementacao.Products.Mappings;
using Selenium;

namespace NhibernateImplementacao
{
    class Program
    {

        static void Main(string[] args)
        {
            Config.Execute();





            
            #region Criando banco de dados
            //    var sessionFactory = Configuration();
            //    var session = sessionFactory.OpenSession();

            //    var category = new Category()
            //    {
            //        Name = "Category 1",
            //    };

            //    var product = new Product()
            //    {
            //        Name = "Product 1",
            //        Description = "Product 1 description",
            //        Price = 10.00m
            //    };


            //    new ProductService(session).Insert(product);

            //    using (var transaction = session.BeginTransaction())
            //    {
            //        var idCategory = session.Save(category);

            //        category.Id = (int)idCategory;
            //        product.Category = category;
            //        var idProduct = session.Save(product);
            //        transaction.Commit();

            //    }
            //    session.Close();

            //}

            //public static ISessionFactory Configuration()
            //{
            //    var path = "c:/projetosestudos/NhibernateImplementacao/bancosauros.sqlite";
            //    return Fluently.Configure().Database(PersistenceConfiguration(path)).Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProductsMap>()).ExposeConfiguration(cfg =>
            //    {
            //        new SchemaUpdate(cfg).Execute(true, true); // cria as tabela
            //    }).BuildConfiguration().BuildSessionFactory();
            //}

            //// exibir todos os sql's
            //public static IPersistenceConfigurer PersistenceConfiguration(string conn)
            //{
            //    return SQLiteConfiguration.Standard.UsingFile(conn).ShowSql().FormatSql();
            //}
            #endregion
        }
    }
}