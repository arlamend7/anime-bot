using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium
{
    public class Config
    {
        static IWebDriver driver = new ChromeDriver(@"C:\ProjetosEstudos\NhibernateImplementacao\Selenium\Drivers\");

        public class Anime
        {
            public string Name { get; set; }
            public string ImageUrl { get; set; }
            public string Link { get; set; }

            public List<string> Episodios { get; set; }

        }
        public static void Execute()
        {
            int pages = 6;
            int currentPage = 1;
            bool allRecovered = false;

            driver.Url = "https://saikoanimes.net/";


            List<Anime> animeList = new List<Anime>();

            //for (int i = 1; i < pages; i++)
            //{

            //    IEnumerable<IWebElement> divs = driver.FindElements(By.ClassName("view-first"));

            //    animeList.AddRange(divs.Select(x => new Anime
            //    {
            //        ImageUrl = x.FindElement(By.TagName("img")).GetAttribute("src"),
            //        Link = x.FindElement(By.TagName("a")).GetAttribute("href"),
            //        Name = x.FindElement(By.ClassName("post-name")).Text,
            //        Episodios = new List<string>() { x.FindElement(By.ClassName("post-ep")).Text.Substring(10) }
            //    }));

            //}
            
            

            while (!allRecovered)
            {
                var animes = RecuperarAnimesPagina(currentPage);
                currentPage++;
                allRecovered = animes.Count() == 0 || animeList.Any(x => x.Episodios.Any(x => animes.SelectMany(x => x.Episodios).Contains(x))) ;
                animeList.AddRange(animes);

            }




            foreach (var item in animeList)
            {
                Console.WriteLine($"{item.Name} - {item.Link} - {item.Episodios.FirstOrDefault()}");
            }
       

            
        

            

        }
        
        public static IEnumerable<Anime> RecuperarAnimesPagina(int page)
        {
            driver.Url = String.Format("https://saikoanimes.net/?fwp_paged={0}", page);

            IEnumerable<IWebElement> divs = driver.FindElements(By.ClassName("view-first"));

          return divs.Select(x => new Anime
            {
                ImageUrl = x.FindElement(By.TagName("img")).GetAttribute("src"),
                Link = x.FindElement(By.TagName("a")).GetAttribute("href"),
                Name = x.FindElement(By.ClassName("post-name")).Text,
                Episodios = new List<string>() { x.FindElement(By.ClassName("post-ep")).Text.Substring(10) }
            });

           
        }


    }


}

