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
        private static double CurrentMotorCurrent;
        private static double CurrentMotorPower;
        private static DateTime LastTime = DateTime.MinValue;

        public static MessageBody CreateTemperatureData(int counter, DataGenerationPolicy policy, bool reset = false)
        {
            if (reset)
            {
                TemperatureDataFactory.CurrentMotorTemperature = policy.CalculateMotorTemperature();
                TemperatureDataFactory.CurrentMotorFrequency = policy.CalculateMotorFrequency();
                TemperatureDataFactory.CurrentMotorCurrent = policy.CalculateMotorCurrent();
            }
            else
            {
                TemperatureDataFactory.CurrentMotorTemperature =
                    policy.CalculateMotorTemperature(TemperatureDataFactory.CurrentMotorTemperature);
                TemperatureDataFactory.CurrentMotorFrequency =
                    policy.CalculateMotorFrequency(TemperatureDataFactory.CurrentMotorFrequency);
                TemperatureDataFactory.CurrentMotorCurrent =
                    policy.CalculateMotorCurrent(TemperatureDataFactory.CurrentMotorCurrent);
            }

            var ambientTemperature = policy.CalculateAmbientTemperature();
            var ambientHumidity = policy.CalculateHumidity();
            var currentVoltage = policy.CalculateVoltage();
            var currentMotorPower = policy.CalculatePower(currentVoltage, TemperatureDataFactory.CurrentMotorCurrent);
            var currentMotorSpeed = policy.CalculateSpeed(TemperatureDataFactory.CurrentMotorFrequency, 1);
            var currentTime = DateTime.Now;

            var messageBody = new MessageBody
            {
                Motor = new Motor
                {
                    Temperature = TemperatureDataFactory.CurrentMotorTemperature,
                    Current =  TemperatureDataFactory.CurrentMotorCurrent,
                    Frequency = TemperatureDataFactory.CurrentMotorFrequency,
                    Power = TemperatureDataFactory.CurrentMotorPower,
                    Speed = currentMotorSpeed,
                    Torque = policy.CalculateTorque(currentMotorSpeed, currentMotorPower),
                    Voltage = currentVoltage, 
                    Energy = policy.CalculateMotorEnergy(currentMotorPower, TemperatureDataFactory.CurrentMotorPower, TemperatureDataFactory.LastTime)
                },
                Ambient = new Ambient
                {
                    Temperature = ambientTemperature,
                    Humidity = ambientHumidity
                },
                TimeCreated = string.Format("{0:O}", currentTime)
            };

            TemperatureDataFactory.CurrentMotorPower = currentMotorPower;
            TemperatureDataFactory.LastTime = currentTime;

            return messageBody;
        }
    }
}
