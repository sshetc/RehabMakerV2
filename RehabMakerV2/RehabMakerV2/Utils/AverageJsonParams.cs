using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RehabMakerV2.Utils
{
    class AverageJsonParams
    {
        [JsonProperty("IdStatitics")]
        public decimal IdStatitics { get; set; }

        [JsonProperty("AverageSpeed")]
        public string AverageSpeed { get; set; }

        [JsonProperty("TotalDistance")]
        public string TotalDistance { get; set; }

        [JsonProperty("TotalCalories")]
        public string TotalCalories { get; set; }

        [JsonProperty("IdDevice")]
        public int IdDevice { get; set; }

        [JsonProperty("idDeviceNavigation")]
        public object IdDeviceNavigation { get; set; }
    }
}
