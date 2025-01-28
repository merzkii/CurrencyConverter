namespace Application.Services
{
    public interface IExchangeRateService
    {
        Task<decimal> GetExchangeRateAsync(string currencyCode, DateTime date);
       
    }
}
