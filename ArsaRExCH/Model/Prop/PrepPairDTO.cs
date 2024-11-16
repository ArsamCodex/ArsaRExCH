using Newtonsoft.Json;

namespace ArsaRExCH.Model.Prop
{
    public class PrepPairDTO
    {
        [JsonProperty("symbol")]  // Maps "symbol" in JSON to PairName
        public string PairName { get; set; }

        [JsonProperty("price")]   // Maps "price" in JSON to Price
        public string Price { get; set; }
    }
}
