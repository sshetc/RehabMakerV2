using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RehabMakerV2.Utils
{
    public partial class JsonParams
    {
        [JsonProperty("idParams")]
        public decimal IdParams { get; set; }

        [JsonProperty("speed")]
        public decimal Speed { get; set; }

        [JsonProperty("distance")]
        public decimal Distance { get; set; }

        [JsonProperty("сalories")]
        public decimal Сalories { get; set; }

        [JsonProperty("idDevice")]
        public int IdDevice { get; set; }

        [JsonProperty("idDeviceNavigation")]
        public object IdDeviceNavigation { get; set; }
    }
}
