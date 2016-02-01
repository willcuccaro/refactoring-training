using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    [Serializable]
    public class Product
    {
        [JsonProperty("Name")]
        public string ProductName;
        [JsonProperty("Price")]
        public double RegularPrice;
        [JsonProperty("Quantity")]
        public int RemainingQuantity;
    }
}
