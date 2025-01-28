using Newtonsoft.Json;

namespace Application.Responses
{

    public class CurrencyInfo
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("rateFormated")]
        public string RateFormatted { get; set; }

        [JsonProperty("diffFormated")]
        public string DiffFormatted { get; set; }

        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("diff")]
        public double Diff { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("validFromDate")]
        public DateTime ValidFromDate { get; set; }
    }

    public class CurrencyData
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("currencies")]
        public List<CurrencyInfo> Currencies { get; set; }
    }
    //public class RootObject
    //{
    //    [JsonProperty("date")]
    //    public DateTime Date { get; set; }

    //    [JsonProperty("currencies")]
    //    public List<CurrencyInfo> Currencies { get; set; }
    //}
}
