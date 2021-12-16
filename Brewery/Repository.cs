using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewery
{
    public class BreweryRepository
    {
        private readonly IAlcoholProvider p_provider;
        public BreweryRepository(IAlcoholProvider provider)
        {
            p_provider = provider;
        }

        /*public decimal AlcoholSumAbv(float min_abv, float max_abv, float abv)
        {
            return p_provider.GetAlcoholByAbv(min_abv, max_abv, abv)
                .Sum(b => b.abv);
        }
        */

    }
}
