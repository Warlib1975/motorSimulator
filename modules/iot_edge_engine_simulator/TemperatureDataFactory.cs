// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace AzureIotEdgeSimulatedTemperatureSensor
{
    public class TemperatureDataFactory
    {
        private static readonly Random rand = new Random();
        private static double CurrentMotorTemperature;
        private static double CurrentMotorFrequency;

        public static MessageBody CreateTemperatureData(int counter, DataGenerationPolicy policy, bool reset = false)
        {
            if (reset)
            {
                TemperatureDataFactory.CurrentMotorTemperature = policy.CalculateMotorTemperature();
                TemperatureDataFactory.CurrentMotorFrequency = policy.CalculateMotorFrequency();
            }
            else
            {
                TemperatureDataFactory.CurrentMotorTemperature =
                    policy.CalculateMotorTemperature(TemperatureDataFactory.CurrentMotorTemperature);
                TemperatureDataFactory.CurrentMotorFrequency =
                    policy.CalculateMotorFrequency(TemperatureDataFactory.CurrentMotorFrequency);
            }

            var motorCurrent = policy.CalculateCurrent(TemperatureDataFactory.CurrentMotorTemperature);
            var ambientTemperature = policy.CalculateAmbientTemperature();
            var ambientHumidity = policy.CalculateHumidity();

            var messageBody = new MessageBody
            {
                Motor = new Motor
                {
                    Temperature = TemperatureDataFactory.CurrentMotorTemperature,
                    Current =  motorCurrent,
                    Frequency = TemperatureDataFactory.CurrentMotorFrequency
                },
                Ambient = new Ambient
                {
                    Temperature = ambientTemperature,
                    Humidity = ambientHumidity
                },
                TimeCreated = string.Format("{0:O}", DateTime.Now)
            };

            return messageBody;
        }
    }
}
