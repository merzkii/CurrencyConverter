using Application.Responses;
using Core;
using Core.Exceptions;
using Infrastructure.Repositories;
using Newtonsoft.Json;

public class NbgApiClient
{
    
    private const string ApiUrl = "https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/ka/json";
    private readonly IExchangeRateRepository _exchangeRateRepository;

    public NbgApiClient(IExchangeRateRepository exchangeRateRepository)
    {
        _exchangeRateRepository = exchangeRateRepository;
    }

    public async Task<IEnumerable<ExchangeRate>> FetchExchangeRatesAsync()
    {

        var exchangeRates = new List<ExchangeRate>();
        using (var client = new HttpClient())
        {
            try
            {
                var response1 = await client.GetAsync(ApiUrl);

                // Check if the response is successful
                if (response1.IsSuccessStatusCode)
                {
                    var responseContent = await response1.Content.ReadAsStringAsync();

                    // Deserialize the JSON response to your chosen type (CurrencyData)
                    var currencies = JsonConvert.DeserializeObject<List<CurrencyData>>(responseContent)!;
                    exchangeRates = currencies.First().Currencies.Select(x => new ExchangeRate() { Currency = x.Code, Rate = x.Rate, Date = x.ValidFromDate.Date }).ToList();
                    //await _exchangeRateRepository.AddRateAsync(exchangeRates);
                }

                else
                {
                    return exchangeRates;
                }
            }
            catch (Exception ex)
            {
                new NotFoundException("No Rates Are Available");
            }
        }

        return exchangeRates;
    }

    private class NbgExchangeRateResponse
    {
        public string Currency { get; set; }
        public decimal Rate { get; set; }
        public string Date { get; set; }
    }
}