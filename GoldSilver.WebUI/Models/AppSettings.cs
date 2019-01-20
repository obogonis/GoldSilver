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
                bool result = Decimal.TryParse(ConfigurationManager.AppSettings["ExchangeRate"], out decimal value);

                if ((result) || (value == 0M))
                {
                    if ((_cachedExchangeRate == 0M) && (LastUpdatedExchangeRate < DateTime.UtcNow.AddHours(-1)))
                    {
                        _cachedExchangeRate = Math.Round(exangeService.GetExchangeRate().Result, 0);
                        LastUpdatedExchangeRate = DateTime.UtcNow;
                    }
                    return _cachedExchangeRate;
                }

                return value;
            }
            set
            {
                ConfigurationManager.AppSettings["ExchangeRate"] = value.ToString();
            }
        }
    }
}