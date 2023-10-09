/*
 * SolarmodulString.cs
 * ---
 * Author: René Schütz
 * Der SolarmodulString, besteht aus mehreren Solarmodulen und soll die Leistung der einzelnen Solarmodule addieren.
 * die GetOutput Methode gibt die aktuelle Leistung aller Solarmodule aus.
 * ---
 */

using System;
using System.Collections.Generic;

namespace SIM
{
    public class SolarmodulString : ISimulator
    {
        public double CurrentPower { get; private set; }
        public long CurrentTime { get; set; }
        public List<ISimulator> ConnectedInputs { get; set; } = new List<ISimulator>();
        public List<ISimulator> ConnectedOutputs { get; set; } = new List<ISimulator>();

        public void Connect(ISimulator input)
        {
            if (input is Solarmodul)
            {
                ConnectedInputs.Add(input);
                input.ConnectedOutputs.Add(this);
            }
            else
            {
                throw new InvalidOperationException(
                    "Nur Solarmodule können an einen SolarmodulString angeschlossen werden.");
            }
        }

        public void Step(long timeMs)
        {
            if(timeMs == CurrentTime) return;
            CurrentTime = timeMs;
            CurrentPower = 0;
            foreach (var input in ConnectedInputs)
            {
                input.Step(timeMs);
                if (input.GetOutput() is double output)
                {
                    CurrentPower += output;
                }
            }
        }

        public object GetOutput()
        {
            return CurrentPower;
        }
    }

}