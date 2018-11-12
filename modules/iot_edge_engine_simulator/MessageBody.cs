// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Newtonsoft.Json;

namespace AzureIotEdgeSimulatedTemperatureSensor
{
    public class MessageBody
    {
        [JsonProperty("motor")]
        public Motor Motor { get; set; }
        [JsonProperty("ambient")]
        public Ambient Ambient { get; set; }
        [JsonProperty("timeCreated")]
        public string TimeCreated { get; set; }
    }

    [JsonObject("motor")]
    public class Motor
    {
        [JsonProperty("temperature")]
        public double Temperature { get; set; }
        [JsonProperty("current")]
        public double Current { get; set; }
        [JsonProperty("frequency")]
        public double Frequency { get; set; }
    }

    [JsonObject("ambient")]
    public class Ambient
    {
        [JsonProperty("temperature")]
        public double Temperature { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }
}
