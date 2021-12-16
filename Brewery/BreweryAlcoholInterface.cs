using Brewery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewery
{
    public interface IAlcoholProvider
    {
        //Does a query to API dependent on given variables
        IEnumerable<Alcohol> GetAlcoholByName(string name);

        IEnumerable<Alcohol> GetAlcoholByAbv(float min_abv, float max_abv, float abv);

        IEnumerable<Alcohol> GetAlcoholByDate(string brewed_before, string brewed_after);

        IEnumerable<Alcohol> GetAlcoholByAbvAndName(string name, float min_abv, float max_abv, float abv);

        IEnumerable<Alcohol> GetAlcoholByAbvAndDate(string brewed_before, string brewed_after, float min_abv, float max_abv, float abv);

        IEnumerable<Alcohol> GetAlcoholByDateAndName(string name, string brewed_before, string brewed_after);

        IEnumerable<Alcohol> GetAlcoholByAll(string name, float min_abv, float max_abv, float abv, string brewed_before, string brewed_after);


    }
}
