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
            MotorFrequencyMax = 50;
            MotorCurrentMin = 0.7;
            MotorCurrentMax = 1;
            AmbientTemperature = 21;
            HumidityPercentMin = 24;
            HumidityPercentMax = 27;
            VoltageMin = 360;
            VoltageMax = 380;
            _normal = (MotorCurrentMax - MotorCurrentMin) / (MotorTemperatureMax - MotorTemperatureMin);
        }

        public double MotorTemperatureMin { get; private set; }
        public double MotorTemperatureMax { get; private set; }
        public double MotorCurrentMin { get; private set; }
        public double MotorCurrentMax { get; private set; }
        public double MotorFrequencyMin { get; private set; }
        public double MotorFrequencyMax { get; private set; }
        public double AmbientTemperature { get; private set; }
        public int HumidityPercentMin { get; private set; }
        public int HumidityPercentMax { get; set; }
        public int VoltageMin { get; private set; }
        public int VoltageMax { get; set; }

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

        public double CalculateMotorCurrent(double? currentCurrent = null)
        {
            var current = currentCurrent ?? MotorCurrentMin;
            current = (current < MotorCurrentMin) ? MotorCurrentMin : current;
            if (current > MotorCurrentMax)
            {
                current -= rnd.NextDouble()/200;
            }
            else
            {
                current += rnd.NextDouble()/200; // add value between [-0.25..1.25] - avg +0.5
            }
            return current;
        }

        public double CalculateSpeed(double currentFrequency, int PolesNum)
        {
            return 60*currentFrequency/PolesNum;
        }

        public double CalculateTorque(double currentSpeed, double CurrentPower)
        {
            return 9.55*CurrentPower/currentSpeed;
        }

        public double CalculatePower(double U, double I)
        {
            double efficiency = 0.72;
            double cosfi = 0.82;
            return Math.Sqrt(3)*U*I*efficiency*cosfi;
        }
        public double CalculateMotorEnergy(double CurrentPower, double PreviousPower, DateTime lastTime)
        {
            if (lastTime == DateTime.MinValue)
                return 0;
            double averagePower = (CurrentPower + PreviousPower)/2;
            averagePower = averagePower/1000;
            TimeSpan diff = DateTime.Now - lastTime;
            return averagePower*diff.TotalHours; //kWt/Hours
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
        public int CalculateVoltage()
        {
            return rnd.Next(VoltageMin, VoltageMax);
        }
    }
}
