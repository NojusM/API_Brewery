using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brewery;

namespace BreweryConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TEMPORARY FOR TESTING!!!!!!!!!!!!

            var provider = new BreweryAlcoholProvider() ;

            //var results = provider.GetAlcoholByName("Buzz").First().image_url;
            //Console.WriteLine(results);
            var results = provider.GetAlcoholByAbv(0,5,0.0F);

            decimal a = 0;
            foreach(var item in results)
            {
                Console.WriteLine(item.abv);
                if (a < item.abv) a = (decimal) item.abv;
            }

            Console.WriteLine("Biggest manual: " + a);

            //var results = provider.GetAlcoholByAbv(0, 0, 4.8F);

            var repository = new BreweryRepository(provider);
            
            //Console.WriteLine(repository.AlcoholSumAbv(0, 5, 0.0F));
            Console.WriteLine("Biggest " + repository.HighestABV(0, 5, 0.0F));

            //Console.WriteLine(results.ElementAt(0).name);

            //labelname.Text = data.ElementAt(0).name;
            Console.ReadLine();
        }
    }
}
