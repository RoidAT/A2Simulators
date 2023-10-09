/*
 * Ladepark.cs
 * ---
 * Author: Marcel Schörghuber
 * Das Solarmodul soll basierend auf der aktuellen Uhrzeit und der Position der Sonne die aktuelle Leistung berechnen.
 * in der GetOutput Methode soll die aktuelle Leistung ausgegeben werden.
 * ---
 */

using System;
using System.Collections.Generic;

namespace SIM
{
    public class Ladepark : ISimulator
    {
        public double MaxPower { get; set; }
        public double CurrentPower { get; set; }
        public long CurrentTime { get; set; }
        public List<ISimulator> ConnectedInputs { get; set; } = new List<ISimulator>();
        public List<ISimulator> ConnectedOutputs { get; set; } = new List<ISimulator>();

        public void Connect(ISimulator input)
        {
            if (input is Wechselrichter)
            {
                ConnectedInputs.Add(input);
                input.ConnectedOutputs.Add(this);
            }
            else
            {
                throw new InvalidOperationException("An attempt was made to connect an incompatible module.");
            }
        }

        public void Step(long timeMs)
        {
            if (timeMs == CurrentTime) return; //Don't step twice

            CurrentPower = 0;

            foreach(var station in ConnectedOutputs)
            {
                station.Step(timeMs);

                if(station.GetOutput() is double output)
                {
                    CurrentPower += output;
                }
                
            }

            foreach(var inverter in ConnectedInputs)
            {
                inverter.Step(timeMs);
            }

            CurrentTime = timeMs;
        }

        public object GetOutput()
        {
            return CurrentPower;
        }
    }
}