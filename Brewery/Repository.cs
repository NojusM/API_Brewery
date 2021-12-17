using Brewery.Models;
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

        public decimal AlcoholSumAbv(float min_abv, float max_abv, float abv)
        {

            return (decimal)p_provider.GetAlcoholByAbv(min_abv, max_abv, abv)
                .Where(x => x.abv != null)
                .Sum(b => b.abv);

        }

        public decimal HighestABV(float min_abv, float max_abv, float abv)
        {
            if (p_provider.GetAlcoholByAbv(min_abv, max_abv, abv).Count() != 0)
            {
                return (decimal)p_provider.GetAlcoholByAbv(min_abv, max_abv, abv)
                    .Where(x => x.abv != null)
                    .Max(b => b.abv);
            }
            else
            {
                return 0M;
            }
        }



    }
}
