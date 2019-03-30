using GoldSilver.WebUI.Infrastructure.Abstract;
using System;
using System.Configuration;

namespace GoldSilver.WebUI.Models
{
    public class AppSettings : IAppSettings
    {
        public DateTime LastUpdatedExchangeRate { get; set; }
        ICurrencyExangeService exangeService;
        private decimal _cachedExchangeRate { get; set; }

        public AppSettings(ICurrencyExangeService exangeService)
        {
            this.exangeService = exangeService;
            LastUpdatedExchangeRate = DateTime.MinValue;
        }

        public decimal ExchangeRate
        {
            get
            {
                if ((_cachedExchangeRate == 0M) || (LastUpdatedExchangeRate < DateTime.UtcNow.AddHours(-1)))
                {
                    bool result = Decimal.TryParse(ConfigurationManager.AppSettings["ExchangeRate"], out decimal value);

                    if ((result) && (value != 0M))
                    {
                        return value;
                    }

                    var exchangeRate = exangeService.GetExchangeRate().Result;
                    Decimal.TryParse(ConfigurationManager.AppSettings["ExchangeСoefficient"] ?? "1", out var exchangeCoeficient);
                    _cachedExchangeRate = Math.Round(exchangeRate * exchangeCoeficient, 2);

                    LastUpdatedExchangeRate = DateTime.UtcNow;
                }
                return _cachedExchangeRate;
            }
            set
            {
                ConfigurationManager.AppSettings["ExchangeRate"] = value.ToString();
            }
        }
    }
}