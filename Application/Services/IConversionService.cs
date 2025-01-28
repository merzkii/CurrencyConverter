namespace Application.Services
{
    public interface IConversionService
    {
        Task<decimal> ConvertCurrencyAsync(
        string originCurrency,
        string destinationCurrency,
        decimal amount,
        DateTime date);
    }
}
