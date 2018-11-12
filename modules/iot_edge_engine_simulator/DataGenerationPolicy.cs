// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace AzureIotEdgeSimulatedTemperatureSensor
{
    public class DataGenerationPolicy
    {
        private static readonly Random rnd = new Random();
        private double _normal;

        public DataGenerationPolicy()
        {
            MotorTemperatureMin = 21;
            MotorTemperatureMax = 100;
            MotorFrequencyMin = 30;
            MotorFrequencyMax = 60;
            MotoreCurrentMin = 1;
            MotorCurrentMax = 10;
            AmbientTemperature = 21;
            HumidityPercentMin = 24;
            HumidityPercentMax = 27;
            _normal = (MotorCurrentMax - MotoreCurrentMin) / (MotorTemperatureMax - MotorTemperatureMin);
        }

        public double MotorTemperatureMin { get; private set; }
        public double MotorTemperatureMax { get; private set; }
        public double MotoreCurrentMin { get; private set; }
        public double MotorCurrentMax { get; private set; }
        public double MotorFrequencyMin { get; private set; }
        public double MotorFrequencyMax { get; private set; }
        public double AmbientTemperature { get; private set; }
        public int HumidityPercentMin { get; private set; }
        public int HumidityPercentMax { get; set; }

        public double CalculateMotorTemperature(double? currentTemperature = null)
        {
            var current = currentTemperature ?? MotorTemperatureMin;
            if (current > MotorTemperatureMax)
            {
                current += rnd.NextDouble() - 0.5; // add value between [-0.5..0.5]
            }
            else
            {
                current += -0.25 + (rnd.NextDouble() * 1.5); // add value between [-0.25..1.25] - avg +0.5
            }
            return current;
        }

        public double CalculateCurrent(double currentTemperature)
        {
            return MotoreCurrentMin + ((currentTemperature - MotorTemperatureMin) * _normal);
        }

        public double CalculateMotorFrequency(double? currentFrequency = null)
        {
            var current = currentFrequency ?? MotorFrequencyMin;
            if(current > MotorFrequencyMax)
            {
                current += rnd.NextDouble() - 0.5; // add value between [-0.5..0.5]
            }
            else
            {
                current += -0.25 + (rnd.NextDouble() * 1.5); // add value between [-0.25..1.25] - avg +0.5
            }
            return current;
        }

        public double CalculateAmbientTemperature()
        {
            return AmbientTemperature + rnd.NextDouble() -0.5;
        }

        public int CalculateHumidity()
        {
            return rnd.Next(HumidityPercentMin, HumidityPercentMax);
        }
    }
}
