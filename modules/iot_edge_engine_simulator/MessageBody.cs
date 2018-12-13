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
        [JsonIgnore]
        public double Temperature { get; set; }
        [JsonProperty("temperature")]
        public string temperature 
        {
            get 
            {
                return Temperature.ToString("F");
            }
        }
        [JsonIgnore]
        public double Current { get; set; }
        [JsonProperty("current")]
        public string current 
        { 
            get
            {
                return Current.ToString("F");
            }
        }
        [JsonIgnore]
        public double Frequency { get; set; }
        [JsonProperty("frequency")]
        public string frequency 
        { 
            get 
            {
                return Frequency.ToString("F");
            }
        }

        [JsonIgnore]
        public double Power { get; set; }
        [JsonProperty("power")]
        public string power 
        { 
            get 
            {
                return Power.ToString("F");
            }
        }
        [JsonIgnore]
        public double Speed { get; set; }
        [JsonProperty("speed")]
        public string speed 
        { 
            get 
            {
                return Speed.ToString("F");
            }
        }
        [JsonIgnore]
        public double Torque { get; set; }
        [JsonProperty("torque")]
        public string torque 
        { 
            get 
            {
                return Torque.ToString("F");
            }
        }

        [JsonIgnore]
        public double Voltage { get; set; }
         [JsonProperty("voltage")]
        public string voltage
        { 
            get 
            {
                return Voltage.ToString("F");
            }
        }

        [JsonProperty("energy")]
        public double Energy { get; set; }

        [JsonIgnore]
        public double CumulativeEnergy { get; set; }
         [JsonProperty("cumulativeenergy")]
        public string cumulativeenergy
        { 
            get 
            {
                return CumulativeEnergy.ToString("F");
            }
        }
    }

    [JsonObject("ambient")]
    public class Ambient
    {
        [JsonIgnore]
        public double Temperature { get; set; }
        
        [JsonProperty("temperature")]
        public string temperature 
        {
            get 
            {
                return Temperature.ToString("F");
            }
        }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }
}
