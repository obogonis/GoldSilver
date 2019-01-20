using GoldSilver.WebUI.Infrastructure.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GoldSilver.WebUI.Infrastructure.Concrete
{
    public class CurrencyExchangeService : ICurrencyExangeService
    {
        const string CURRENCY_EXCHANGE_API = "http://free.currencyconverterapi.com/";

        public async Task<decimal> GetExchangeRate(string from = "USD", string to="UAH")
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri(CURRENCY_EXCHANGE_API);
                HttpResponseMessage response = client.GetAsync($"api/v6/convert?q={from}_{to}&compact=y").Result; // TODO refactor to async/await
                var stringResult = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(stringResult);
                return (decimal)data[$"{from}_{to}"].val.Value;
            }
        }

    }
}