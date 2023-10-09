/*
 * PhotovoltaikAnlage.cs
 * ---
 * Author: René Schütz, Marlene Berger
 * Die PhotovoltaikAnlage besteht aus mehreren SolarmodulStrings und einem Wechselrichter.
 * Die GetOutput Methode soll die aktuelle Leistung ausgeben.
 * ---
 */

using System;
using System.Collections.Generic;

namespace SIM
{
    public class PhotovoltaikAnlage : ISimulator
    {
        public long CurrentTime { get; set; }
        public List<ISimulator> ConnectedInputs { get; set; } = new List<ISimulator>();
        public List<ISimulator> ConnectedOutputs { get; set; } = new List<ISimulator>();
        //public Wechselrichter Inverter { get; private set; }

        public double CurrentPower { get; private set; }

        public void Connect(ISimulator input)
        {
            if (input is Wechselrichter wechselrichter)
            {
                ConnectedInputs.Add(input);
                input.ConnectedOutputs.Add(this);
            }
            else
            {
                throw new InvalidOperationException("Nur SolarmodulStrings oder Wechselrichter können an eine PhotovoltaikAnlage angeschlossen werden.");
            }
        }

        public void Step(long timeMs)
        {
            if(timeMs == CurrentTime) return;
            CurrentTime = timeMs;

            CurrentPower = 0;

            foreach(var inverter in ConnectedInputs)
            {
                if(inverter is Wechselrichter wechselrichter)
                {
                    wechselrichter.Step(timeMs);
                    CurrentPower += (double)wechselrichter.GetOutput();
                }
            }

            
        }

        
        public object GetOutput()
        {
            return CurrentPower;
        }
    }
}