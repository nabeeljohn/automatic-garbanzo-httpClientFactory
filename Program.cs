using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientFactory_Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await RunMe();
        }

        static async Task RunMe()
        {
            string myURL = $"http://dataservice.accuweather.com/forecasts/v1/daily/1day/12065?apikey=RUuBWKtaUKRJC4GtWkjGDuhStOZblr78";

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var services = serviceCollection.BuildServiceProvider();
            var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();

            //Example 1: Basic Use
            
            var httpClient = httpClientFactory.CreateClient();
            var responseMessage = await httpClient.GetAsync(myURL);
            

            //Example 2:
            var httpClientWeather = httpClientFactory.CreateClient("weather");
            var responseMessage2 = await httpClientWeather.GetAsync("");
            var conStream = await responseMessage2.Content.ReadAsStringAsync();
            var contentStream = await responseMessage2.Content.ReadAsStreamAsync();
            
            Console.WriteLine(conStream);

            Console.ReadLine();
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            //Example 1 : Basic Use
            serviceCollection.AddHttpClient();

            //Example 2:
            serviceCollection.AddHttpClient("weather", options =>
            {
                options.BaseAddress = new Uri("http://dataservice.accuweather.com/forecasts/v1/daily/1day/12065?apikey=RUuBWKtaUKRJC4GtWkjGDuhStOZblr78");
                options.DefaultRequestHeaders.Add(HeaderNames.Accept, "*/*");
                //options.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", "RUuBWKtaUKRJC4GtWkjGDuhStOZblr78");
            });
        }
        

    }
}
