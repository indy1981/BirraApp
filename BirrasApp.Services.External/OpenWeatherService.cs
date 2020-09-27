using BirrasApp.External.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BirrasApp.External.Services
{
    public class OpenWeatherService : IWeatherService
    {
        private const string BaseUrl = "https://community-open-weather-map.p.rapidapi.com/forecast/daily?units=metric&q=London,uk";

        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public OpenWeatherService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<float?> GetWeatherByDay(DateTimeOffset date)
        {
            try
            {
                if (date > DateTimeOffset.Now.AddDays(7) || date.Date < DateTimeOffset.Now.Date)
                {
                    // Open Wheater solo da el pronostico para 7 dias y solo a partir del presente
                    return null;
                }

                _client.DefaultRequestHeaders.Add("x-rapidapi-host", _configuration["x-rapidapi-host"]);
                _client.DefaultRequestHeaders.Add("x-rapidapi-key", _configuration["x-rapidapi-key"]);
                _client.DefaultRequestHeaders.Add("x-rapidapi-host", _configuration["x-rapidapi-host"]);

                var wheaterResponse = await _client.GetAsync(_configuration["wheatherUrl"]);
                if (!wheaterResponse.IsSuccessStatusCode)
                {
                    // agregar logs 
                    return null;
                }

                var contentWheaterResponse = await wheaterResponse.Content.ReadAsStringAsync();
                var weatherParsedReponse = JObject.Parse(contentWheaterResponse.ToString());
                var indexArrayDay = (date - DateTimeOffset.Now).Days;

                // muy mejorable
                return float.Parse(weatherParsedReponse["list"][indexArrayDay]["temp"]["day"].ToString());
            }
            catch (Exception)
            {
                // agregar logs 
                return null;
            }
        }
    }

}
