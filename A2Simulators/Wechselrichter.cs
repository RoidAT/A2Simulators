/*
 * Wechselrichter.cs
 * ---
 * Author: René Schütz
 * Der Wechselrichter soll die Leistung aller angeschlossenen Solarmodule addieren und die Effizienz berücksichtigen.
 * Die GetOutput Methode soll die aktuelle Leistung ausgeben.
 * ---
 */
using System.Collections.Generic;

namespace A01
{
    public class Wechselrichter : ISimulator
    {
        private const double Efficiency = 0.99;
        public long CurrentTime { get; set; }
        public List<ISimulator> ConnectedInputs { get; set; } = new List<ISimulator>();
        public List<ISimulator> ConnectedOutputs { get; set; } = new List<ISimulator>();

        public double CurrentPower { get; private set; }

        public void Connect(ISimulator input)
        {
            if (input is SolarmodulString solarmodulString)
            {
                ConnectedInputs.Add(solarmodulString);
                input.ConnectedOutputs.Add(this);
                // Wenn ein Wechselrichter bereits verbunden ist, füge den SolarmodulString hinzu
               //Inverter?.Connect(solarmodulString);
            }
            else if(input is Batteriespeicher battery)
            {
                ConnectedInputs.Add(battery);
                input.ConnectedOutputs.Add(this);
            }
            else
            {
                ConnectedInputs.Add(input);
                input.ConnectedOutputs.Add(this);
            }  
        }

        public void Step(long timeMs)
        {
            if(timeMs == CurrentTime) return;
            CurrentTime = timeMs;
            double inputPower = 0;
            double requestedPower = 0;
            foreach (var input in ConnectedInputs)
            {
                if(input is Batteriespeicher batteriespeicher)
                {
                    
                    foreach(var park in ConnectedOutputs)
                    {
                        foreach(var station in park.ConnectedOutputs)
                        {
                            if(station.GetOutput() is double output)
                            {
                                requestedPower += output;
                            }
                        }
                    }
                    CurrentPower = requestedPower / Efficiency;
                }
                else if(input is SolarmodulString solarString)
                {
                    input.Step(timeMs);
                    foreach(var solar in solarString.ConnectedInputs)
                    {
                        if(solar.GetOutput() is double o)
                        {
                            inputPower += o;
                        }
                    }
                    CurrentPower = inputPower * Efficiency;
                }
                
            }

            
        }

        public object GetOutput()
        {
            return CurrentPower;
        }
    }

}