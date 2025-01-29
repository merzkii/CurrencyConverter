using Core;

namespace Application.Services
{
    public interface IConversionService
    {
        Task<decimal> ConvertCurrencyAsync(
             string clientName,
             string personalNumber,
             string originCurrency,
             string destinationCurrency,
             decimal amount,
             DateTime date
        );
    }
}
