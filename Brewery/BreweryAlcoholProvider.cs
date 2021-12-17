using Brewery.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Serialization.Json;
using Newtonsoft.Json;

namespace Brewery
{
    public class BreweryAlcoholProvider : IAlcoholProvider
    {
        private IRestClient p_client;

        public BreweryAlcoholProvider(IRestClient restClient)
        {
            p_client = restClient;
        }
        public BreweryAlcoholProvider()
        {
            //api client
            p_client = new RestClient($"https://api.punkapi.com/v2/beers");
            p_client.Timeout = -1;
        }

        //Does a query to API dependent on given variables
        public IEnumerable<Alcohol> GetAlcoholByAbv(float min_abv, float max_abv, float abv)
        {
            if(abv == 0.0f) //Checks if user uses a range (2.5|5) or a single value (5)
            {
                if (min_abv < 0 || max_abv < 0) return null;
                //Appends a query to api request
                var request = new RestRequest($"?abv_gt={min_abv}&abv_lt={max_abv}&per_page=80", Method.GET);
                IRestResponse response = p_client.Execute(request);
                //Deserializes data
                return JsonConvert.DeserializeObject<IEnumerable<Alcohol>>(response.Content);
            }
            else
            {
                if (abv < 0) return null;
                min_abv = abv - 0.1F;
                max_abv = abv + 0.1F;
                var request = new RestRequest($"?abv_gt={min_abv}&abv_lt={max_abv}&per_page=80", Method.GET);
                IRestResponse response = p_client.Execute(request);
                return JsonConvert.DeserializeObject<IEnumerable<Alcohol>>(response.Content);
            }
        }

        public IEnumerable<Alcohol> GetAlcoholByDate(string brewed_before, string brewed_after)
        {
            var request = new RestRequest($"?brewed_before={brewed_before}&brewed_after={brewed_after}&per_page=80", Method.GET);
            IRestResponse response = p_client.Execute(request);
            return JsonConvert.DeserializeObject<IEnumerable<Alcohol>>(response.Content);
        }

        public IEnumerable<Alcohol> GetAlcoholByName(string name)
        {
            var request = new RestRequest($"?beer_name={name}&per_page=80", Method.GET);
            IRestResponse response = p_client.Execute(request);
            return JsonConvert.DeserializeObject<IEnumerable<Alcohol>>(response.Content);
        }

        public IEnumerable<Alcohol> GetAlcoholByAbvAndName(string name, float min_abv, float max_abv, float abv)
        {
            if (abv == 0.0f)
            {
                if (min_abv < 0 || max_abv < 0) return null;
                var request = new RestRequest($"?abv_gt={min_abv}&abv_lt={max_abv}&beer_name={name}&per_page=80", Method.GET);
                IRestResponse response = p_client.Execute(request);
                return JsonConvert.DeserializeObject<IEnumerable<Alcohol>>(response.Content);
            }
            else
            {
                if (abv < 0) return null;
                min_abv = abv - 0.1F;
                max_abv = abv + 0.1F;
                var request = new RestRequest($"?abv_gt={min_abv}&abv_lt={max_abv}&beer_name={name}&per_page=80", Method.GET);
                IRestResponse response = p_client.Execute(request);
                return JsonConvert.DeserializeObject<IEnumerable<Alcohol>>(response.Content);
            }
        }

        public IEnumerable<Alcohol> GetAlcoholByAbvAndDate(string brewed_before, string brewed_after, float min_abv, float max_abv, float abv)
        {
            if (abv == 0.0f)
            {
                if (min_abv < 0 || max_abv < 0) return null;
                var request = new RestRequest($"?abv_gt={min_abv}&abv_lt={max_abv}&brewed_before={brewed_before}&brewed_after={brewed_after}&per_page=80", Method.GET);
                IRestResponse response = p_client.Execute(request);
                return JsonConvert.DeserializeObject<IEnumerable<Alcohol>>(response.Content);
            }
            else
            {
                if (abv < 0) return null;
                min_abv = abv - 0.1F;
                max_abv = abv + 0.1F;
                var request = new RestRequest($"?abv_gt={min_abv}&abv_lt={max_abv}&brewed_before={brewed_before}&brewed_after={brewed_after}&per_page=80", Method.GET);
                IRestResponse response = p_client.Execute(request);
                return JsonConvert.DeserializeObject<IEnumerable<Alcohol>>(response.Content);
            }
        }

        public IEnumerable<Alcohol> GetAlcoholByDateAndName(string name, string brewed_before, string brewed_after)
        {
            var request = new RestRequest($"?beer_name={name}&brewed_before={brewed_before}&brewed_after={brewed_after}&per_page=80", Method.GET);
            IRestResponse response = p_client.Execute(request);
            return JsonConvert.DeserializeObject<IEnumerable<Alcohol>>(response.Content);
        }
        public IEnumerable<Alcohol> GetAlcoholByAll(string name, float min_abv, float max_abv, float abv, string brewed_before, string brewed_after)
        {
            if (abv == 0.0f)
            {
                if (min_abv < 0 || max_abv < 0) return null;
                var request = new RestRequest($"?abv_gt={min_abv}&abv_lt={max_abv}&brewed_before={brewed_before}&brewed_after={brewed_after}&beer_name={name}&per_page=80", Method.GET);
                IRestResponse response = p_client.Execute(request);
                return JsonConvert.DeserializeObject<IEnumerable<Alcohol>>(response.Content);
            }
            else
            {
                if (abv < 0) return null;
                min_abv = abv - 0.1F;
                max_abv = abv + 0.1F;
                var request = new RestRequest($"?abv_gt={min_abv}&abv_lt={max_abv}&brewed_before={brewed_before}&brewed_after={brewed_after}&beer_name={name}&per_page=80", Method.GET);
                IRestResponse response = p_client.Execute(request);
                return JsonConvert.DeserializeObject<IEnumerable<Alcohol>>(response.Content);
            }
        }
    }
}
