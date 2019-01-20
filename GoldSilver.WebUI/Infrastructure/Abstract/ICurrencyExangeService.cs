using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldSilver.WebUI.Infrastructure.Abstract
{
    public interface ICurrencyExangeService
    {
        Task<decimal> GetExchangeRate(string from = "USD", string to = "UAH");
    }
}
