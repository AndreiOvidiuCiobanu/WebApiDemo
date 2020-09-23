using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.Services
{
    public class WeatherInformation : IWeatherInformation
    {
        private IHttpClientFactory _httpClientFactory;

        public WeatherInformation(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Rootobject> GetWheatherInformation()
        {
            var client = _httpClientFactory.CreateClient("weatherApi");

            try
            {
                Rootobject weather = await client.GetFromJsonAsync<Rootobject>("location/44418/");
                return weather;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw new Exception();
            }
        }
    }
}
